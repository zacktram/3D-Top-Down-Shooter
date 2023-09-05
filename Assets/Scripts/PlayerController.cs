using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManagerScript;
    private Powerup powerupScript;

    private Camera mainCamera;
    private Rigidbody playerRb;

    public AudioSource playerAudio;
    public AudioClip healSound;
    public AudioClip powerupSound;
    public AudioClip doublePointsSound;
    public AudioClip gunShotSound;
    public AudioClip deathSound;
    public AudioClip[] hitSounds;

    public GameObject powerupIndicator;
    public GameObject doublePointsIndicator;

    private float speed = 80.0f;
    public float health = 8.0f;
    public int gunBaseStrength = 1;
    public int totalPoints;

    private float zBound = 15.5f;
    private float xBound = 27.75f;
    
    public bool hasPowerup = false;

    public bool hasDoublePoints = false;

    public void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        playerAudio = GetComponent<AudioSource>();

        powerupScript = GameObject.Find("Powerup Empty Script Holder").GetComponent<Powerup>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
       
    }

    // Update is called once per frame
    void Update()
    {

        if(gameManagerScript.gameOver == false)
        {
            MovePlayer();
            RotatePlayer();
        }
       
        KeepPlayerInBounds();

    }

    // Moves Player based on WASD Input
    void MovePlayer()
    {
       
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            playerRb.AddForce(Vector3.forward * verticalInput * speed);
            playerRb.AddForce(Vector3.right * horizontalInput * speed);
        
        
    }

    // Prevents Player from leaving the Play Area
    void KeepPlayerInBounds()
    {
        if(transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }

        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }

        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
    }

    // Rotates the Player where the Player faces towards the Mouse Cursor
    void RotatePlayer()
    {
        // cameraRay stores the line from the camera to where the mouse cursor is on screen by ScreenPointToRay()
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Need cameraRay to stop at the ground, so groundPlane is used to determine where the ground is
        // Vector3.up b/c we need the Y-axis, and Vector3.zero equals zero
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        // Stores the length of the new line we created
        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    void PlayRandomHitSound()
    {
        int hitIndex = Random.Range(0, hitSounds.Length);
        playerAudio.PlayOneShot(hitSounds[hitIndex], 0.8f); ;

    }

    // Handles powerup pickups
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Life Up(Clone)")
        {
            powerupScript.OnHealthPickup();
            Destroy(other.gameObject);
            
        }

     
        if(other.gameObject.name == "Power Up(Clone)")
        {
            powerupScript.PowerupActive();
            Destroy(other.gameObject);
        }

        if(other.gameObject.name == "Double Points(Clone)")
        {
            powerupScript.DoublePointsActive();
            Destroy(other.gameObject);
        }
    
    }


    private void OnCollisionEnter(Collision collision)
    {
        // Enemy Hits Player
        if(collision.gameObject.CompareTag("Enemy_Default"))
        {
            health--;

            if (health > 0)
            {
                PlayRandomHitSound();
            }
        }

        if(collision.gameObject.CompareTag("Enemy_Fast"))
        {
            health--;

            if (health > 0)
            {
                PlayRandomHitSound();
            }
        }

        if(collision.gameObject.CompareTag("Enemy_Strong"))
        {
            health--;

            if(health > 0)
            {
                PlayRandomHitSound();
            }
            
        }
    }
  

    

}
