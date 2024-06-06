using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private TextMeshProUGUI _textScoreMeshPro, _textBestScoreMeshPro;
    private int _scorePlayer;
    private int _BestScorePLayer;
    private const string BestScoreKey = "BestScore";

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

    private void Awake()
    {
        LoadBestScore();
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
        if (textScore == _textBestScoreMeshPro)
        {
            textScore.text = "BEST SCORE:\n" + value.ToString();
            SaveBestScore();
        }
        else textScore.text = value.ToString();
    }

    private void SaveBestScore()
    {
        PlayerPrefs.SetInt(BestScoreKey, BestScorePlayer);
        PlayerPrefs.Save();
    }

    private void LoadBestScore()
    {
        BestScorePlayer = PlayerPrefs.GetInt(BestScoreKey, 0);
        UpdateScorePlayerText(ref _textBestScoreMeshPro, BestScorePlayer);
    }
}
