using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Menu : MonoBehaviour
{
    private Animator anim;
    private AudioSource source;
    public AudioClip bootClip;

    // Activate and Deactivate UI Gameobjects
    public void OCObject(GameObject obj) => obj.SetActive(!obj.activeInHierarchy);

    // Load Scene
    public void LoadScene(int scene) => StartCoroutine(LoadSceneFancy(scene));

    // Close Editor && Close Game
    public void ExitGame() => StartCoroutine(ExitGameFancy());

	void Start() {
		anim = GetComponent<Animator>();
		source = GetComponent<AudioSource>();
	}

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
