using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneName; //Required Field; Case Sensitve; Please use full path instead of name
    public float approachThreshold = 5f;
    private float departThreshold;
    private Rigidbody2D playerRb;
    private float distance;



    
    // Start is called before the first frame update
    void Start()
    {
        departThreshold = approachThreshold * 1.5f;
        playerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void LateUpdate() {
        
        distance = playerRb.Distance(gameObject.GetComponent<BoxCollider2D>()).distance; // Calucates distance to trigger
        if (distance < approachThreshold)
        {
            StartCoroutine(LoadSceneAsync());
        }
    }


    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }*/

    IEnumerator LoadSceneAsync()
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }
    
}
