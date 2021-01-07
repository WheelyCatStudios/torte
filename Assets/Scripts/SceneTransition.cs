using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneName; //Required Field; Case Sensitve; Please use full path instead of name
    public float approachThreshold = 10f;
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
        
        distance = playerRb.Distance(gameObject.GetComponent<BoxCollider2D>()).distance;
        Debug.Log(distance);  
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    /*private void float CalcDistanceToTrigger()
    {
        return playerRb.Distance(gameObject.GetComponent<BoxCollider2D>()).distance;
    }*/
    
}
