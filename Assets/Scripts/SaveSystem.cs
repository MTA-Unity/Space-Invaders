using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    // Save scores to a binary file.
    private void SaveScores(ScoreEntry[] scores)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/scores"; 
        FileStream stream = new FileStream(path, FileMode.Create); 
        formatter.Serialize(stream, scores); 
        stream.Close(); 
    }

    // Load scores from a binary file.
    private ScoreEntry[] LoadScores()
    {
        string path = Application.persistentDataPath + "/scores"; 
        if (File.Exists(path)) // Check if the file exists
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open); 
            ScoreEntry[] bestScores = formatter.Deserialize(stream) as ScoreEntry[]; // Deserialize and load the scores
            if (bestScores == null)
            {
                bestScores = new ScoreEntry[3] {new ScoreEntry(), new ScoreEntry(), new ScoreEntry()};
            }
            stream.Close(); // Close the file stream
            return bestScores;
        }
        return new ScoreEntry[3] {new ScoreEntry(), new ScoreEntry(), new ScoreEntry()}; // If the file doesn't exist, return default scores
    }

    // Public method to save best scores.
    public void SaveBestScores(ScoreEntry[] scores)
    {
        SaveScores(scores); 
    }

    // Public method to load best scores.
    public ScoreEntry[] LoadBestScores()
    {
        return LoadScores(); 
    }
}