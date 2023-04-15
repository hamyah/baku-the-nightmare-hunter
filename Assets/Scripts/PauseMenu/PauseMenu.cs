using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    

    void Start()
    {
        HidePauseScreen();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ShowPauseScreen();
    }

    public void ShowPauseScreen()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void HidePauseScreen()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnReturnPressed()
    {
        HidePauseScreen();
    }
    
    public void OnQuitPressed()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
