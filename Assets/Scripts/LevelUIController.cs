using UnityEngine;

public class LevelUIController : MonoBehaviour {
    public GameObject _PostGameMenuUI;
    public GameObject _PostGameFailure;
    public GameObject _PostGameSuccess;
    public GameObject _PostGameEndOfContent;

    void Awake() {
        _PostGameMenuUI.SetActive(false);
        _PostGameFailure.SetActive(false);
        _PostGameSuccess.SetActive(false);
        _PostGameEndOfContent.SetActive(false);
    }

    public void OpenPostGameMenu(EndGameCondition.EGameResult gameResult) {
        _PostGameMenuUI.SetActive(true);
        _PostGameFailure.SetActive(false);
        _PostGameSuccess.SetActive(false);
        _PostGameEndOfContent.SetActive(false);
        if (gameResult == EndGameCondition.EGameResult.Failure) {
            _PostGameFailure.SetActive(true);
        } else if (gameResult == EndGameCondition.EGameResult.Success) {
            _PostGameSuccess.SetActive(true);
        } else if (gameResult == EndGameCondition.EGameResult.EndOfContent) {
            _PostGameEndOfContent.SetActive(true);
        }
    }

    public void OnButtonReplayPressed() {
    }

    public void OnButtonReplayEndOfContentPressed() {
    }

    public void OnButtonNextPressed() {
    }

    public void OnButtonQuitPressed() {
        Application.Quit();
    }
}
