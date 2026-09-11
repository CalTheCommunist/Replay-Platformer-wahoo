using UnityEngine;
using UnityEngine.SceneManagement;

public class NotSoSecretButtonStuff : MonoBehaviour
{
    public void ToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void EndGame()
    {
        Application.Quit();
    }
}
