using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager Instance { get; private set; }

    [SerializeField] private AudioClip _yippee;
    [SerializeField] private AudioClip _honk;
    [SerializeField] private AudioClip _coin;
    [SerializeField] private AudioClip _thinking;
    [SerializeField] private AudioClip _angry;
    [SerializeField] private AudioClip _alien;

    private AudioSource _audioSource;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayYippee()
    {
        _audioSource.PlayOneShot(_yippee);
    }

    public void PlayHonk()
    {
        _audioSource.PlayOneShot(_honk, 3f);
    }

    public void PlayCoin()
    {
        _audioSource.PlayOneShot(_coin);
    }

    public void PlayThinking()
    {
        _audioSource.PlayOneShot(_thinking, 3f);
    }

    public void PlayAngry()
    {
        _audioSource.PlayOneShot(_angry, 3f);
    }

    public void PlayAlien()
    {
        _audioSource.PlayOneShot(_alien);
    }
}
