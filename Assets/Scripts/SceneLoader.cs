using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] int lastBuildIndex;
    TextMeshProUGUI stageNumberText;
    TextMeshProUGUI instructionsText;
    TextMeshProUGUI commandsText;
    int stageNumber = 0;
    bool isCommandsShowing = false;
    //string currentGameName;
    int nextGameIndex;
    int currentGameIndex;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageNumberText = GameObject.Find("Stage number").GetComponent<TextMeshProUGUI>();
        instructionsText = GameObject.Find("Instructions").GetComponent<TextMeshProUGUI>();
        commandsText = GameObject.Find("Commands").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isCommandsShowing)
            {
                SceneManager.LoadScene(currentGameIndex);
            }
            stageNumber ++;
            nextGameIndex = UnityEngine.Random.Range(1, lastBuildIndex + 1);
            InstructionShowing(nextGameIndex);
            currentGameIndex = nextGameIndex;
        }
    }

    void InstructionShowing(int index)
    {
        stageNumberText.text = stageNumber.ToString();
        switch (index)
        {
            case 1:
                instructionsText.text= "Pousse le masque !";
                commandsText.text = "Click : met une piece\nGlisse la souris : Deplace ou la piece va tomber";
                isCommandsShowing = true;
                break;
            default:
            break;
        }
    }


}
