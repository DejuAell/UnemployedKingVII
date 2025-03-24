using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck3 : MonoBehaviour
{
    private EnemyBehaviour3 enemyParent;

    private void Awake()
    {
        enemyParent = GetComponentInParent<EnemyBehaviour3>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player1"))
        {
            gameObject.SetActive(false);
            enemyParent.target = collider.transform;
            enemyParent.inRange = true;
            enemyParent.hotZone.SetActive(true);
        }
    }
}
