using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealthPoint = 100;
    public int currentHealth;

    int layerMask = 1 << 8;

    public HealthBar healthbar;

    float horizontalInput;
    public float speed = 5;
    public float jumpHeight = 5;
    float jumpCount = 2;

    float knockbackForce = 3f;
    public float knockbackCounter = 0;
    float knockbackTotalTime = .1f;
    public bool knockbacked = false;

    public bool knockFromRight;
    bool knockFromBelow;

    Rigidbody2D rb;
    bool isJumping = false;

    Animator anim;
    SpriteRenderer sr;

    public int damage = 23;
     
    float atkEndedTimer = 1f;

    public Transform attackRange;
    Ray ray;

    public SwordSystem ss;
    public SlimeBehaviour sb;
    public SlimeBehaviour sb_2;
    public DialogueSystem ds;
    public Chomper cs;
    public Transform respawnPoint;
    public GameObject flameSmitePref;
    public GameObject shieldPrefab;
    public GameObject chomper;
    public GameObject enemy1;
    public GameObject echo;

    public float dashForce = 4f;
    float startDashTimer = 0.1f;
    float currentDashTimer;
    float dashDirection;
    bool isDashing;

    bool hitObstacle = false;

    public float timeBtwSpawns;
    public float startTimeBtwSpawns;

    void Start()
    {
        currentHealth = maxHealthPoint;
        healthbar.SetMaxHealth(maxHealthPoint);
        layerMask = ~layerMask;
        ray = new Ray(attackRange.position, Vector3.right);

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && jumpCount != 0)
        {
            anim.SetBool("isJumping", true);
            if(jumpCount == 1)
            {
                anim.SetBool("isJumping", false);
                anim.Play("Jump");
                anim.SetBool("isJumping", true);
                isJumping = true;
            }

            anim.SetBool("isWalking", false);
            
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            jumpCount--;
        }

        //Debug.DrawRay(attackRange.position, Vector2.right * 2.9f * transform.localScale.x, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(attackRange.position, Vector2.right * transform.localScale.x, 2.9f, layerMask);
            if (hit.collider != null)
            {
                if(string.Equals(hit.collider.gameObject.name, "Slime"))
                {
                    sb.TakeDamage(damage);
                }

                if (string.Equals(hit.collider.gameObject.name, "Slime_2"))
                {
                    sb_2.TakeDamage(damage);
                }
            }
            
            else
            {
                Debug.DrawRay(attackRange.position, Vector2.right * 2.9f * transform.localScale.x, Color.green);
            }
            
            anim.Play("Attack");
            ss.Dissolving();
            atkEndedTimer = 2f;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(flameSmitePref, new Vector3(20.97f, -1.44f, -0.1f), Quaternion.identity);
            sb.TakeDamage(40);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(shieldPrefab, transform.position + new Vector3(0, 0.5f, -0.1f), Quaternion.identity);
            sb.rb.AddForce((enemy1.transform.position - transform.position).normalized * jumpHeight, ForceMode2D.Impulse);
            RaycastHit2D hit = Physics2D.Raycast(attackRange.position, Vector2.right * transform.localScale.x, 2.9f, layerMask);
            if (hit.collider != null)
            {
                if (string.Equals(hit.collider.gameObject.name, "Slime"))
                {
                    sb.TakeDamage(70);
                }
            }
        }

        if (atkEndedTimer > 0)
        {
            atkEndedTimer -= Time.deltaTime;
        }
        if(atkEndedTimer <= 0f)
        {
            ss.Regaining();
        }

        if(currentHealth <= 0)
        {
            transform.position = respawnPoint.position;
            currentHealth = maxHealthPoint;
            healthbar.SetMaxHealth(maxHealthPoint);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isDashing = true;
            currentDashTimer = startDashTimer;
            rb.velocity = Vector2.zero;
            dashDirection = transform.localScale.x;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            isDashing = false;
        }
        if (isDashing)
        {
            rb.velocity = transform.right * dashDirection * dashForce;

            currentDashTimer -= Time.deltaTime;
            if(currentDashTimer <= 0)
            {
                isDashing = false;
            }
        }

        if (cs.damagePlayer)
        {
            sr.color = new Color(255f, 0f, 0f, 1f);
            knockbackCounter = knockbackTotalTime;
            knockbacked = true;
            if (chomper.transform.position.x > transform.position.x)
            {
                knockFromRight = true;
            }
            if (chomper.transform.position.x <= transform.position.x)
            {
                knockFromRight = false;
            }
            cs.damagePlayer = false;
            cs.canBeDamaged = false;
            TakeDamage(20);
        }
    }

    void FixedUpdate()
    {
        if(knockbackCounter <= 0)
        {
            sr.color = new Color(255f, 255f, 255f, 1f);
            if (!knockbacked)
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");

                if (horizontalInput == 0)
                {
                    anim.SetBool("isWalking", false);
                }
                else
                {
                    if (horizontalInput < 0)
                    {
                        transform.localScale = new Vector2(-1, 1);
                    }
                    else
                    {
                        transform.localScale = new Vector2(1, 1);
                    }
                    anim.SetBool("isWalking", true);
                }
                transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);
            }
        }
        else
        {
            anim.SetBool("isWalking", false);
            if (knockFromRight)
            {
                rb.velocity = new Vector2(-knockbackForce, knockbackForce);
            }
            if (!knockFromRight)
            {
                rb.velocity = new Vector2(knockbackForce, knockbackForce);
            }
            if (knockFromBelow)
            {
                rb.velocity = new Vector2(0, knockbackForce * 2f);
            }
            knockbackCounter -= Time.deltaTime;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            jumpCount = 2;
            anim.SetBool("isJumping", false);
            knockbacked = false;
            isJumping = false;
        }

        if (collision.collider.tag == "Obstacle")
        {
            sr.color = new Color(255f, 0f, 0f, 1f);
            knockFromBelow = true;
            knockbackCounter = knockbackTotalTime;
            TakeDamage(20);
        }

        if(collision.collider.tag == "Enemy")
        {
            sr.color = new Color(255f, 0f, 0f, 1f);
            knockbackCounter = knockbackTotalTime;
            knockbacked = true;
            if (collision.transform.position.x > transform.position.x)
            {
                knockFromRight = true;
            }
            if (collision.transform.position.x <= transform.position.x)
            {
                knockFromRight = false;
            }
            TakeDamage(20);
        }  
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth);
    }
}
