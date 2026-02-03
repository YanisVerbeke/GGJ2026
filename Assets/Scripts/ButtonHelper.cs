using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHelper : MonoBehaviour
{
    public void RestartButtonClick()
    {
        Debug.Log("Button");
        SfxManager.Instance.PlayButton();
        GameMaster.Instance.ResetGame();
        SceneManager.LoadScene(1);
    }

    public void MenuButtonClick()
    {
        SfxManager.Instance.PlayButton();
        GameMaster.Instance.ResetGame();
        // normalement il faudrait mettre une corroutine pour l'anim des rideaux comme les mini-jeux, 
        // mais vu qu'il est sur un canva au dessus de celui des rideaux on peut pas, pas grave
        SceneManager.LoadScene(0);
    }
}
