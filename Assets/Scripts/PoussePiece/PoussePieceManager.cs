using UnityEngine;

public class PoussePieceManager : MonoBehaviour
{


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
    }
}
