using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper : MonoBehaviour
{
    public bool playerInside = false;
    public bool canBounce = false;
    public bool damagePlayer = false;
    public bool canBeDamaged = false;

    public PlayerController pc;
    public Rigidbody2D player;
    Animator anim;

    // Start is called before the first frame update
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Bounce()
    {
        canBeDamaged = false;
        damagePlayer = false;
        canBounce = true;
        GetComponent<Collider2D>().isTrigger = false;
    }

    void NoBounce()
    {
        GetComponent<Collider2D>().isTrigger = true;
        canBounce = false;
        
        anim.SetBool("playerDetected", false);
    }

    void CanBeDamaged()
    {
        canBeDamaged = true;
    }

    void DamagePlayer()
    {
        damagePlayer = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("playerDetected", true);
            Debug.Log("Player Entered");
            playerInside = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (canBeDamaged)
            {
                Debug.Log("Player take damage after");
                DamagePlayer();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            damagePlayer = false;
            playerInside = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (canBounce)
            {
                player.AddForce(Vector2.up * pc.jumpHeight * 1.2f, ForceMode2D.Impulse);
            }
        }
    }
}
