using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private int totalPointsGained;

    private Powerup powerupScript;
    private PlayerController playerControllerScript;
    private GameManager gameManagerScript;

    private Rigidbody enemyRb;
    private GameObject player;

    public float speed;
    private float health;
    private int enemyPointValue;

    private float enemyDefaultSpeed = 50.0f;
    private int enemyDefaultHealth = 3;
    private int enemyDefaultPoints = 50;

    private float enemyFastSpeed = 65.0f;
    private int enemyFastHealth = 2;
    private int enemyFastPoints = 25;

    private float enemyStrongSpeed = 45.0f;
    private int enemyStrongHealth = 4;
    private int enemyStrongPoints = 75;
    public int enemyStrongHitPower = 2;

    private int hitCount;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        powerupScript = GameObject.Find("Powerup Empty Script Holder").GetComponent<Powerup>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        SetEnemyStats();
    }

    // Update is called once per frame
    void Update()
    {

        if(gameManagerScript.gameOver == false)
        {
            FollowPlayer();

            HitByBullet();

            LookAtPlayer();
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void FollowPlayer()
    {

        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
    }

    void LookAtPlayer()
    {
        transform.LookAt(player.transform);
    }

    void SetEnemyStats()
    {
        if (gameObject.CompareTag("Enemy_Default"))
        {
            speed = enemyDefaultSpeed;
            health = enemyDefaultHealth;
            enemyPointValue = enemyDefaultPoints;
        }
        else if (gameObject.CompareTag("Enemy_Fast"))
        {
            speed = enemyFastSpeed;
            health = enemyFastHealth;
            enemyPointValue = enemyFastPoints;
        }
        else if (gameObject.CompareTag("Enemy_Strong"))
        {
            speed = enemyStrongSpeed;
            health = enemyStrongHealth;
            enemyPointValue = enemyStrongPoints;
        }
    }

    void HitByBullet()
    {
        if(hitCount >= health)
        {
            if (!playerControllerScript.hasDoublePoints)
            {
                Destroy(gameObject);
                playerControllerScript.totalPoints += (enemyPointValue);
            }
            else
            {
                totalPointsGained = (enemyPointValue * powerupScript.doublePointsModifier);
                Destroy(gameObject);
                playerControllerScript.totalPoints += totalPointsGained;
            }
            
        }

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // powerModifier applied to the Player if hasPowerup is true
        if (collision.gameObject.CompareTag("Projectile") && playerControllerScript.hasPowerup == true)
        {
            hitCount += powerupScript.powerupModifier;
            Destroy(collision.gameObject);
            
        }
        else if(collision.gameObject.CompareTag("Projectile") && playerControllerScript.hasPowerup == false)
        {
            hitCount += playerControllerScript.gunBaseStrength;
            Destroy(collision.gameObject);
            
        }
        
        
    }
}
