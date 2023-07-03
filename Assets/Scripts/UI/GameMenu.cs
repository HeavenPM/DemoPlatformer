using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _gameOverPanel;

    private bool _isPausePanelVisible = false;
    private bool _isGameOverPanelVisible = false;

    private void Update()
    {
        _pausePanel.SetActive(_isPausePanelVisible);
        _gameOverPanel.SetActive(_isGameOverPanelVisible);

        if (Input.GetKeyDown(KeyCode.Escape) && !_isGameOverPanelVisible) TogglePausePanelState();
        if (!_isPausePanelVisible && !_isGameOverPanelVisible) Time.timeScale = 1;
    }

    public void TogglePausePanelState()
    {
        _isPausePanelVisible = !_isPausePanelVisible;
        Time.timeScale = _isPausePanelVisible ? 0 : 1;
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void OnEnable()
    {
        EventManager.GameOver += EventGameOver;
    }

    private void OnDisable()
    {
        EventManager.GameOver -= EventGameOver;
    }

    private void EventGameOver()
    {
        _isGameOverPanelVisible = true;
        Time.timeScale = 0;
    }
}
