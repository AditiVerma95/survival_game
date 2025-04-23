using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject frontScreen;
    public GameObject controlsScreen;
    public GameObject howToPlayScreen;

    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void EnableControlsScreen() {
        DisableEverything();
        controlsScreen.SetActive(true);
    }
    
    public void EnableFrontScreen() {
        DisableEverything();
        frontScreen.SetActive(true);
    }

    public void EnableHowToPlay() {
        DisableEverything();
        howToPlayScreen.SetActive(true);
    }

    public void GoToTestScene() {
        SceneManager.LoadScene(2);
    }


    public void DisableEverything() {
        howToPlayScreen.SetActive(false);
        frontScreen.SetActive(false);
        controlsScreen.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }
    
}
