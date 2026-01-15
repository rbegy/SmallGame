using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject Player;


    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
    }

    public void Die()
    {
        Player player = GetComponent<Player>();
        player.enabled = false;
        Player.transform.localScale = new Vector3(0, .5f, 0);
    }
}
