using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    // This variable will display the game over screen after player is killed
    [SerializeField] private GameObject gameOverPanel;


    // Displays the game over screen and also pause the game in the background
    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // This will restart the game after player hits restart button
    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Quits the game and loads the main menu screen
    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}

