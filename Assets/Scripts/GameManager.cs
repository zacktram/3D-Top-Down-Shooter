using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private ShootProjectile shootProjectileScript;
    private PlayerController playerControllerScript;
    private SpawnManager spawnManagerScript;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI enemyCountText;
    public TextMeshProUGUI waveNumberText;
    public TextMeshProUGUI healthText;

    public Button restartButton;
    public GameObject gameOverScreen;

    public GameObject titleScreen;
    public Button startButton;

    public bool gameOver;


    private void Awake()
    {
        gameOver = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        shootProjectileScript = GameObject.Find("Aim_Indicator").GetComponent<ShootProjectile>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        UpdateHealth();
        UpdateWaveNumber();
        UpdateEnemyCount();
        GameOver();
        CloseWindow();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + playerControllerScript.totalPoints;
    }

    void UpdateWaveNumber()
    {
        waveNumberText.text = "Wave: " + spawnManagerScript.waveNumber;
    }

    void UpdateHealth()
    {
        healthText.text = "Health: " + playerControllerScript.health;
    }

    void UpdateEnemyCount()
    {
        enemyCountText.text = "Enemies: " + spawnManagerScript.enemyCount;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GameOver()
    {
        if (playerControllerScript.health == 0 && !gameOver)
        {
            gameOver = true;
            shootProjectileScript.canFire = false;
            playerControllerScript.playerAudio.PlayOneShot(playerControllerScript.deathSound, 0.6f);
            gameOverScreen.SetActive(true);        
        }
    }

    public void StartGame()
    {
        gameOver = false;
        titleScreen.SetActive(false);
    }

    void CloseWindow()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


}
