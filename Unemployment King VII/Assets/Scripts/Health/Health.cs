using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour

{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth; //{ get; private set; }
    private Animator anim;
    private bool dead;
    private Rigidbody2D rb;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float _damage)
    {
       currentHealth = Mathf.Clamp(currentHealth - (_damage * startingHealth), 0, startingHealth);
        
        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            //if frames

        }
        else
        {
            if(!dead)
            {
            anim.SetTrigger("Die");
            GetComponent<PlayerMovement>().enabled = false;
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            dead = true;
            }
           
        }
    }

    private void Update()
    {
        
    }
    public void AddHealth(float _value)
    {
    currentHealth = Mathf.Clamp(currentHealth + (_value * startingHealth), 0, startingHealth);
    }
}
