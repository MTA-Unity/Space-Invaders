using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private bool _isPlayer;
    [SerializeField] private int health = 50;
    [SerializeField] private int _score = 50;
    
    private CameraShake cameraShake;
    private const float ShakeDuration = 0.2f;
    private const float ShakeMagnitude = 0.1f;
    
    private AudioPlayer _audioPlayer;
    private ScoreKeeper _scoreKeeper;
    private LevelManager _levelManager;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        _levelManager = FindObjectOfType<LevelManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider contains a DamageDealer component
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {// Take damage, play damage sound, shake camera, and notify DamageDealer it's been hit
            TakeDamage(damageDealer.GetDamage());
            _audioPlayer.PlayDamageClip();
            cameraShake.ShakeCamera(ShakeDuration, ShakeMagnitude);
            damageDealer.Hit();
        }
    }

    private void TakeDamage(int damage)
    {// Reduce health and check if health is zero or below
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (_isPlayer)
        {// If this is the player, update best scores, and load the game over screen
            _scoreKeeper.UpdateBestScores();
            _levelManager.LoadGameOver();
        }
        else
        {// If not the player, increase the score 
            _scoreKeeper.ChangeScore(_score);
        }
        Destroy(gameObject);
    }
    
    public int GetHealth()
    {
        return health;
    }
}
