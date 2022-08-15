using UnityEditor;
using UnityEngine;
using InventorySystem;

public class InventoryTesterScript:MonoBehaviour
{
    [SerializeField]
    private int howManyDoYouWant;

    [SerializeField]
    private InventoryItem whatDoYouWant;

    void Start()
    {
        Inventory inventory = gameObject.GetComponent<Inventory>();
        for(int i = 0; i!=howManyDoYouWant; i++)
        {
            inventory.AddItem(whatDoYouWant);
        }
    }

}