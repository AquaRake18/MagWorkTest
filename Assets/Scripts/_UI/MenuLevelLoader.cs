using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuLevelLoader : MonoBehaviour {
    public TextMeshProUGUI _SliderText;
    public Slider _LevelSlider;

    private LevelCollection _LevelCollection;
    private int _SelectedLevel;

    void Awake() {
        _SelectedLevel = 1;
        _LevelCollection = SaveSystem.LoadLevels();
        int levelCount = _LevelCollection.Count;
        if (levelCount <= 1) {
            _LevelSlider.gameObject.SetActive(false);
        } else {
            _LevelSlider.minValue = 1;
            _LevelSlider.maxValue = levelCount;
        }
    }

    public void UpdateText(float value) {
        _SelectedLevel = Mathf.RoundToInt(value);
        _SliderText.text = "Lv. " + _SelectedLevel;
    }

    public void GoLoad() {
        SaveSystem.SaveUserData(new UserData(_SelectedLevel));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
