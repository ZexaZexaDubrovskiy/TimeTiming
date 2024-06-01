using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private TextMeshProUGUI _textScoreMeshPro, _textBestScoreMeshPro;
    private int _scorePlayer;
    private int _BestScorePLayer;

    public int ScorePlayer
    {
        get { return _scorePlayer; }
        set { _scorePlayer = value; }
    }
    public int BestScorePlayer
    {
        get { return _BestScorePLayer; }
        set { _BestScorePLayer = value; }
    }

    public void UpdateScore(int value)
    {
        ScorePlayer += value;
        UpdateScorePlayerText(ref _textScoreMeshPro, ScorePlayer);
    }

    private void UpdateBestScore()
    {
        BestScorePlayer = ScorePlayer > BestScorePlayer ? ScorePlayer : BestScorePlayer;
        UpdateScorePlayerText(ref _textBestScoreMeshPro, BestScorePlayer);
    }

    public void ResetScore()
    {
        UpdateBestScore();
        UpdateScore(-ScorePlayer);
    }

    private void UpdateScorePlayerText(ref TextMeshProUGUI textScore, int value)
    {
        textScore.text = value.ToString();
    }
}
