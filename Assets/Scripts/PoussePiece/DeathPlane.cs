using UnityEngine;

public class DeathPlane : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        // If MASK = WIN YIPPEEEEEEEEEEEEE

        Destroy(collision.gameObject);
    }
}
