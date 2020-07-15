using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreConfig : Singleton<ScoreConfig> {
    // Singleton
    protected ScoreConfig() {}

    public int _BasicScore = 200;
    public int _BonusScorePerTier = 50;
    public int _LinksPerTier = 3;
    [Space(14)]
    public TextMeshProUGUI _MovesText;
    public TextMeshProUGUI _CurrentScoreText;
    public TextMeshProUGUI _TargetScoreText;
    public RectTransform _ScrollingTextParent;
    public GameObject _ScrollingTextPrefab;

    private Animation _MovesBounceAnimation;

    void Awake() {
        _MovesBounceAnimation = _MovesText.gameObject.GetComponent<Animation>();
    }

    public void Initialize() {
        _MovesText.text = "" + LevelProgress._MovesLeft;
        _CurrentScoreText.text = "" + LevelProgress._CurrentScore;
        _TargetScoreText.text = "" + LevelSettings.Instance.TargetScore;
    }

    public void AddMovesLeft(int moves) {
        LevelProgress._MovesLeft += moves;
        _MovesText.text = "" + LevelProgress._MovesLeft;
        _MovesBounceAnimation.Play("NumberBounce");
        if (LevelProgress._MovesLeft <= 0) {
            Publisher.Instance.NotifyAll(ESubjectTypes.LevelEnd);
        }
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
        LevelProgress._CurrentScore += newScore;
        _CurrentScoreText.text = "" + LevelProgress._CurrentScore;
        if (LevelProgress._CurrentScore >= LevelSettings.Instance.TargetScore) {
            Publisher.Instance.NotifyAll(ESubjectTypes.LevelEnd);
        }
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
