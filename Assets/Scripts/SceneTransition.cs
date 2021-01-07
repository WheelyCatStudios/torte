using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneName; //Required Field; Case Sensitve; Please use full path instead of name
    public float approachThreshold = 1f;
    private float departThreshold;
    


    
    // Start is called before the first frame update
    void Start()
    {
        departThreshold = approachThreshold * 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
