using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform endLevel;

    [SerializeField] private GameObject pausePanel;

    private bool isPause = false;

    [Header("EndLevel")]
    public GameObject endLevelPanel;
    public GameObject win;
    public GameObject loose;
    public TextMeshProUGUI percentageValue;

    private void Awake()
    {
        instance = this;
        Resume();
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        isPause = true;
        Time.timeScale = 0.0f;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        isPause = false;
        Time.timeScale = 1.0f;
    }

    public void BackMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnEscape(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            if(!isPause)
                Pause();
            else
                Resume();
        }
    }

    public void EndLevel(bool levelWin, int percentage)
    {
        Time.timeScale = 0.0f;

        endLevelPanel.SetActive(true);
        if (levelWin)
            win.SetActive(true);
        else
            loose.SetActive(true);

        percentageValue.text = percentage + "%";
    }

    public void Retry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
