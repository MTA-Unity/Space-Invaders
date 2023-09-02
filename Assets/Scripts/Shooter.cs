using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifeTime = 5f;
    [SerializeField] private float baseFiringRate = 0.2f;
    
    [Header("AI")]
    [SerializeField] private bool useAI;
    [SerializeField] private float firingRateVariance = 0f;
    [SerializeField] private float minimumFiringRate = 0.1f;

    [HideInInspector] public bool isFiring;
    private Sequence _firingSequence;
    private AudioPlayer _audioPlayer;

    private void Awake()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start()
    {
        if (useAI)
        {// Adjust firing rate for AI enemies
            isFiring = true;
            baseFiringRate = Mathf.Max(baseFiringRate - firingRateVariance, minimumFiringRate);
        }
    }

    private void Update()
    {
        Fire(); 
    }
    
    private void Fire()
    {
        if (isFiring && _firingSequence == null)
        {// Calculate firing rate, accounting for AI variance
            float firingRate = useAI ? baseFiringRate + Random.Range(-firingRateVariance, firingRateVariance) : baseFiringRate;
            // Create a firing sequence with initial delay and looping
            _firingSequence = DOTween.Sequence()
                .AppendCallback(FireProjectile) // Fire a projectile
                .AppendInterval(firingRate) // Initial delay
                .SetLoops(-1); // Loop indefinitely
        }
        else if (!isFiring && _firingSequence != null)
        {// Stop firing sequence if not firing anymore
            _firingSequence.Kill();
            _firingSequence = null;
        }
    }

    // Fire a projectile in the specified direction
    private void FireProjectile()
    {
        Vector3 shootingDirection = useAI ? -transform.up : transform.up; // Determine shooting direction
        _audioPlayer.PlayShootingClip(); // Play shooting sound
        GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity); // Create a projectile instance
        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = shootingDirection * projectileSpeed; // Set projectile velocity
        }
        Destroy(instance, projectileLifeTime); // Destroy projectile after a certain lifetime
    }
    
    // Clean up firing sequence on object destruction
    private void OnDestroy()
    {
        if (_firingSequence != null)
        {
            _firingSequence.Kill();
            _firingSequence = null;
        }
    }
}
