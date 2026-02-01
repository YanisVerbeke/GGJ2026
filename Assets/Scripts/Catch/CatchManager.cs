using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatchManager : MonoBehaviour
{
    private enum State { IDLE, PULLING, END }

    private int _mashCounter;
    private Animator _animator;
    [SerializeField] private float _mashBuffer;
    private State _state;
    private float _timer;
    private float _timerMax = 7f;
    private Image _timerImage;
    private bool _isEndAnimLaunched = false;

    private void OnEnable()
    {
        TransitionCanva.Instance?.EndTransition();
    }

    private void Start()
    {
        _mashCounter = Random.Range(25, 30);
        _animator = GameObject.Find("Catcher").GetComponent<Animator>();
        _timerImage = GameObject.Find("timerImage").GetComponent<Image>();
        _timer = _timerMax;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _mashCounter--;
            _mashBuffer = 0.4f;
        }

        switch (_state)
        {
            case State.IDLE:
                if (_mashBuffer > 0f)
                {
                    _state = State.PULLING;
                    _animator.SetTrigger("Pulling");
                }
                break;
            case State.PULLING:
                if (_mashBuffer > 0f)
                {
                    _mashBuffer -= Time.deltaTime;
                }
                else
                {
                    _state = State.IDLE;
                    _animator.SetTrigger("Idle");
                }
                break;
            case State.END:
                break;
        }

        if (_mashCounter <= 0 && _state != State.END)
        {
            // WIN
            _state = State.END;
            _animator.SetTrigger("End");
            if (!_isEndAnimLaunched)
            {
                StartCoroutine(EndAnim(true));
                _isEndAnimLaunched = true;
            }
        }

        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;
            _timerImage.fillAmount = _timer / _timerMax;
        }
        else
        {
            // Loose
            if (!_isEndAnimLaunched)
            {
                StartCoroutine(EndAnim(false));
                _isEndAnimLaunched = true;
            }
        }
    }

    private IEnumerator EndAnim(bool won)
    {
        yield return new WaitForSeconds(0.2f);

        if (won)
        {
            SfxManager.Instance?.PlayYippee();
        }
        else
        {
            SfxManager.Instance?.PlayHonk();
        }

        yield return new WaitForSeconds(2f);

        TransitionCanva.Instance?.StartTransition();

        yield return new WaitForSeconds(1.5f);

        GameMaster.Instance?.EndMiniGame(won);

        yield return null;
    }
}
