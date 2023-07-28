using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;

    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private PlayerController player;
    private Animator anim;
    private bool dead;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        anim = GetComponent<Animator>();
    }

    public void PlayerTakeDamage(int damage)
    {
        if((damage - player.defence) > 0)
        {
            currentHealth -= (damage - player.defence);
        }
        else
        {
            currentHealth -= 1;
        }

        if (currentHealth > 0)
        {
            healthBar.SetHealth(currentHealth);
        }
        else
        {
            if(!dead)
            {
                healthBar.SetHealth(currentHealth);
                anim.SetTrigger("die");
                Debug.Log("Die");

                if (GetComponent<PlayerController>() != null)
                {
                    GetComponent<PlayerController>().enabled = false;
                }
                if (GetComponent<EnemyController>() != null)
                {
                    GetComponent<EnemyController>().enabled = false;
                }

                dead = true;
            }
        }
    }

    public void PlayerRecoveryHp(int amount)
    {
        currentHealth += amount;
        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.SetHealth(currentHealth);
    }

    public void PlayerIncreaseHp(int amount)
    {
        maxHealth += amount;
        healthBar.SetHealth(currentHealth);
    }

    public void PlayerDecreaseHp(int amount)
    {
        maxHealth -= amount;
        healthBar.SetHealth(currentHealth);
    }
}
