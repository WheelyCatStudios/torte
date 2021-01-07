using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneName; //Required Field; Case Sensitve; Please use full path instead of name
    public float approachThreshold = 10f;
    private float departThreshold;
    private Vector2 direction;
    private float distance;



    
    // Start is called before the first frame update
    void Start()
    {
        departThreshold = approachThreshold * 1.5f;
        direction = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity; //Grab velocity of player and set a Vector2
    }

    void LateUpdate() {
        //distance = Vector2.Distance(direction, )    
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
