using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class EnemyController : MonoBehaviour
{
    //[SerializeField]
    //private ExperienceBar experienceBar;

    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;
    [SerializeField]
    private int defence = 0;
    [SerializeField]
    private int attackDamage;
    [SerializeField]
    private float attackCooldown;

    [SerializeField]
    private float respawnTime = 10f;
    [SerializeField]
    private int dropExperience = 0;

    [SerializeField]
    private float rangeX;
    [SerializeField]
    private float rangeY;
    [SerializeField]
    private float colliderDistance;
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField]
    private BoxCollider2D boxCollider;

    [SerializeField]
    private HealthBar healthBar;

    private Health playerHealth;

    [SerializeField]
    private LayerMask playerLayer;

    private int nextMove;

    [SerializeField]
    private GameObject[] dropItem;

    // 스파인
    [SerializeField]
    private SkeletonAnimation skeletonAnimation;
    [SerializeField]
    private AnimationReferenceAsset[] animClip;

    private AnimState animState;

    private string currentAnimation;
    private Rigidbody2D rigid;

    private LevelSystem levelSystem;

    private ExperienceBar experienceBar;

    private enum AnimState
    {
        Idle, Walk, Attack, Dead
    }

    private void Awake()
    {
        levelSystem = new LevelSystem();
        experienceBar = FindObjectOfType<ExperienceBar>();
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        RandomNextMove();
    }

    private void Update()
    {
        SetCurrentAnimation(animState);
        Attack();
        //print(animState);
    }

    private void FixedUpdate()
    {
        //rigid.velocity = new Vector2(temp * 300 * Time.deltaTime, rigid.velocity.y);
        Walk();
        GroundCheck();
    }

    private void AsyncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale)
    {
        // 동일한 애니메이션 재생시 실행 안함
        if(animClip.name.Equals(currentAnimation))
        {
            return;
        }

        skeletonAnimation.state.SetAnimation(0, animClip, loop).TimeScale = timeScale;
        skeletonAnimation.loop = loop;
        skeletonAnimation.timeScale = timeScale;

        currentAnimation = animClip.name;
    }

    private void SetCurrentAnimation(AnimState state)
    {
        switch(state)
        {
            case AnimState.Idle:
                AsyncAnimation(animClip[(int)AnimState.Idle], true, 1f);
                break;
            case AnimState.Walk:
                AsyncAnimation(animClip[(int)AnimState.Walk], true, 1f);
                break;
            case AnimState.Attack:
                AsyncAnimation(animClip[(int)AnimState.Attack], false, 1f);
                break;
            case AnimState.Dead:
                AsyncAnimation(animClip[(int)AnimState.Dead], false, 1f);
                break;
        }
    }

    private void Walk()
    {
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
    }

    private void RandomNextMove()
    {

        float randomNextMoveTime = Random.Range(2f, 4f);

        if(animState != AnimState.Dead && !PlayerInSight())
        {
            nextMove = Random.Range(-1, 2);
            Invoke("RandomNextMove", randomNextMoveTime);

            if (nextMove == 0)
            {
                animState = AnimState.Idle;
            }
            else
            {
                animState = AnimState.Walk;
                transform.localScale = new Vector3(nextMove * 0.5f, 0.5f, 1f);
            }
        }
        else if(animState != AnimState.Dead)
        {
            CancelInvoke();
        }
    }

    private void GroundCheck()
    {
        Vector2 downVector = new Vector2(rigid.position.x + nextMove * 0.5f, rigid.position.y + 0.2f);
        Debug.DrawRay(downVector, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(downVector, Vector3.down, 1, LayerMask.GetMask("Ground"));

        Vector2 frontVector = new Vector3(rigid.position.x + nextMove, rigid.position.y + 0.5f);
        if(nextMove == 1)
        {
            frontVector = new Vector3(rigid.position.x, rigid.position.y + 0.5f);
        }
        else
        {
            frontVector = new Vector3(rigid.position.x + nextMove, rigid.position.y + 0.5f);
        }
        Debug.DrawRay(frontVector, Vector3.right, new Color(0, 1, 0));
        RaycastHit2D frontRayHit = Physics2D.Raycast(frontVector, Vector3.right, 1, LayerMask.GetMask("Ground"));
        if (rayHit.collider == null || frontRayHit.collider != null)
        {
            nextMove *= -1;
            if(nextMove != 0)
            {
                transform.localScale = new Vector3(nextMove * 0.5f, 0.5f, 1f);
            }
            CancelInvoke();
            Invoke("RandomNextMove", 5);
        }
    }

    public void EnemyTakeDamage(int damage)
    {
        if(animState != AnimState.Dead)
        {
            healthBar.gameObject.SetActive(true);
            if ((damage - defence) > 0)
            {
                currentHealth -= damage - defence;
            }
            else
            {
                currentHealth -= 1;
            }

            if (currentHealth > 0)
            {
                healthBar.SetHealth(currentHealth);
                Debug.Log("적 공격 받음" + (damage - defence));
            }
            else
            {
                if (animState != AnimState.Dead)
                {
                    healthBar.SetHealth(currentHealth);
                    healthBar.gameObject.SetActive(false);
                    Die();
                }
            }
        }
    }

    private void Die()
    {
        //LevelSystem levelSystem = new LevelSystem();
        //experienceBar.SetLevelSystem(levelSystem);

        Debug.Log("Died");

        nextMove = 0;

        animState = AnimState.Dead;

        DropItem();
        Debug.Log(dropExperience);

        //levelSystem.AddExperience(dropExperience);
        experienceBar.AddExperience(dropExperience);
        //Debug.Log(levelSystem.GetLevelNumber());
        Invoke("Disabled", 3);
    }

    private void Disabled()
    {
        gameObject.SetActive(false);
        Invoke("Respawn", respawnTime);
    }

    private void Respawn()
    {
        gameObject.SetActive(true);
        animState = AnimState.Idle;

        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);

        Invoke("RandomNextMove", 5);
    }

    private void Attack()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight() && animState != AnimState.Dead)
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                nextMove = 0;
                animState = AnimState.Attack;
                DamagePlayer();
            }
            else if(attackCooldown - cooldownTimer <= 0.2f)
            {
                animState = AnimState.Idle;
            }
        }
        else if(!PlayerInSight() && animState == AnimState.Attack)
        {
            animState = AnimState.Idle;
            RandomNextMove();
        }
    }

    private bool PlayerInSight()
    {
        Vector3 size = new Vector3(boxCollider.bounds.size.x * rangeX, boxCollider.bounds.size.y * rangeY, boxCollider.bounds.size.z);
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rangeX * transform.localScale.x * colliderDistance, size, 0, Vector2.left, 0, playerLayer);

        if(hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Vector3 size = new Vector3(boxCollider.bounds.size.x * rangeX, boxCollider.bounds.size.y * rangeY, boxCollider.bounds.size.z);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * rangeX * transform.localScale.x * colliderDistance, size);
    }

    private void DamagePlayer()
    {
        if(PlayerInSight())
        {
            playerHealth.PlayerTakeDamage(attackDamage);
        }
    }

    private void DropItem()
    {
        // 드랍템 생성
        for(int i = 0; i < dropItem.Length; i++)
        {
            Instantiate(dropItem[i], boxCollider.bounds.center, Quaternion.identity);
        }
    }
}
