using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    public void OnTriggerEnter(Collider player)
    {
        Health health = player.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
        if(player.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        Destroy(gameObject);
    }
}

