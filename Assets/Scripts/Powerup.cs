using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    private ShootProjectile shootProjectileScript;
    private PlayerController playerControllerScript;

    Coroutine powerupCountdownRoutine;
    Coroutine doublePointsCountdownRoutine;

    private Vector3 rotation = new Vector3(0.0f, 30.0f, 0.0f);

    private int lifeupHealthModifier = 1;

    private int powerupDuration = 10;
    public int powerupModifier = 2;

    private int doublePointsDuration = 10;
    public int doublePointsModifier = 2;
   

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        shootProjectileScript = GameObject.Find("Aim_Indicator").GetComponent<ShootProjectile>();


    }

    // Update is called once per frame
    void Update()
    {
        RotatePickup();
    }
    
    void RotatePickup()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }

    // Increases total health value by lifeupHealthModifier (2)
    public void OnHealthPickup()
    {
      
            playerControllerScript.health += lifeupHealthModifier;
            playerControllerScript.playerAudio.PlayOneShot(playerControllerScript.healSound, 0.75f);
        

        
    }

    // Activates DoublePoints if hasDoublePoints is true by triggering a double points pickup
    public void DoublePointsActive()
    {
        playerControllerScript.hasDoublePoints = true;
        playerControllerScript.playerAudio.PlayOneShot(playerControllerScript.doublePointsSound);

        if (doublePointsCountdownRoutine != null)
        {
            StopCoroutine(doublePointsCountdownRoutine);

            StartCoroutine(DoublePointsCountdownRoutine());

            doublePointsCountdownRoutine = null;


        }
        else
        {
            
            playerControllerScript.doublePointsIndicator.SetActive(true);
            doublePointsCountdownRoutine = StartCoroutine(DoublePointsCountdownRoutine());

        }

        
    }

    // Activates Powerup if hasPowerup is true by triggering a powerup pickup
    public void PowerupActive()
    {
        playerControllerScript.hasPowerup = true;
        playerControllerScript.playerAudio.PlayOneShot(playerControllerScript.powerupSound, 0.75f);

        if (powerupCountdownRoutine != null)
        {

            StopCoroutine(powerupCountdownRoutine);

            StartCoroutine(PowerupCountdownRoutine());

            powerupCountdownRoutine = null;
        }
        else
        {

            shootProjectileScript.timeBetweenFiring = 0.1f;
            playerControllerScript.powerupIndicator.SetActive(true);
            powerupCountdownRoutine = StartCoroutine(PowerupCountdownRoutine());

        }

        
    }

    // Deactivates DoublePointsActive() after doublePointsDuration (10 secs)
    IEnumerator DoublePointsCountdownRoutine()
    {
        yield return new WaitForSeconds(doublePointsDuration);
        playerControllerScript.hasDoublePoints = false;
        playerControllerScript.doublePointsIndicator.SetActive(false);

        doublePointsCountdownRoutine = null;

    }

    // Deactivates PowerupActive() after powerupDuration (10 secs)
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerupDuration);
        playerControllerScript.hasPowerup = false;
        shootProjectileScript.timeBetweenFiring = 0.3f;
        playerControllerScript.powerupIndicator.SetActive(false);

        powerupCountdownRoutine = null;

    }

}
