using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    // An array of TextMeshProUGUI components where you want to display the best scores
    [SerializeField] TextMeshProUGUI[] bestScoresText;

    [SerializeField] TextMeshProUGUI[] bestPlayerNameScoresText;
    ScoreKeeper _scoreKeeper;
    
    void Awake()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        scoreText.text = _scoreKeeper.GetScore().ToString();
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        // Retrieve the best scores from the ScoreKeeper class
        ScoreEntry[] bestScores = _scoreKeeper.GetBestScores();
        var bestScoresLength = bestScores.Length;
        for (int i = 0; i < bestScoresLength; i++)
        {
            if (i < bestScoresText.Length)
            {
                bestPlayerNameScoresText[i].text = bestScores[i].PlayerName;
                bestScoresText[i].text = bestScores[i].Score.ToString();
            }
        }
    }


}