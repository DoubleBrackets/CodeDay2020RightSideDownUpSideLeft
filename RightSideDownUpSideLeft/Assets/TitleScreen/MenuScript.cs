using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("Level1");
    }

}
