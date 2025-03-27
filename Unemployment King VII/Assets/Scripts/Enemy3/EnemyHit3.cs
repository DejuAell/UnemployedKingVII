using UnityEngine;

public class EnemyHit3 : MonoBehaviour
{
    public EnemyBehaviour3 enemyBehaviour;

    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            if (enemyBehaviour.cooling3 == false)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
                
            }
        }
    }
}
