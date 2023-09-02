using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private AudioClip shootingClip;   
    [SerializeField] [Range(0f, 1f)] private float shootingVolume; 

    [Header("Damage")]
    [SerializeField] private AudioClip damageClip;     
    [SerializeField] [Range(0f, 1f)] private float damageVolume; 

    // Play the shooting sound effect
    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    // Play the damage sound effect
    public void PlayDamageClip()
    {
        PlayClip(damageClip, damageVolume);
    }

    // Play a provided audio clip at a specific position with a given volume
    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            // Play the audio clip at the position of the main camera with the specified volume
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }
    }
}