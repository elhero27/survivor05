using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class logicManagerScript : MonoBehaviour
{
    public float playerMaxHealth;
    public float playerHealth;
    public int playerLevel = 1;
    public int waveNumber = 0;


    public Text playerHealthText;
    public Text playerLevelText;
    public Text waveNumberText;


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


    public void changePlayerHealth(float maxHealthChange=1f, float healthChange=1f)
    {
        // Update variables
        playerMaxHealth += maxHealthChange;
        playerHealth += healthChange;

        // Update UI Text
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
}
