using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private GameManager gameManagerScript;
    public GameObject aimIndicator;
    public GameObject bulletPrefab;

    public Transform bulletSpawnPoint;

    public bool canFire = true;

    public float timeBetweenFiring = 0.3f;
    private float timer;

    // Start is called before the first frame update
    void Awake()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootBullet();
    }

    public void ShootBullet()
    {
    
        if (!canFire)
        {
            // timer increases as Time progresses in the game
            timer += Time.deltaTime;

            // Prevents the Player from spamming the Shoot button
            if(timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }


        if (Input.GetKeyDown(KeyCode.Mouse0) && canFire && !gameManagerScript.gameOver)
        {
            canFire = false;
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            playerControllerScript.playerAudio.PlayOneShot(playerControllerScript.gunShotSound, 0.35f);

        }
    }
}
