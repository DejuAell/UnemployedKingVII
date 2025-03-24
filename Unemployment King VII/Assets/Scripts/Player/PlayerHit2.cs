using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit2 : MonoBehaviour
{


    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            // Attempt to get the EnemyHealth component
            EnemyHealth3 enemyHealth = collision.GetComponent<EnemyHealth3>();

            // Check if the component is null before calling TakeDamage
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("EnemyHealth component not found on " + collision.gameObject.name);
            }
        }
    }

}
