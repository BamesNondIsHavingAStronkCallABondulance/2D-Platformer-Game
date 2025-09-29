using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Collectibles : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Do collision for 2D objects

        print("collectible has hit: " + other.gameObject.tag);


        Destroy(gameObject);
    }

}
