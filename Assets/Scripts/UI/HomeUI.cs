using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUI : MonoBehaviour
{

    public void GoToDetailCharacter()
    {
        SceneManager.LoadScene("Detail Char");
    }

    public void GoToCollection()
    {
        SceneManager.LoadScene("Dress Collection Room");
    }

    public void GoToStage()
    {
        SceneManager.LoadScene("Stage");
    }
        
}
