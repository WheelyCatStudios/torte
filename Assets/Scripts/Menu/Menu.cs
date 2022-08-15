using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Behaviour controller scripture for the main menu scene
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Menu : MonoBehaviour
{
	#region attributes
	/// <summary>
	/// The main menu animator
	/// </summary>
    private Animator anim;

	/// <summary>
	/// The menu audio source
	/// </summary>
    private AudioSource source;

	/// <summary>
	/// The audio clip played when the user clicks 'play'
	/// </summary>
    public AudioClip bootClip;
	#endregion

	/// <summary>
	/// Monobehaviour start 
	/// <br/>
	/// Populates <a cref="anim"/> and <a cref="source"/> from sibling Animator and AudioSource
	/// </summary>
	void Start() {
		anim = GetComponent<Animator>();
		source = GetComponent<AudioSource>();
	}

    /// <summary>
	/// Activate and Deactivate UI Gameobjects within the scene
	/// </summary>
	/// <param name="obj">Object to enable or disable</param>
    public void OCObject(GameObject obj) => obj.SetActive(!obj.activeInHierarchy);

    /// <summary>
	/// Animates a transition between the menu scene and another
	/// </summary>
	/// <param name="scene">The scene index to switch to</param>
    public void LoadScene(int scene) => StartCoroutine(LoadSceneFancy(scene));

    /// <summary>
	/// Animates a scene transistion, then closes the runtime
	/// </summary>
    public void ExitGame() => StartCoroutine(ExitGameFancy());

	/// <summary>
	/// Animates a scene transition between the menu and the specified scene index
	/// </summary>
	/// <param name="scene">The scene index to switch to</param>
	/// <returns>Nothing, co routines just waits for animation</returns>
    IEnumerator LoadSceneFancy(int scene)
    {
        source.PlayOneShot(bootClip);
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(scene);
    }

	/// <summary>
	/// Animates a scene transition, then exits the runtime
	/// </summary>
	/// <returns>Nothing, co routines just waits for animation</returns>
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
