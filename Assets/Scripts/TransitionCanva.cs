using UnityEngine;

public class TransitionCanva : MonoBehaviour
{
    public static TransitionCanva Instance { get; private set; }

    private Animator _animator;

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
        _animator = GetComponent<Animator>();
    }

    public void StartTransition()
    {
        _animator.SetTrigger("In");
    }

    public void EndTransition()
    {
        _animator.SetTrigger("Out");
    }
}
