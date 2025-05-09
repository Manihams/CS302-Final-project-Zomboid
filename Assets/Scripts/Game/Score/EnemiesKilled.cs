using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class EnemiesKilled : MonoBehaviour
{
    // Allows global access to track the score
    public static EnemiesKilled Instance;

    [SerializeField] private TextMeshProUGUI scoreText; // Assign in the Inspector

    // Stores current score
    private int _score = 0;

    // Reference to playerPowerUp which boosts player's health after killing zombies
    private PlayerPowerUp _playerPowerUp;

    private void Awake()
    {
        // Ensure only one instance of this script exists (Singleton pattern)
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Calls the PlayerPowerUp for storing kills
        _playerPowerUp = FindObjectOfType<PlayerPowerUp>();
    }

    // This method us used to increase the score
    public void AddScore(int amount)
    {
        _score += amount;
        UpdateScoreUI();

        // Increases player's health every 5 kills
        if (_score % 5 == 0 && _playerPowerUp != null)
        {
            _playerPowerUp.RegisterKill();
        }
    }

    // Updates the TextMeshPro text field with the current score
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "KILLS: " + _score;
        }
    }

    // Get current score
    public int GetScore()
    {
        return _score;
    }
}



