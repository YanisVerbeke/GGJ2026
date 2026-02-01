using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHelper : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartButtonClick()
    {
        Debug.Log("Button");
        SfxManager.Instance.PlayButton();
        GameMaster.Instance.ResetGame();
        SceneManager.LoadScene(0);
    }
}
