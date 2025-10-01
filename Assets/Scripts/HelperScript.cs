using System.ComponentModel;
using UnityEngine;

public class HelperScript : MonoBehaviour
{
    public void FlipObject( bool flip)
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

        if (flip == true)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    public void Hello(bool hello)
    {
        if (Input.GetKeyDown("h") == true)
        {
            print("hello world");
            hello = true;
        }
        else
        {
            hello = false;
        }
    }
}
