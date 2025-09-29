using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    public LayerMask groundLayerMask;

    Rigidbody2D rb;

    float xvel, yvel;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        xvel = -1;

    }

    // Update is called once per frame
    void Update()
    {
        yvel = rb.linearVelocity.y;

        if (xvel < 0)
        {
            if(ExtendedRayCollisionCheck(-0.5f, 0) == false)
            {
                xvel = -xvel;
            }
        }
        else if (xvel > 0)
        {
            if (ExtendedRayCollisionCheck(0.5f, 0) == false)
            {
                xvel = -xvel;
            }
        }

        rb.linearVelocity = new Vector2(xvel, yvel);


    }

    public bool ExtendedRayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 0.8f; // length of raycast
        bool hitSomething = false;

        // convert x and y offset into a Vector3 
        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        //cast a ray downward 
        RaycastHit2D hit;


        hit = Physics2D.Raycast(transform.position + offset, Vector2.down, rayLength, groundLayerMask);

        Color hitColor = Color.white;


        if (hit.collider != null)
        {
            hitColor = Color.green;
            hitSomething = true;
        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, Vector3.down * rayLength, hitColor);

        return hitSomething;

    }

}
