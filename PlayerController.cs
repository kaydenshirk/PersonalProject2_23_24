using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 15.0f;
    private float horizontalInput;
    private Rigidbody rb;
    private Vector3 forceDirection;
    private ConstantForce cForce;

    public float projectileSpeed = 10f;
    public float lowerBound = 0.0f;
    public float upperbound = 30.0f;
    public float jumpForce;
    public bool isOnGround = true;
    public bool GameOver = false;
    public bool CanGlide = true;
    private bool isGliding = false;
    public int eggStorage = 0;
    public int maxEggStorage = 10;
    public GameObject eggProjectilePrefab;
    public AudioSource eggCollectSound;
    public AudioSource Elytra;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cForce = GetComponent<ConstantForce>();
        eggCollectSound = GetComponent<AudioSource>();
        Elytra = GetComponent<AudioSource>();
        Player = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && eggStorage > 0)
        {
            GameObject eggProjectile = Instantiate(eggProjectilePrefab, Player.position, Player.rotation);
            
            eggProjectile.transform.parent = transform;
            
            Rigidbody projectileRigidbody = eggProjectile.GetComponent<Rigidbody>();
            projectileRigidbody.velocity = -transform.up * projectileSpeed;

            eggStorage--;
        } 

            // Left & right input
            float horizontalInput = Input.GetAxis("Horizontal");

            // Calculate movement direction
                Vector3 movement = new Vector3(0f, 0f, horizontalInput); // Adjust the movement vector

                // Move the player
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, movement.z * speed); // Adjust the velocity

        // Rotate the player based on movement direction
        if (horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(-90f, 0f, 0f);  // Rotate right
        }
        else if (horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(-90f, 0f, -180f);  // Rotate left
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            rb.drag = 0;
            speed = 15.0f;
        }

        // Gliding
        if (CanGlide && Input.GetKeyUp(KeyCode.Space))
        {
            rb.drag = 10;
            speed = 25.0f;
            isGliding = true;
            Elytra.Play();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGliding)
        {
            isGliding = false;
            rb.drag = 0;
            Elytra.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Keeps the player from infinitely jumping
        if (collision.gameObject.CompareTag("ground"))
        {
            isOnGround = true;
            isGliding = false;
            Elytra.Stop();
        }

        if (collision.gameObject.CompareTag("enemy"))
        {
            //write damage code here
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("elytra"))
        {
            CanGlide = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Nest"))
        {
            Debug.Log("level complete");
            Destroy(other.gameObject);
        }

        if (transform.position.y > upperbound)
        {
            transform.position = new Vector3(transform.position.x, upperbound, transform.position.z);
        }

         if (other.CompareTag("egg"))
        {
            eggStorage += 10;
            Destroy(other.gameObject);
            eggCollectSound.Play();
            return;
        }
    }
    //spawns projectile
    private void SpawnEggProjectile()
    {
    GameObject egg = Instantiate(eggProjectilePrefab, transform.position, Quaternion.identity);
    }
}