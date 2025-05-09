using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    // Just loading the scene with build index 1
    public void play()
    {
        SceneManager.LoadScene(1);
    }
}
