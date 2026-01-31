using System.Collections;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class PoussePieceManager : MonoBehaviour
{
    private float _timer;
    private float _maxTimer = 40f;
    private bool _endAnimLaunched = false;
    private TextMeshProUGUI _endText;
    private Image _timerImage;

    private void Awake()
    {
        _endText = GameObject.Find("endText").GetComponent<TextMeshProUGUI>();
        _endText.enabled = false;
        _timerImage = GameObject.Find("timerImage").GetComponent<Image>();
        _timer = _maxTimer;
    }

    private void OnEnable()
    {
        TransitionCanva.Instance.EndTransition();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) // W parce que qwerty etc
        {
            // debug win
            GameMaster.Instance.EndMiniGame(true);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            // debug lose
            GameMaster.Instance.EndMiniGame(false);
        }


        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;
            _timerImage.fillAmount = (_timer / _maxTimer);
        }
        else
        {
            if (!_endAnimLaunched)
            {
                // Loose Mini Game
                StartCoroutine(EndAnim(false));
                _endAnimLaunched = true;
            }
        }
    }

    private IEnumerator EndAnim(bool won)
    {
        yield return new WaitForSeconds(0.4f);

        if (won)
        {
            _endText.text = "Bravo !";
        }
        else
        {
            _endText.text = "Dommage...";
        }
        _endText.enabled = true;

        yield return new WaitForSeconds(2f);

        TransitionCanva.Instance.StartTransition();

        yield return new WaitForSeconds(1.5f);

        GameMaster.Instance.EndMiniGame(won);

        yield return null;
    }

    public void WinMiniGame()
    {
        if (!_endAnimLaunched)
        {
            StartCoroutine(EndAnim(true));
            _endAnimLaunched = true;
        }
    }
}
