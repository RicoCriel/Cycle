using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _playerAudioSource;
    [SerializeField] private AudioClip _gunshot;
    [SerializeField] private AudioClip _hurt;
    [SerializeField] private AudioClip _hit;
    [SerializeField] private AudioClip _collect;

    public static AudioManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }


    public void PlayGunShotFX()
    {
        _playerAudioSource.PlayOneShot(_gunshot);
    }

    public void PlayHurtFX()
    {
        _playerAudioSource.PlayOneShot(_hurt);
    }

    public void PlayHitFX()
    {
        _playerAudioSource.PlayOneShot(_hit);
    }

    public void PlayCollectFX()
    {
        _playerAudioSource.PlayOneShot(_collect);
    }
}
