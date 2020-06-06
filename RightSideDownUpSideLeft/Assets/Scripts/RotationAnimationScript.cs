using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimationScript : MonoBehaviour
{
    public static RotationAnimationScript rotationAnimationScript;
    // Start is called before the first frame update
    LineRenderer lineRen;

    private int pointCount = 36;

    public float radius;

    private void Awake()
    {
        rotationAnimationScript = this;
        lineRen = gameObject.GetComponent<LineRenderer>();
    }

    public IEnumerator StartRotationAnimation(int dir)//-1 is clockwise, 1 is counter
    {
        float incr = 360f / pointCount;

        for(int x = 0;x <= pointCount;x++)
        {
            lineRen.positionCount = x + 1;
            float angle = (dir)* x * incr * Mathf.Deg2Rad;
            Vector2 pos = new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
            if(x%3==0)
                yield return new WaitForFixedUpdate();
            lineRen.SetPosition(x, pos);
        }

        //Retracts the line
        Vector3[] positions = new Vector3[lineRen.positionCount];
        for (int x = 0; x <= pointCount; x++)
        {
            lineRen.GetPositions(positions);
            Vector3[] newPos = new Vector3[lineRen.positionCount-1];
            for(int y = 0;y < lineRen.positionCount-1;y++)
            {
                newPos[y] = positions[y + 1];
            }
            lineRen.positionCount--;
            lineRen.SetPositions(newPos);
            if (x % 3 == 0)
                yield return new WaitForFixedUpdate();
            positions = new Vector3[lineRen.positionCount];
        }
    }

    
}
