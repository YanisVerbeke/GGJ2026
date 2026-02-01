using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] private AudioClip _mainMusic;
    [SerializeField] private AudioClip _poussePieceMusic;

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

    public void StartMainMusic()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
        _audioSource.time = 0f;
        _audioSource.clip = _mainMusic;
        _audioSource.volume = 1f;
        _audioSource.Play();
    }

    public void StartPoussePieceMusic()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
        _audioSource.time = 0f;
        _audioSource.clip = _poussePieceMusic;
        _audioSource.volume = 0.8f;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
        _audioSource.time = 0f;
    }

}
