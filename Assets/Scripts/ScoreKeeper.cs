using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper Instance { get; private set; }
    private SaveSystem _saveSystem;
    private ScoreEntry[] _bestScores;
    private ScoreEntry _score = new ScoreEntry();

    private void Awake()
    {
        // Ensure that only one instance of the ScoreKeeper exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
        _saveSystem = GetComponent<SaveSystem>();
        LoadBestScores(); // Load best scores from save
    }


    // Save the best scores to the save system
    private void SaveBestScores()
    {
        _saveSystem.SaveBestScores(_bestScores);
    }

    // Load the best scores from the save system
    public void LoadBestScores()
    {
        _bestScores = _saveSystem.LoadBestScores();
    }
    
    public ScoreEntry[] GetBestScores()
    {
        return _bestScores;
    }

    // Update the best scores with the current score
    public void UpdateBestScores()
    {
        for (int i = 0; i < _bestScores.Length; i++)
        {
            if (_score.Score > _bestScores[i].Score)
            {
                for (int j = _bestScores.Length - 1; j > i; j--)
                {
                    _bestScores[j] = _bestScores[j - 1];
                }
                _bestScores[i] = _score;
                break;
            }
        }

        SaveBestScores(); // Save the updated best scores
    }

    public int GetScore()
    {
        return _score.Score;
    }
    
    public void ChangeScore(int scoreToAdd)
    {
        _score.Score += scoreToAdd;
        _score.Score = Mathf.Clamp(_score.Score, 0, int.MaxValue); // Ensure score doesn't go below 0
    }
    public void ResetScore(string name)
    {
            _score= new ScoreEntry();
            this.SetName(name);
            _score.Score = 0;
    }

    public void SetName(string name)
    {
            _score.PlayerName = name;
    }

    public string GetTheCureentPlayerName()
    {
        return _score.PlayerName;
    }
}