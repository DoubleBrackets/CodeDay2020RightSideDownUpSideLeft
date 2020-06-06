using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangeScript : MonoBehaviour
{
    public string sceneToChangeTo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(UIManager.uiManager.ShowScreenBlackout());
            Invoke("ChangeScene", 2f);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(sceneToChangeTo);
        this.enabled = false;
    }
}
