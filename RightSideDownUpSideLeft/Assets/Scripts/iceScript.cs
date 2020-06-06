
using UnityEngine;

public class iceScript : MonoBehaviour
{
    public GameObject slip;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == slip ){
            PlayerMovementScript.playerMovementScript.IncrementSlowdownActive();
            PlayerMovementScript.playerMovementScript.moveSpeed += 10;
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject== slip)
        {
            PlayerMovementScript.playerMovementScript.DecrementSlowdownActive();
            PlayerMovementScript.playerMovementScript.moveSpeed -= 10;
        }
        
    }
}
