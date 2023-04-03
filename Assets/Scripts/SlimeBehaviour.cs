using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : MonoBehaviour
{
    public int maxHealthPoint = 100;
    public int currentHealth;

    public HealthBar healthbar;

    Transform player;
    float targetTime = 3f;
    float speed = 2f;
    float jumpHeight = 7f;
    bool isGrounded = false;

    public GameObject slashPrefab;
    public GameObject lootPrefab;
    public GameObject lootTarget;
    public GameObject resPoint;

    Vector3 offset;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealthPoint;
        healthbar.SetMaxHealth(maxHealthPoint);
        offset = new Vector3(0.5f, 0, -0.2f);
        lootTarget = GameObject.FindGameObjectWithTag("DropLootTracker");

        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(player.position.x, 0, 0);

        if (targetTime > 0)
        {
            targetTime -= Time.deltaTime;
        }

        Vector3 scale = transform.localScale;

        if (isGrounded)
        {
            if (player.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * 1;
            }
            else
            {
                scale.x = Mathf.Abs(scale.x) * -1;
            }
            transform.localScale = scale;
        }
        else
        {
            if (player.position.x > transform.position.x)
            {
                transform.Translate(speed * Time.deltaTime, 0f, 0f);
            }
            else
            {
                transform.Translate(speed * Time.deltaTime * -1, 0f, 0f);
            }
        }

        if (targetTime <= 0)
        {   
            Jump();
            speed = 2;
        }

        if(currentHealth <= 0)
        {
            for (int i = 0; i < maxHealthPoint / 10; i++)
            {
                var go = Instantiate(lootPrefab, transform.position + new Vector3(Random.Range(0, 1.2f), 1.2f), Quaternion.identity);

                go.GetComponent<LootFollow>().target = lootTarget.transform;
            }
            transform.position = resPoint.transform.position;
            currentHealth = maxHealthPoint;
            healthbar.SetMaxHealth(maxHealthPoint); 
        }
    }

    void Jump()
    {
        targetTime = 2f;
        speed = 4;
        rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth);

        Instantiate(slashPrefab, transform.position + offset, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
