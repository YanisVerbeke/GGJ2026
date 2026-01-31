using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    // Pour ajouter un mini jeu, ajouter un asset de type scriptableobject/minigame, et le mettre dans la liste
    [SerializeField] private List<MiniGameScriptableObject> _miniGameList;

    public int CurrentStageNumber { get { return _currentStageNumber; } }
    public int CurrentLives { get { return _currentLives; } }
    public List<MiniGameScriptableObject> MiniGameList { get { return _miniGameList; } }

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

    private void ResetGame()
    {
        _currentStageNumber = 1;
        _currentDifficultyLevel = 1;
        _currentLives = 4;
    }

    public void EndMiniGame(bool won)
    {
        // To do inscrease difficulty etc 
        // Fonction à appeler lorsque la condition de victoire ou de défaite est atteinte dans le mini game en cours

        _currentStageNumber++;
        if (!won)
        {
            _currentLives--;
            if (_currentLives <= 0)
            {
                // GAME OVEEEEEER #notyippee
            }
        }
        SceneManager.LoadScene(0);

    }
}
