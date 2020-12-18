using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{

    // Activate and Deactivate UI Gameobjects
    public void OCObject(GameObject obj)
    {
        if (obj.activeInHierarchy)
        {
            obj.SetActive(false);
        }
        else
        {
            obj.SetActive(true);
        }

    }

    // Load Scene
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    // Close Editor && Close Game
    public void ExitGame()
    {
        Debug.Log("Exiting Game");
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif

        Application.Quit();
    }
}
