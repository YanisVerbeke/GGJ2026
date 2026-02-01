using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _creditMenu;

    private void Awake()
    {
        _creditMenu.SetActive(false);
    }

    private void Start()
    {
        MusicManager.Instance.StartMainMusic();
        TransitionCanva.Instance.EndTransition();
    }


    public void PlayButtonClick()
    {
        SfxManager.Instance.PlayButton();
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.2f);

        TransitionCanva.Instance.StartTransition();

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(1);

        yield return null;
    }

    public void QuitButtonClick()
    {
        SfxManager.Instance.PlayButton();
        Application.Quit();
    }

    public void CreditButtonClick()
    {
        SfxManager.Instance.PlayButton();
        _creditMenu.SetActive(true);
    }

    public void BackCreditButtonClick()
    {
        SfxManager.Instance.PlayButton();
        _creditMenu.SetActive(false);
    }
}
