using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class logicScript : MonoBehaviour
{
    public float playerMaxHealth;
    public float playerHealth;
    public int playerLevel = 1;
    public int waveNumber = 0;


    public Text playerHealthText;
    public Text playerLevelText;
    public Text waveNumberText;


    [ContextMenu("IncreaseWaveNumber1")]
    public void increaseWaveNumber()
    {
        waveNumber++;
        waveNumberText.text = "Wave : " + waveNumber.ToString();
    }


    [ContextMenu("IncreaseWaveNumber2")]
    public void increasePlayerLevel()
    {
        playerLevel++;
        playerLevelText.text = "Level : " + playerLevel.ToString();
    }


    [ContextMenu("IncreaseWaveNumber3")]
    public void changePlayerHealth(float maxHealthChange=1f, float healthChange=1f)
    {

            playerMaxHealth += maxHealthChange;


            playerHealth += healthChange;

        Debug.Log(healthChange);

            if (playerHealth >= playerMaxHealth)
            {
                playerHealth = playerMaxHealth;
            }

        playerHealthText.text = "Health : " + playerHealth.ToString() + " / " + playerMaxHealth.ToString();
    }
}
