using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager;
    public Image screenBlackout;
    
    void Awake()
    {
        uiManager = this;
    }

    private void Start()
    {
        StartCoroutine(HideScreenBlackout());
    }

    public IEnumerator ShowScreenBlackout()
    {
        Color c = screenBlackout.color;
        for(int x = 0;x <= 20;x++)
        {
            c.a = (x/20f);
            screenBlackout.color = c;
            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator HideScreenBlackout()
    {
        Color c = screenBlackout.color;
        for (int x = 0; x <= 20; x++)
        {
            c.a = 1-(x / 20f);
            screenBlackout.color = c;
            yield return new WaitForFixedUpdate();
        }
    }
}
