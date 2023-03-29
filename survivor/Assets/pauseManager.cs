using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseManager : MonoBehaviour
{
    public static bool isPaused;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }

    }

    public void pauseGame()
    {
        // use unscaled time Time.unscaledDeltaTime for actions during pause
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

    }
}
