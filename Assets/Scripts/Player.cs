using Unity.VisualScripting;
using UnityEditor.U2D.Sprites;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 0f;
    public Vector2 direction;

    private Rigidbody2D rb;
    //private bool isGrounded;

    private SpriteRenderer spriter;

    public Enemy enemy;

    private int cherrycounter = 0;

    private Vector2 distanceToEnemy;

    //private float jumpVelocity = 12f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();

        if(rb != null)
        {
            rb.gravityScale = 0;
            rb.freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        distanceToEnemy = transform.position - enemy.transform.position;
        PlayerMovement();
        if(rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
        else
        {
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
        //isGrounded =
        //Physics2D.Raycast(transform.position, Vector2.down, 0.15f);


    }
    
    public void PlayerMovement()
    {
        float h = 0f;
        float v = 0f;
            //Input.GetKeyDown(KeyCode.D)
        if(Keyboard.current.dKey.isPressed)
        {
            h = 1f;
            spriter.flipX = false;
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            h = -1;
            spriter.flipX = true;
        }

        if (Keyboard.current.wKey.isPressed)
        {
            v = 1f;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            v = -1f;
        }

        if(h != 0f || v != 0)
        {
            speed = 2f;
            direction = new Vector2(h, v).normalized;
        }
        else
        {
            speed = 0f;
            direction = Vector2.zero;
        }

        /*
        if (Keyboard.current.wKey.wasPressedThisFrame && isGrounded)
        {
            if(rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
            }
            else
            {
                transform.position += Vector3.up * (jumpVelocity * 0.1f);
            }
        }

        */

        //if (Keyboard.current.eKey.wasPressedThisFrame && (float)distanceToEnemy  < 1)
        //{
        //    Attack();
        //}

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Collider2D>() != null
            && collision.gameObject.CompareTag("Cherry"))
        {
            cherrycounter++;
            Destroy(collision.gameObject);
        }
    }

    private void Attack()
    {
        enemy.TakeDamage(5);
    }

}
