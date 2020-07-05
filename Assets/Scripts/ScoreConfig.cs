using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreConfig : MonoBehaviour {
    public int _BasicScore = 200;
    public int _BonusScorePerTier = 50;
    public int _LinksPerTier = 3;
    [Space(14)]
    public TextMeshProUGUI _MovesText;
    public TextMeshProUGUI _CurrentScoreText;
    public TextMeshProUGUI _TargetScoreText;
    public RectTransform _ScrollingTextParent;
    public GameObject _ScrollingTextPrefab;

    private int _TargetScore = 9999;
    private LevelProgress _LevelProgress;

    public void Initialize(LevelSettings settings, LevelProgress levelProgress) {
        _TargetScore = settings.TargetScore;
        _LevelProgress = levelProgress;
        _MovesText.text = "" + _LevelProgress.MovesLeft;
        _CurrentScoreText.text = "" + _LevelProgress.CurrentScore;
        _TargetScoreText.text = "" + _TargetScore;
    }

    public void AddMovesLeft(int moves) {
        _LevelProgress.MovesLeft += moves;
        _MovesText.text = "" + _LevelProgress.MovesLeft;
    }

    public void AddScore(List<Vector3> positions) {
        int linkInTier = 0;
        int currentTier = 0;
        int newScore = 0;
        for (int links = 0; links < positions.Count; ++links) {
            int score = (_BasicScore + currentTier * _BonusScorePerTier);
            //AddScrollingText(score, positions[links]);
            newScore += score;
            ++linkInTier;
            if (linkInTier == _LinksPerTier) {
                ++currentTier;
                linkInTier = 0;
            }
        }
        _LevelProgress.CurrentScore += newScore;
        _CurrentScoreText.text = "" + _LevelProgress.CurrentScore;
    }

    public void AddScrollingText(int score, Vector3 position) {
        Vector2 viewport = Camera.main.WorldToViewportPoint(position);
        Vector2 screenPosition = new Vector2(
            viewport.x * _ScrollingTextParent.sizeDelta.x,
            viewport.y * _ScrollingTextParent.sizeDelta.y
        );
        screenPosition.y = -screenPosition.y;

        GameObject go = Instantiate(
            _ScrollingTextPrefab,
            screenPosition,
            Quaternion.identity
        );
        go.transform.SetParent(_ScrollingTextParent);
        go.GetComponent<TextMeshProUGUI>().text = "" + score;
    }
}
