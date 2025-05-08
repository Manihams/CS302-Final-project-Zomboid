using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class EnemiesKilled : MonoBehaviour
{
    public static EnemiesKilled Instance;

    [SerializeField] private TextMeshProUGUI scoreText; // Assign in the Inspector

    private int _score = 0;

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

        _playerPowerUp = FindObjectOfType<PlayerPowerUp>();
    }

    // Call this method to increase the score
    public void AddScore(int amount)
    {
        _score += amount;
        UpdateScoreUI();

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



