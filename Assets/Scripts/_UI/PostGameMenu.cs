using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PostGameMenu
    : MonoBehaviour
    , IObserverLevelEnd {
    public GameObject _PostGameMenuUI;
    public GameObject _PostGameFailure;
    public GameObject _PostGameSuccess;
    public GameObject _PostGameEndOfContent;
    public TextMeshProUGUI _FailureText;

    private int _LevelCount;
    private readonly string _FailureMoves = "Out of moves!";
    private readonly string _FailureShuffle = "Failed to shuffle";

    void Awake() {
        Publisher.Instance.Subscribe(this);

        _PostGameMenuUI.SetActive(false);
        _PostGameFailure.SetActive(false);
        _PostGameSuccess.SetActive(false);
        _PostGameEndOfContent.SetActive(false);
        LevelCollection levelCollection = SaveSystem.LoadLevels();
        _LevelCount = levelCollection.Count;
    }

    public void OnLevelEnd() {
        PostGameResults.EGameResult gameResult = PostGameResults.GetResult();

        _PostGameMenuUI.SetActive(true);
        _PostGameFailure.SetActive(false);
        _PostGameSuccess.SetActive(false);
        _PostGameEndOfContent.SetActive(false);

        UserData data = SaveSystem.LoadUserData();
        bool shuffleFailed = gameResult == PostGameResults.EGameResult.FailureShuffle;

        switch (gameResult) {
            case PostGameResults.EGameResult.Failure:
            case PostGameResults.EGameResult.FailureShuffle:
                _FailureText.text = shuffleFailed ? _FailureShuffle : _FailureMoves;
                _PostGameFailure.SetActive(true);
                break;
            case PostGameResults.EGameResult.Success:
                if (data._CurrentLevel == _LevelCount) {
                    _PostGameEndOfContent.SetActive(true);
                } else {
                    _PostGameSuccess.SetActive(true);
                }
                break;
        }
    }

    public void OnButtonReplayPressed() {
        LoadNextLevelScene();
    }

    public void OnButtonReplayEndOfContentPressed() {
        UserData data = SaveSystem.LoadUserData();
        data._CurrentLevel = 1;
        SaveSystem.SaveUserData(data);
        LoadNextLevelScene();
    }

    public void OnButtonNextPressed() {
        UserData data = SaveSystem.LoadUserData();
        ++data._CurrentLevel;
        SaveSystem.SaveUserData(data);
        LoadNextLevelScene();
    }

    public void OnButtonQuitPressed() {
        Application.Quit();
    }

    private void LoadNextLevelScene() {
        Publisher.Instance.NotifyAll(ESubjectTypes.UnloadScene);
        Publisher.Instance.UnsubscribeAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
