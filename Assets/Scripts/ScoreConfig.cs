using UnityEngine;
using TMPro;

public class ScoreConfig : MonoBehaviour {
    public int _BasicScore = 200;
    public int _BonusScorePerTier = 50;
    public int _LinksPerTier = 3;
    private int _TargetScore = 9999;
    private int _CurrentScore = 0;
    [Space(14)]
    public TextMeshProUGUI _CurrentScoreText;
    public TextMeshProUGUI _TargetScoreText;

    void Awake() {
        _TargetScore = gameObject.GetComponent<LevelSettings>()._ClearScore;
        _CurrentScore = 0;
        _CurrentScoreText.text = "" + _CurrentScore;
        _TargetScoreText.text = "" + _TargetScore;
    }

    public void AddScore(int linkLength) {
        int linkInTier = 0;
        int currentTier = 0;
        int newScore = 0;
        for (int links = 0; links < linkLength; ++links) {
            newScore += (_BasicScore + currentTier * _BonusScorePerTier);
            ++linkInTier;
            if (linkInTier == _LinksPerTier) {
                ++currentTier;
                linkInTier = 0;
            }
        }
        _CurrentScore += newScore;
        _CurrentScoreText.text = "" + _CurrentScore;
    }
}
