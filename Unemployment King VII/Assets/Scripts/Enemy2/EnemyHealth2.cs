using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth2 : MonoBehaviour

{
    //public EnemyBehaviour enemyBehaviour;

    [SerializeField] private float startingHealth;
    [SerializeField] private float currentHealth; //{ get; private set; }
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponentInParent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            //if frames

        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("Die");
                GetComponentInParent<EnemyBehaviour2>().enabled = false;
                dead = true;
                
            }

        }
    }

    private void Update()
    {

    }

}
