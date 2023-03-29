using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class upgradeChoiceManager : MonoBehaviour
{
    public pauseManager pauseManager;
    public player_movement player;

    public GameObject pickUpgradeScreen;
    public Text choice0Text;
    public Text choice1Text;
    public Text choice2Text;

    public static int[] upgradePicks;

    public void pickUpgrade()
    {
        pauseManager = GameObject.FindGameObjectWithTag("PauseManager").GetComponent<pauseManager>();
        pauseManager.pauseGame(); 
        upgradePicks = new int[] { -1, -1, -1 };

        // pick 3 random numbers for upgrades
        for (int k = 0; k < 3; k++)
        {
            int newPick = UnityEngine.Random.Range(0, 4);
            while (Array.Exists(upgradePicks, element => element == newPick))
            {
                newPick = UnityEngine.Random.Range(0, 4);
            }
            upgradePicks[k] = newPick;
        }

        // set button text corresponding to upgradePicks content
        choice0Text.text = player.getPlayerAttribute(upgradePicks[0]);
        choice1Text.text = player.getPlayerAttribute(upgradePicks[1]);
        choice2Text.text = player.getPlayerAttribute(upgradePicks[2]);
        pickUpgradeScreen.SetActive(true);

    }

    public void exitUpgradeScreen()
    {

        pickUpgradeScreen = GameObject.FindGameObjectWithTag("UpgradeScreen");
        pauseManager = GameObject.FindGameObjectWithTag("PauseManager").GetComponent<pauseManager>();
        // Continue Game
        pickUpgradeScreen.SetActive(false);
        pauseManager.pauseGame();
    }


    public void pickUpgrade(int choiceNumberIn)
    {
        int chosenUpgrade = upgradePicks[choiceNumberIn];
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player_movement>();

        switch (chosenUpgrade)
        {
            case 0:
                player.increaseStats(5f, 0, 0, 0);
                break;

            case 1:
                player.increaseStats(0, 0.6f, 0, 0);
                break;

            case 2:
                player.increaseStats(0, 0, 2.25f, 0);
                break;

            case 3:
                player.increaseStats(0, 0, 0, 0.35f);
                break;

            default:
                break;

        }
    }

    
}
