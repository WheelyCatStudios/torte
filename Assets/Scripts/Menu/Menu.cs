using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("Fancy")]
    public Animator anim;
    public AudioSource source;
    public AudioClip bootClip;

    // Activate and Deactivate UI Gameobjects
    public void OCObject(GameObject obj) => obj.SetActive(!obj.activeInHierarchy);

    // Load Scene
    public void LoadScene(int scene) => StartCoroutine(LoadSceneFancy(scene));

    // Close Editor && Close Game
    public void ExitGame() => StartCoroutine(ExitGameFancy());

    IEnumerator LoadSceneFancy(int scene)
    {
        source.PlayOneShot(bootClip);
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(scene);
    }

    IEnumerator ExitGameFancy()
    {
        source.PlayOneShot(bootClip);
        anim.SetTrigger("Exit");

        yield return new WaitForSeconds(6f);

        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #endif

        Application.Quit();
    }
}
