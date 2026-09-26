using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("HomeScreen");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
