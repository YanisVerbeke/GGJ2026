using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] int lastBuildIndex;
    TextMeshProUGUI stageNumberText;
    TextMeshProUGUI stageNameText;
    TextMeshProUGUI commandsText;
    private Image _life1;
    private Image _life2;
    private Image _life3;
    private Image _life4;
    [SerializeField] private Sprite _lifeIcon;
    [SerializeField] private Sprite _brokenLifeIcon;
    private GameObject _gameOverMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageNumberText = GameObject.Find("Stage number").GetComponent<TextMeshProUGUI>();
        stageNameText = GameObject.Find("Stage Name").GetComponent<TextMeshProUGUI>();
        commandsText = GameObject.Find("Commands").GetComponent<TextMeshProUGUI>();
        _life1 = GameObject.Find("Life1").GetComponent<Image>();
        _life2 = GameObject.Find("Life2").GetComponent<Image>();
        _life3 = GameObject.Find("Life3").GetComponent<Image>();
        _life4 = GameObject.Find("Life4").GetComponent<Image>();
        stageNumberText.enabled = false;
        stageNameText.enabled = false;
        commandsText.enabled = false;
        _life1.enabled = false;
        _life2.enabled = false;
        _life3.enabled = false;
        _life4.enabled = false;
        _gameOverMenu = GameObject.Find("GameOverMenu");
        _gameOverMenu.SetActive(false);

        StartNextMiniGame(GameMaster.Instance.MiniGameList[Random.Range(0, GameMaster.Instance.MiniGameList.Count)]);
    }

    private void OnEnable()
    {
        //TransitionCanva.Instance.EndTransition();
    }

    public void StartNextMiniGame(MiniGameScriptableObject miniGame)
    {
        if (GameMaster.Instance.CurrentLives > 0)
        {
            StartCoroutine(StartAnim(miniGame));
        }
        else
        {
            // Display game over
            _gameOverMenu.SetActive(true);
        }

    }

    IEnumerator StartAnim(MiniGameScriptableObject miniGame)
    {
        yield return new WaitForSeconds(0.5f);

        DisplayLives();

        switch (miniGame.controls)
        {
            case Controls.MOUSE:
                commandsText.text = "À ta souris !";
                break;
            case Controls.KEYBOARD:
                commandsText.text = "À ton clavier !";
                break;
            default:
                commandsText.text = "";
                break;
        }
        commandsText.enabled = true;

        yield return new WaitForSeconds(2f);

        commandsText.enabled = false;

        yield return new WaitForSeconds(0.5f);

        stageNumberText.text = GameMaster.Instance.CurrentStageNumber.ToString();
        stageNameText.text = miniGame.displayName;
        stageNumberText.enabled = true;
        stageNameText.enabled = true;

        yield return new WaitForSeconds(2f);

        stageNumberText.enabled = false;
        stageNameText.enabled = false;

        //TransitionCanva.Instance.StartTransition();

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(miniGame.indexInBuild);

        yield return new WaitForSeconds(0.5f);

        yield return null;
    }

    private void DisplayLives()
    {
        _life1.enabled = true;
        _life2.enabled = true;
        _life3.enabled = true;
        _life4.enabled = true;
        _life1.sprite = GameMaster.Instance.CurrentLives > 3 ? _lifeIcon : _brokenLifeIcon;
        _life2.sprite = GameMaster.Instance.CurrentLives > 2 ? _lifeIcon : _brokenLifeIcon;
        _life3.sprite = GameMaster.Instance.CurrentLives > 1 ? _lifeIcon : _brokenLifeIcon;
        _life4.sprite = GameMaster.Instance.CurrentLives > 0 ? _lifeIcon : _brokenLifeIcon;
    }

}
