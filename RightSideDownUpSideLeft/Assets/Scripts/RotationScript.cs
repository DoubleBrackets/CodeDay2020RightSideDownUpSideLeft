using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotationScript : MonoBehaviour
{
    public static RotationScript rotationScript;
    private void Awake()
    {
        rotationScript = this;
        playerRb = gameObject.GetComponent<Rigidbody2D>();
    }


    public GameObject levelContainer;
    private Rigidbody2D playerRb;

    private bool rotationDebounce = false;

    //Mouse controls
    Vector2 startPos;

    private float slowMotionScale = 0.8f;

    private int canRotate = 0;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow) && !rotationDebounce && canRotate == 0)
        {
            StartCoroutine(RotateLevel(-90));
            StartCoroutine(RotationAnimationScript.rotationAnimationScript.StartRotationAnimation(1));
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !rotationDebounce && canRotate == 0)
        {
            StartCoroutine(RotateLevel(90));
            StartCoroutine(RotationAnimationScript.rotationAnimationScript.StartRotationAnimation(-1));
        }

        /*
        //Mouse controls
        if(Input.GetMouseButtonDown(0))
        {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            Vector2 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 startDiff = startPos - (Vector2)transform.position;
            Vector2 endDiff = endPos - (Vector2)transform.position;
            float angle1 = Mathf.Atan2(startDiff.y, startDiff.x);
            float angle2 = Mathf.Atan2(endDiff.y, endDiff.x);

            float angleDiff1 = (angle2 - angle1 + 2 * Mathf.PI);
            float angleDiff2 = (angle2 - angle1);
            float angleDiff3 = (angle2 - angle1 - 2 * Mathf.PI);

            float angleDiff;

            if(Mathf.Abs(angleDiff1) <= Mathf.Abs(angleDiff2))
            {
                if (Mathf.Abs(angleDiff1) <= Mathf.Abs(angleDiff3))
                {
                    angleDiff = angleDiff1;
                }
                else
                    angleDiff = angleDiff3;
            }
            else
            {
                if (Mathf.Abs(angleDiff2) <= Mathf.Abs(angleDiff3))
                {
                    angleDiff = angleDiff2;
                }
                else
                    angleDiff = angleDiff3;
            }
            if (angleDiff > 0 && !rotationDebounce)
            {
                StartCoroutine(RotateLevel(90));
            }
            else if(angleDiff < 0 && !rotationDebounce)
            {
                
                StartCoroutine(RotateLevel(-90));
            }

        }
        */

    }


    public event Action<float> onRotateEvent;

    public void SetRotation(float angle)
    {
        levelContainer.transform.rotation = Quaternion.Euler(0, 0, angle);

        gameObject.transform.rotation = Quaternion.identity;
        Camera.main.transform.rotation = Quaternion.identity;
    }

    IEnumerator RotateLevel(float angle)
    {
        //Freezes player, saves velocity
        /*Vector2 velocity = playerRb.velocity;
        playerRb.constraints = RigidbodyConstraints2D.FreezeAll;*/

        rotationDebounce = true;

        //Rotates the entire level, while keeping the player + camera at no rotation
        float currentRotation = levelContainer.transform.rotation.eulerAngles.z;
        float targetRotation = currentRotation + angle;

        //Slowdown effect
        Time.timeScale = slowMotionScale;
        Time.fixedDeltaTime = slowMotionScale * 0.02f;

        for (int x = 0; x < 25; x++)
        {
            float  val = Mathf.Lerp(currentRotation, targetRotation, 0.17f);
            currentRotation = val;

            levelContainer.transform.rotation = Quaternion.Euler(0, 0, val);

            gameObject.transform.rotation = Quaternion.identity;
            Camera.main.transform.rotation = Quaternion.identity;
            yield return new WaitForFixedUpdate();
        }

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        levelContainer.transform.rotation = Quaternion.Euler(0, 0, targetRotation);

        gameObject.transform.rotation = Quaternion.identity;
        Camera.main.transform.rotation = Quaternion.identity;

        
        onRotateEvent?.Invoke(angle);

        //unfreeze player, restore velocity
        /*playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerRb.velocity = velocity;*/

        yield return new WaitForSeconds(0.1f);
        rotationDebounce = false;
    }

    float Lerp(float a, float b, float t)
    {
        if(a > b)
        {
            return b + (a - b) * t;
        }
        else
        {
            return a + (b - a) * t;
        }
    }

    public void IncrementCanRotate()
    {
        canRotate++;
    }

    public void DecrementCanRotate()
    {
        canRotate--;
    }
}
