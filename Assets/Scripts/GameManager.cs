using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI waveText;

    public int maxWaves = 10;

    public bool gameEnded = false;


    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }



    public void SetWave(int currentWave)
    {
        if (waveText != null)
        {
            waveText.text =
                "Wave " +
                currentWave +
                "/" +
                maxWaves;
        }
    }



    public void WinGame()
    {
        if (gameEnded)
            return;


        gameEnded = true;


        if (waveText != null)
        {
            waveText.text = "YOU WIN (Press R to restart.)";
        }


        Time.timeScale = 0f;
    }



    public void LoseGame()
    {
        if (gameEnded)
            return;


        gameEnded = true;


        if (waveText != null)
        {
            waveText.text = "YOU LOSE (Press R to restart.)";
        }


        Time.timeScale = 0f;
    }



    void RestartGame()
    {
        Time.timeScale = 1f;


        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}