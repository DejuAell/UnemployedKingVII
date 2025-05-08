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
    [SerializeField] private float iFramesDuration = 2f;
    [SerializeField] private int numberOfFlashes = 5;
    private SpriteRenderer spriteRend;
    private bool isInvulnerable = false;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float _damage)
    {
        if (isInvulnerable) return;

        currentHealth = Mathf.Clamp(currentHealth - (_damage * startingHealth), 0, startingHealth);
        
        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(Invulnerability());
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
    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(13, 12, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));

        }

        Physics2D.IgnoreLayerCollision(13, 12, false);

        Physics2D.IgnoreLayerCollision(13, 10, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));

        }

        Physics2D.IgnoreLayerCollision(13, 10, false);
        isInvulnerable = false;


    
 }
}