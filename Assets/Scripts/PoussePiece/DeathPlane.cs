using UnityEngine;

public class DeathPlane : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        // If MASK = WIN YIPPEEEEEEEEEEEEE

        if (collision.gameObject.name == "Mask")
        {
            GameObject.Find("PoussePieceManager").GetComponent<PoussePieceManager>().WinMiniGame();
        }

        Destroy(collision.gameObject);
    }
}
