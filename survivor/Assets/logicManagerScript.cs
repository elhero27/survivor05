using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class logicManagerScript : MonoBehaviour
{
    public float playerMaxHealth;
    public float playerHealth;
    public int playerLevel = 1;
    public int waveNumber = 0;

    public int numberOfEnemies = 0;
    public int numberOfKills = 0;
    public int highscore = 0;

    public Text playerHealthText;
    public Text playerLevelText;
    public Text waveNumberText;
    public Text numberOfEnemiesText;
    public Text numberOfKillsText;
    public Text highscoreText;
    public GameObject gameOverScreen;

    public pauseManager pauseManager;

    public void Start()
    {
        numberOfEnemies = 0;
        numberOfKills = 0;
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoreText.text = "Highscore: " + highscore.ToString();

    }

    public void Update()
    {
        refreshScore();
    }


    private void refreshScore()
    {
        numberOfEnemies = 0;
        numberOfEnemies += GameObject.FindGameObjectsWithTag("Enemy").Length;
        numberOfEnemiesText.text = "Kills to farm: " + numberOfEnemies.ToString();
    }

    public void addKillCount()
    {
        numberOfKills++;
        numberOfKillsText.text = "Kills farmed: " + numberOfKills.ToString();

        if (numberOfKills > highscore)
        {
            highscore = numberOfKills;
            PlayerPrefs.SetInt("Highscore", highscore);
            highscoreText.text = "Highscore: " + highscore.ToString();
        }
    }

    //[ContextMenu("IncreaseWaveNumber1")]
    public void increaseWaveNumber()
    {
        // Update variables
        waveNumber++;

        // Update UI Text
        waveNumberText.text = "Wave : " + waveNumber.ToString();
    }


    public void increasePlayerLevel()
    {
        // Update variables
        playerLevel++;

        // Update UI Text
        playerLevelText.text = "Level : " + playerLevel.ToString();
    }


    public void changePlayerHealth(float maxHealthChange = 1f, float healthChange = 1f)
    {

        playerMaxHealth += maxHealthChange;
        playerHealth += healthChange;

        playerHealthText.text = "Health : " + playerHealth.ToString() + " / " + playerMaxHealth.ToString();
    }

    public void setPlayerHealth(float maxHealthInput, float healthInput)
    {
        // Update variables
        playerMaxHealth = maxHealthInput;
        playerHealth = healthInput;

        // Update UI Text
        playerHealthText.text = "Health : " + playerHealth.ToString() + " / " + playerMaxHealth.ToString();

    }

public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pauseManager.pauseGame();
    }


    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        pauseManager.pauseGame();
    }
}
