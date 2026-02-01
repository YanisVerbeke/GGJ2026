using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FakePoussePieceManager : MonoBehaviour
{
    private bool _endAnimLaunched = false;
    private TextMeshProUGUI _endText;
    private bool _hasStarted = false;
    [SerializeField] private GameObject _trueMenu;
    [SerializeField] private AudioClip _goatClip;

    private void Awake()
    {
        _endText = GameObject.Find("endText").GetComponent<TextMeshProUGUI>();
        _endText.enabled = false;
        Time.timeScale = 0;
        GameObject.Find("PoussePiece").GetComponent<CoinSpawner>().enabled = false;
    }

    private IEnumerator EndAnim(bool won)
    {
        yield return new WaitForSeconds(0.4f);

        if (won)
        {
            _endText.text = "Bravo !";
            SfxManager.Instance.PlayYippee();
        }
        else
        {
            _endText.text = "Dommage...";
            SfxManager.Instance.PlayHonk();
        }
        _endText.enabled = true;

        yield return new WaitForSeconds(2f);

        TransitionCanva.Instance.StartTransition();

        yield return new WaitForSeconds(2f);

        GameObject.Find("SfxManager").GetComponent<AudioSource>().PlayOneShot(_goatClip, 2f);

        yield return new WaitForSeconds(1f);

        MusicManager.Instance.StartMusic();

        _trueMenu.SetActive(true);
        TransitionCanva.Instance.EndTransition();

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

    public void PlayButtonClick()
    {
        if (!_hasStarted)
        {
            _hasStarted = true;
            GameObject.Find("Menu").SetActive(false);
            GameObject.Find("PoussePiece").GetComponent<CoinSpawner>().enabled = true;
            Time.timeScale = 1;
        }
        else
        {
            StartCoroutine(StartGame());
        }
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.2f);

        TransitionCanva.Instance.StartTransition();

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(0);

        yield return null;
    }

    public void QuitButtonClick()
    {
        Application.Quit();
    }

    public void CreditButtonClick()
    {
        // ouais ouais ouais
    }
}