using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyVisualScript : MonoBehaviour
{
    public static KeyVisualScript keyVisualScript;

    //This script deals with the visuals of the key and using it(does not deal with game logic for using the key, only visuals)
    private void Awake()
    {
        keyVisualScript = this;
        spriteRen = gameObject.GetComponent<SpriteRenderer>();
        keyRb = gameObject.GetComponent<Rigidbody2D>();
    }

    bool isFollowingPlayer = false;
    bool isVisible = false;

    SpriteRenderer spriteRen;
    Rigidbody2D keyRb;

    private float followDistance = 4f;

    // Update is called once per frame
    void Update()
    {
        if(isVisible)
        {
            if (isFollowingPlayer)
            {
                Vector2 source = PlayerEntityScript.playerEntityScript.gameObject.transform.position;
                Vector2 diff =  source - (Vector2)gameObject.transform.position;
                if(diff.magnitude > followDistance)
                {
                    gameObject.transform.position = source - diff.normalized * followDistance;
                }
            }
        }
    }

    public IEnumerator UseKeyVisuals(GameObject target)
    {
        keyRb.simulated = false;
        isFollowingPlayer = false;
        for(int x = 0;x < 50;x++)
        {
            gameObject.transform.position = Vector2Lerp(gameObject.transform.position, target.transform.position, 0.1f);
            yield return new WaitForFixedUpdate();
        }
        gameObject.transform.position = target.transform.position;
        yield return new WaitForSeconds(0.5f);
        //Hides key
        isVisible = false;
        spriteRen.enabled = false;
        keyRb.simulated = true;
    }

    Vector2 Vector2Lerp(Vector2 a,Vector2 b, float val)
    {
        return new Vector2(Mathf.Lerp(a.x, b.x, val), Mathf.Lerp(a.y, b.y, val));
    }

    public void GetKeyVisuals()
    {
        isVisible = true;
        spriteRen.enabled = true;
        isFollowingPlayer = true;
    }
}
