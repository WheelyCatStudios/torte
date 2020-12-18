using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject pin;

    public void OnPointerEnter(PointerEventData eventData)
    {
        pin.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pin.SetActive(false);
    }
}
