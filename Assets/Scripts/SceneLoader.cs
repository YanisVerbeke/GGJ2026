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
    //TextMeshProUGUI instructionsText;
    TextMeshProUGUI commandsText;
    int stageNumber = 0;
    bool isCommandsShowing = false;
    //string currentGameName;
    int nextGameIndex;
    int currentGameIndex;
    private Image _life1;
    private Image _life2;
    private Image _life3;
    private Image _life4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageNumberText = GameObject.Find("Stage number").GetComponent<TextMeshProUGUI>();
        stageNameText = GameObject.Find("Stage Name").GetComponent<TextMeshProUGUI>();
        //instructionsText = GameObject.Find("Instructions").GetComponent<TextMeshProUGUI>();
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

        StartNextMiniGame(GameMaster.Instance.MiniGameList[Random.Range(0, GameMaster.Instance.MiniGameList.Count)]);
    }

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isCommandsShowing)
            {
                SceneManager.LoadScene(currentGameIndex);
            }
            stageNumber++;
            nextGameIndex = UnityEngine.Random.Range(1, lastBuildIndex + 1);
            InstructionShowing(nextGameIndex);
            currentGameIndex = nextGameIndex;
        }
    }*/

    /*void InstructionShowing(int index)
    {
        stageNumberText.text = stageNumber.ToString();
        switch (index)
        {
            case 1:
                instructionsText.text = "Pousse le masque !";
                commandsText.text = "Click : met une piece\nGlisse la souris : Deplace ou la piece va tomber";
                isCommandsShowing = true;
                break;
            default:
                break;
        }
    }*/

    public void StartNextMiniGame(MiniGameScriptableObject miniGame)
    {
        StartCoroutine(StartAnim(miniGame));
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

        TransitionCanva.Instance.StartTransition();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(miniGame.indexInBuild);

        yield return new WaitForSeconds(0.5f);

        TransitionCanva.Instance.EndTransition();

        yield return null;
    }

    private void DisplayLives()
    {
        _life1.enabled = GameMaster.Instance.CurrentLives > 3 ? true : false;
        _life2.enabled = GameMaster.Instance.CurrentLives > 2 ? true : false;
        _life3.enabled = GameMaster.Instance.CurrentLives > 1 ? true : false;
        _life4.enabled = GameMaster.Instance.CurrentLives > 0 ? true : false;
    }

}
