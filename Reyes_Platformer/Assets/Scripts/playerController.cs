using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody2D myRB;
    public Vector2 velocity;
    public Vector2 respawnPos;
    private Quaternion zero;
    public GameObject shamac;
    public GameManager gm;

    private float timeDifference;
    private float timeDifference2;
    public int health = 3;
    public float speed = 5;
    public float jumpHeight = 6.25f;
    public bool isGrounded = true;
    public float shamacCooldown;
    public bool shamacEffect = false; //when we make the enemy script will tell enemies to either see or not see us
    public float shamacDuration;
    public float groundDetectDistance = .1f;
    public Vector2 groundDetection;
    public float shamacTimestamp = 0.0f;
    public float shamacDurTimestamp;



    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        respawnPos = new Vector2(0,0);
        zero = new Quaternion();

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        groundDetection.x = transform.position.x;
        groundDetection.y = transform.position.y - 1f;
        if (shamacTimestamp <= Time.time)
        {
            if (Input.GetKeyDown(KeyCode.Z) && Physics2D.Raycast(groundDetection, Vector2.down, groundDetectDistance))
            {
                GameObject s = Instantiate(shamac, new Vector2(transform.position.x, transform.position.y), transform.rotation);

                Destroy(s, 1);
                shamacTimestamp = Time.time + shamacCooldown;
                shamacDurTimestamp = Time.time + shamacDuration;

                shamacEffect = true;
                GetComponent<SpriteRenderer>().color = UnityEngine.Color.magenta;
            }
        }
        
        
        if (shamacDurTimestamp <= Time.time)
        {
            shamacEffect = false;
            GetComponent<SpriteRenderer>().color = UnityEngine.Color.white;
        }

        if (health <= 0)
        {
            transform.SetPositionAndRotation(respawnPos, zero);
            health = 3;
        }
        velocity = myRB.velocity;

        
        velocity.x = Input.GetAxisRaw("Horizontal") * speed;

        Debug.DrawRay(groundDetection, Vector2.down, UnityEngine.Color.white);

        if (Input.GetKeyDown(KeyCode.Space) && Physics2D.Raycast(groundDetection, Vector2.down, groundDetectDistance))
        {
            velocity.y = jumpHeight;
        }


        myRB.velocity = velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Door")
        {
            gm.GameEnd = true;
            Time.timeScale = .5f;
        }

        if (collision.gameObject.name.Contains("checkpoint"))
        {
            respawnPos = collision.gameObject.transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D Collision)
    {
        if (Collision.gameObject.name.Contains("enemy"))
        {
            health--;
        }
    }

   


        
}
