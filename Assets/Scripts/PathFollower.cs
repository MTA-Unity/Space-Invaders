using UnityEngine;
using PathCreation;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private PathCreator path; 
    [SerializeField] private float speed = 2.5f; 
    private float _distanceTravelled; 
    private readonly EndOfPathInstruction _pathEnd = EndOfPathInstruction.Stop; 
    
    void Update()
    {
        _distanceTravelled += speed * Time.deltaTime; // Move along the path based on speed and time
        transform.position = path.path.GetPointAtDistance(_distanceTravelled, _pathEnd); 
        
        // Check if the end of the path has been reached
        if (_pathEnd == EndOfPathInstruction.Stop && _distanceTravelled >= path.path.length)
        {
            Destroy(gameObject); // Destroy the game object if it has reached the end of the path
        }
    }

    // Set the path for the PathFollower
    public void SetPath(PathCreator inputPath)
    {
        path = inputPath;
    }
}