using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    // Pour ajouter un mini jeu, ajouter un asset de type scriptableobject/minigame, et le mettre dans la liste
    [SerializeField] private List<MiniGameScriptableObject> _miniGameList;
    [SerializeField] float difficultyLever;

    public int CurrentStageNumber { get { return _currentStageNumber; } }
    public int CurrentLives { get { return _currentLives; } }
    public List<MiniGameScriptableObject> MiniGameList { get { return _miniGameList; } }
    public Dictionary<MiniGameScriptableObject,int> weightedMiniGameList = new Dictionary<MiniGameScriptableObject, int>();

    private int _currentStageNumber;
    private int _currentDifficultyLevel;
    private int _currentLives;

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
    }

    private void Start()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        _currentStageNumber = 1;
        _currentDifficultyLevel = 1;
        _currentLives = 4;
        weightedMiniGameList = new Dictionary<MiniGameScriptableObject, int>();
        foreach (MiniGameScriptableObject miniGame in MiniGameList)
        {
            weightedMiniGameList.Add(miniGame, 1);
        }
    }

    public void EndMiniGame(bool won)
    {
        // To do inscrease difficulty etc 
        // Fonction � appeler lorsque la condition de victoire ou de d�faite est atteinte dans le mini game en cours

        _currentStageNumber++;
        Time.timeScale = Time.timeScale + (difficultyLever * _currentStageNumber);
        if (!won)
        {
            _currentLives--;
        }
        SceneManager.LoadScene(1);

    }

    public void RestartButtonClick()
    {
        Debug.Log("OUI");
        SfxManager.Instance.PlayButton();
        Instance.ResetGame();
        SceneManager.LoadScene(1);
    }

    public void MenuButtonClick()
    {
        SfxManager.Instance.PlayButton();
    }

    public MiniGameScriptableObject MiniGameChoice()
    {
        List<MiniGameScriptableObject> miniGames = new List<MiniGameScriptableObject>();
        int randomInt;
        foreach (MiniGameScriptableObject miniGame in weightedMiniGameList.Keys)
        {
            for (int i = 0; i < weightedMiniGameList[miniGame]; i++)
            {
                miniGames.Add(miniGame);
            }
        }
        foreach (MiniGameScriptableObject miniGame in MiniGameList)
        {
            weightedMiniGameList[miniGame]++;
        }
        randomInt = Random.Range(0, miniGames.Count);
        weightedMiniGameList[miniGames[randomInt]] = 0;
        return miniGames[randomInt];
    }
}
