using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuartziScene : MonoBehaviour
{
    GameObject player;

    public string sceneName;
    [Space]
    public float distanceThreshold;
    [Space]
    public Vector2 directionTreshold;

    bool sceneLoadTriggered;
    bool sceneLoaded;
    bool goScene;

    float distance;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate()
    {
        distance = Vector2.Distance(player.transform.position, this.transform.position);
        if(!sceneLoadTriggered && distance < distanceThreshold)
        {
            StartCoroutine(LoadSceneAsync());
        }
        else if(sceneLoaded && distance > distanceThreshold * 1.5f)
        {
            goScene = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (player.GetComponent<Rigidbody2D>().velocity.x >= directionTreshold.x)
            {
                goScene = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (player.GetComponent<Rigidbody2D>().velocity.x >= directionTreshold.x)
            {
                goScene = true;
            }
        }
    }

    IEnumerator LoadSceneAsync()
    {
        sceneLoadTriggered = true;

        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(sceneName);
        sceneLoad.allowSceneActivation = false;

        while (!sceneLoad.isDone)
        {
            sceneLoaded = sceneLoad.progress >= 0.9f;
            if (goScene && sceneLoaded)
            {
                sceneLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
