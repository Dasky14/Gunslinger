using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void ExitButton()
    {
        if (Application.isEditor)
            Debug.Log("Exit game");
        else
            Application.Quit();
    }
}
