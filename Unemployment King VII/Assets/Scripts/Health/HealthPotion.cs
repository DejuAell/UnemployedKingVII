using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
     [SerializeField] private float healthValue;

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.tag == "Player1")
          {
                collision.GetComponent<Health>().AddHealth(healthValue);
                gameObject.SetActive(false);
          }
     }
}
