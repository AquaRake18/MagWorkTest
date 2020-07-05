using UnityEngine;
using UnityEngine.SceneManagement;

public class PostGameMenu : MonoBehaviour {
    public GameObject _PostGameMenuUI;
    public GameObject _PostGameFailure;
    public GameObject _PostGameSuccess;
    public GameObject _PostGameEndOfContent;

    private int _LevelCount;

    void Awake() {
        _PostGameMenuUI.SetActive(false);
        _PostGameFailure.SetActive(false);
        _PostGameSuccess.SetActive(false);
        _PostGameEndOfContent.SetActive(false);
        LevelCollection levelCollection = SaveSystem.LoadLevels();
        _LevelCount = levelCollection.Count;
    }

    public void OpenPostGameMenu(EndGameCondition.EGameResult gameResult) {
        _PostGameMenuUI.SetActive(true);
        _PostGameFailure.SetActive(false);
        _PostGameSuccess.SetActive(false);
        _PostGameEndOfContent.SetActive(false);

        UserData data = SaveSystem.LoadUserData();

        if (gameResult == EndGameCondition.EGameResult.Failure) {
            _PostGameFailure.SetActive(true);
        } else if (gameResult == EndGameCondition.EGameResult.Success) {
            if (data._CurrentLevel == _LevelCount) {
                _PostGameEndOfContent.SetActive(true);
            } else {
                _PostGameSuccess.SetActive(true);
            }
        }
    }

    public void OnButtonReplayPressed() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnButtonReplayEndOfContentPressed() {
        UserData data = SaveSystem.LoadUserData();
        data._CurrentLevel = 1;
        SaveSystem.SaveUserData(data);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnButtonNextPressed() {
        UserData data = SaveSystem.LoadUserData();
        ++data._CurrentLevel;
        SaveSystem.SaveUserData(data);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnButtonQuitPressed() {
        Application.Quit();
    }
}
