using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void PlayGame() {
        UserData data = SaveSystem.LoadUserData();
        if (data == null) {
            data = new UserData(1);
            SaveSystem.SaveUserData(data);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
