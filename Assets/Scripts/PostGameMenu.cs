using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PostGameMenu : MonoBehaviour {
    public GameObject _PostGameMenuUI;
    public GameObject _PostGameFailure;
    public GameObject _PostGameSuccess;
    public GameObject _PostGameEndOfContent;
    public TextMeshProUGUI _FailureText;

    private int _LevelCount;
    private readonly string _FailureMoves = "Out of moves!";
    private readonly string _FailureShuffle = "Failed to shuffle";

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
        bool shuffleFailed = gameResult == EndGameCondition.EGameResult.FailureShuffle;

        switch (gameResult) {
            case EndGameCondition.EGameResult.Failure:
            case EndGameCondition.EGameResult.FailureShuffle:
                _FailureText.text = shuffleFailed ? _FailureShuffle : _FailureMoves;
                _PostGameFailure.SetActive(true);
                break;
            case EndGameCondition.EGameResult.Success:
                if (data._CurrentLevel == _LevelCount) {
                    _PostGameEndOfContent.SetActive(true);
                } else {
                    _PostGameSuccess.SetActive(true);
                }
                break;
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
