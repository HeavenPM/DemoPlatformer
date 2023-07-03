using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(PlayerPrefs.HasKey("Last completed level") ?
            PlayerPrefs.GetInt("Last completed level") : 1);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
        #else
                Application.Quit();
        #endif
    }
}
