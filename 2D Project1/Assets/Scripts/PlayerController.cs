using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string currentMapName;

    [SerializeField]
    private DialogueManager dialogueManager;

    private LevelSystem levelSystem;

    private Rigidbody2D rigidBody;
    private Animator anim;

    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private float jupmPower = 5;

    [SerializeField]
    private Transform pos;

    [SerializeField]
    private Vector2 attackRange;

    [SerializeField]
    private Transform attackPos;
    public int attackDamage = 20;
    public int defence = 0;

    [SerializeField]
    private float checkRadius;
    [SerializeField]
    private LayerMask isLayer;
    private float currentTime;
    [SerializeField]
    private float cooldown = 0.5f;

    private bool isGround;

    public GameObject scanObject;

    private Vector3 playerRay = new Vector3(2, 0, 0);

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentMapName = "Town";
    }

    private void Update()
    {
        if(!Inventory.inventoryActivated)
        {

            PlayerAnimation();
            if (dialogueManager.isAction == false)
            {
                PlayerMove();
                PlayerJump();
            }
            else
            {
                rigidBody.velocity = new Vector2(0f, 0f);
            }
            PlayerAttack();
            PlayerDialogue();
        }
        PlayerRay();
    }

    private void PlayerMove()
    {
        float horizotal = Input.GetAxis("Horizontal");

        rigidBody.velocity = new Vector2(horizotal * speed, rigidBody.velocity.y);

        if (horizotal > 0.01f)
        {
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
        else if (horizotal < -0.01f)
        {
            transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
        }
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jupmPower);
            anim.SetTrigger("jump");
            isGround = false;
        }
            
    }

    private void PlayerAnimation()
    {
        float horizotal = Input.GetAxis("Horizontal");
        if (dialogueManager.isAction == false)
        {
            anim.SetBool("run", horizotal != 0);
            anim.SetBool("isGround", isGround);
        }
        else
        {
            anim.SetBool("run", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    private void PlayerAttack()
    {
        if (currentTime <= 0 && isGround && dialogueManager.isAction == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                currentTime = cooldown;
            }
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }

    private void Attack()
    {
        anim.SetTrigger("attack1");

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPos.position, attackRange, 0);

        foreach(Collider2D enemy in hitEnemies)
        {
            if(enemy.tag == "Enemy")
            {
                enemy.GetComponent<EnemyController>().EnemyTakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPos == null)
        {
            return;
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(attackPos.position, attackRange);
    }

    private void PlayerDialogue()
    {
        if(Input.GetKeyDown(KeyCode.E) && scanObject != null && isGround == true)
        {
            dialogueManager.ScanAction(scanObject);
        }
    }

    private void PlayerRay()
    {
        float horizotal = Input.GetAxis("Horizontal");

        playerRay = new Vector3(4, 0, 0);
        Vector2 rayVector = new Vector2(rigidBody.position.x - playerRay.x * 0.5f, rigidBody.position.y);
        Debug.DrawRay(rayVector, playerRay, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rayVector, playerRay, 4f, LayerMask.GetMask("Object"));
        
        if(rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }

    public void PlayerIncreaseDef(int amount)
    {
        defence += amount;
    }

    public void PlayerDecreaseDef(int amount)
    {
        defence -= amount;
    }

    public void PlayerIncreaseAttackDamage(int amount)
    {
        attackDamage += amount;
    }
    public void PlayerDecreaseAttackDamage(int amount)
    {
        attackDamage -= amount;
    }

    // 레벨업시 플레이어에게 영향가도록 설정
    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        levelSystem.onLevelChanged += LevelSystemOnLevelChanged;
    }

    // 레벌업시 기능 추가
    private void LevelSystemOnLevelChanged(object sender , EventArgs e)
    {
        Debug.Log("레벨업");
    }
}