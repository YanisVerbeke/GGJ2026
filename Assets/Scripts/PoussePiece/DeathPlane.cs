using UnityEngine;

public class DeathPlane : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        // If MASK = WIN YIPPEEEEEEEEEEEEE

        if (collision.gameObject.name == "Mask")
        {
            if (GameObject.Find("PoussePieceManager").GetComponent<PoussePieceManager>() != null)
            {
                GameObject.Find("PoussePieceManager").GetComponent<PoussePieceManager>().WinMiniGame();
            }
            else
            {
                GameObject.Find("PoussePieceManager").GetComponent<FakePoussePieceManager>().WinMiniGame();
            }
        }

        Destroy(collision.gameObject);
    }
}
