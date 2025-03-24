using UnityEngine;

public class EnemyHit2 : MonoBehaviour
{
    public EnemyBehaviour2 enemyBehaviour;

    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            if (enemyBehaviour.cooling == false)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
                
            }
        }
    }
}
