using UnityEngine;

public class SpikesDamage : MonoBehaviour
{
    [SerializeField] private float damage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
