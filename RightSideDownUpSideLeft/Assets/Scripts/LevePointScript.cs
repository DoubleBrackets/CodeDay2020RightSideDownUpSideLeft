using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevePointScript : MonoBehaviour
{
    static int currentLevelPoint;

    // Start is called before the first frame update
    //Bounds
    public GameObject bound1;
    public GameObject bound2;
    public GameObject bound3;
    public GameObject bound4;

    public bool FollowBounds = true;
    //RespawnPoint
    public GameObject respawnPoint;

    public bool setNewRespawn = true;
    // Update is called once per frame
    private void Start()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && currentLevelPoint != gameObject.GetInstanceID())
        {
            currentLevelPoint = gameObject.GetInstanceID();
            //UpdateBounds();
            if (setNewRespawn)
            {
                PlayerEntityScript.playerEntityScript.SetRespawnPoint(respawnPoint);
            }
        }
    }

    protected void UpdateBounds()
    {
        if(currentLevelPoint == (gameObject.GetInstanceID()))
        {
            //Sets bounds
            CameraScript camScript = CameraScript.cameraScript;
            if (FollowBounds)
            {
                camScript.SetFollowBounds(true);
                float[] boundsx = { bound1.transform.position.x, bound2.transform.position.x, bound3.transform.position.x, bound4.transform.position.x };
                float[] boundsy = { bound1.transform.position.y, bound2.transform.position.y, bound3.transform.position.y, bound4.transform.position.y };
                camScript.SetLeftBound(Mathf.Min(boundsx));
                camScript.SetRightBound(Mathf.Max(boundsx));
                camScript.SetUpperBound(Mathf.Max(boundsy));
                camScript.SetLowerBound(Mathf.Min(boundsy));
            }
        }
    }

}
