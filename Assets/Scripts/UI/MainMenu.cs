using UnityEngine;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Loads the game scene.
    /// </summary>
    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    /// <summary>
    /// Exits the game if not in editor.
    /// </summary>
    public void ExitButton()
    {
        if (Application.isEditor)
            Debug.Log("Exit game");
        else
            Application.Quit();
    }
}
