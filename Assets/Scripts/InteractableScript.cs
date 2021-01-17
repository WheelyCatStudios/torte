using UnityEngine;
using UnityEngine.UI;

public class InteractableScript : MonoBehaviour
{
    public bool IsAnpc;
    public bool IsAObject;

    public bool InContact;
   
    [Header("Trigger collider variables")] 
    
    [Range(0 , 5f)]
    public float ColliderBoundsX;
    
    [Range(0, 5f)]
    public float ColliderBoundsY;
    
    [Range(0, 5f)]
    public float OffsetColliderBoundsX;
    
    [Range(0, 5f)]
    public float OffsetColliderBoundsY;
    
    private BoxCollider2D ObjCollider;

    [Header("physycal collider")]
    public bool HavePhysicalCollider;
    private BoxCollider2D colobj;
    
    [Header("feedback")]
    public GameObject InteractFeedback;
    public Text feedbacktext;

    void Start()
    {
        ColliderSetup();
        if (IsAnpc == true) feedbacktext.text = "press f to talk";
        if (IsAObject == true) feedbacktext.text = "press f to get";


    }


    void Update()
    {
        if(InContact == true /* & input.getButtonDown(E)*/ )
        {
            OnContactAndAction();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InContact = true;
            InteractFeedback.SetActive(true);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InContact = false;
            InteractFeedback.SetActive(false);
        }
    }
    public void ColliderSetup()
    {
        ObjCollider = gameObject.AddComponent (typeof (BoxCollider2D) ) as BoxCollider2D;
        ObjCollider.size = new Vector2 ( ColliderBoundsX + 1 , ColliderBoundsY + 1 );
        ObjCollider.offset = new Vector2 ( OffsetColliderBoundsX , OffsetColliderBoundsY );
        ObjCollider.isTrigger = true;
        ObjCollider.enabled = true;

        if(HavePhysicalCollider == true)
        {
            colobj = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        }


    }
    
    
    public void OnContactAndAction()
    {
       if (IsAnpc == true) 
       {
            NPCbehavior.action();
       }
       if (IsAObject == true)
       {
            ObjCollider.enabled = false;
            InContact = false;
            OBJbehavior.action();

       }   
    }





}
