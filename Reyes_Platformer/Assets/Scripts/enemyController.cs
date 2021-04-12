using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    private Rigidbody2D myRB;
    public GameObject playerTarget;
    public float movementSpeed = 3;
    public bool isFollowing = false;
    public bool spikey = false;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        playerTarget = GameObject.Find("player");
        playerController playerController = playerTarget.GetComponent<playerController>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookPos = playerTarget.transform.position - transform.position;
        //float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        //myRB.rotation = angle;
        lookPos.Normalize();

        //if (isFollowing)
        //{
        //myRB.velocity = new Vector2(lookPos.x * movementSpeed, 0);

        // Checking to see if we're moving to the right
        //if (myRB.velocity.x > 0)
        //GetComponent<SpriteRenderer>().flipX = false;

        // Checking to see if we're moving to the left
        //else if (myRB.velocity.x < 0)
        //GetComponent<SpriteRenderer>().flipX = true;
        //}

        //else if(isFollowing == false | playerTarget.GetComponent<playerController>().shamacEffect == false)
        //{
        //myRB.velocity = new Vector2(0, 0);
        //}
        if (spikey)
        {
            Destroy(GetComponent<PolygonCollider2D>());
            anim.Play("Spike");
            gameObject.AddComponent<PolygonCollider2D>();
        }
        else if (!spikey)
        {
            Destroy(GetComponent<PolygonCollider2D>());
            anim.Play("idleSpike");
            gameObject.AddComponent<PolygonCollider2D>();

        }

            if (playerTarget.GetComponent<playerController>().shamacEffect == true)
            spikey = false;
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (!isFollowing && (collision.gameObject.name == "player") && playerTarget.GetComponent<playerController>().shamacEffect == false)
        //{
            //isFollowing = true;
        //}
        if((collision.gameObject.name == "player") && playerTarget.GetComponent<playerController>().shamacEffect == false)
        {
            spikey = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (isFollowing && (collision.gameObject.name == "player"))
        //{
            //isFollowing = false;
        //}
        if(spikey && (collision.gameObject.name == "player"))
        {
            spikey = false;
        }
    }
}
