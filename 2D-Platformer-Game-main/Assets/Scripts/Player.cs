using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;


public class Movement : MonoBehaviour
{

    Rigidbody2D rb;

    float xvel, yvel;
    float health;

    public bool isFacingRight;

    public bool isGrounded;
    float delay;

    public Animator anim;

    public LayerMask groundLayerMask;
    public LayerMask barrierLayerMask;
    public LayerMask enemyLayerMask;

    void Start()
    {
        health = 5;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GetComponent<SpriteRenderer>().flipX = false;
        groundLayerMask = LayerMask.GetMask("Ground");
        barrierLayerMask = LayerMask.GetMask("Death Barrier");
        enemyLayerMask = LayerMask.GetMask("Enemy");
        delay = 0;
    }
    void Update()
    {
        xvel = 0;
        yvel = rb.linearVelocity.y;



        if (Input.GetKey("a"))
        {
            GetComponent<SpriteRenderer>().flipX = true;
            xvel = -5;
        }

        if (Input.GetKey("d"))
        {
            GetComponent<SpriteRenderer>().flipX = false;
            xvel = 5;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            yvel = 15;
        }

        GroundCheck();
        DeathBarrierCheck();
        EnemyCheckVert(.75f);
        EnemyCheckHori(0.5f);
        CheckForSprint();
        DoWalkAnimation();
        HealthCheck();

        rb.linearVelocity = new Vector2(xvel, yvel);

        if (Input.GetKeyDown("r"))
        {
            transform.position = new Vector2(-8, -5);
        }



        //do jump animation
        anim.SetBool("isJumping", false);

        if (yvel != 0 && isGrounded == false)
        {
            anim.SetBool("isJumping", true);
        }
    }

    void CheckForSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //xvel = xvel * 2;

            xvel *= 2;
            anim.SetBool("isRunning", true);

        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    void DoWalkAnimation()
    {
        if (xvel < -0.5f || xvel > 0.5f)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    void GroundCheck()
    {
        isGrounded = false;


        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.2f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayerMask);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
    }
    void DeathBarrierCheck()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.2f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, barrierLayerMask);
        if (hit.collider != null)
        {
            health -= 1;
            transform.position = new Vector2(-8, -5);
            print(health);
        }
    }
    void EnemyCheckVert(float yoffs)
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
            return;
        }

        Vector2 offset = new Vector2(0, yoffs);
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        Vector2 directionUp = Vector2.up;
        float distance = .8f;
        Color hitColor = Color.red;

        RaycastHit2D hit = Physics2D.Raycast(position + offset, direction, distance, enemyLayerMask);
        RaycastHit2D hitDown = Physics2D.Raycast(position + offset, directionUp, distance, enemyLayerMask);

        if (hit.collider != null || hitDown.collider != null)
        {
            health -= 1;
            print(health);
            delay = 2;
            hitColor = Color.green;
        }

        Debug.DrawRay(position + offset, Vector2.up * distance, hitColor);
        Debug.DrawRay(position + offset, Vector2.down * distance, hitColor);
    }

    void EnemyCheckHori(float yoffs)
    {

        if (delay > 0)
        {
            delay -= Time.deltaTime;
            return;
        }
        Vector2 directionLeft = Vector2.left;
        Vector2 directionRight = Vector2.right;
        float distance = .3f;
        Vector2 position = transform.position;
        Vector2 offset = new Vector2(0, yoffs);

        Color hitColor = Color.red;

        RaycastHit2D hitLeft = Physics2D.Raycast(position + offset, directionLeft, distance, enemyLayerMask);
        RaycastHit2D hitRight = Physics2D.Raycast(position + offset, directionRight, distance, enemyLayerMask);

        if (hitLeft.collider != null || hitRight.collider != null)
        {
            health -= 1;
            print(health);
            delay = 2;
            hitColor = Color.green;
        }
        Debug.DrawRay(position + offset, Vector2.left * distance, hitColor);
        Debug.DrawRay(position + offset, Vector2.right * distance, hitColor);
    }
    void HealthCheck()
    {
        if (health <= 0)
        {
            transform.position = new Vector2(-8, -5);
            health = 5;
        }
    }

}
