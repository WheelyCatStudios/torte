using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Animation")]
    public Animator anim;

    [Header("Visual")]
    public GameObject pin;

    [Header("Sound")]
    public AudioClip sound;

    bool hover;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;

        if(anim != null)
        {
            anim.SetBool("Hover", hover);
        }

        if(pin != null)
        {
            pin.SetActive(true);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;

        if (anim != null)
        {
            anim.SetBool("Hover", hover);
        }

        if (pin != null)
        {
            pin.SetActive(false);
        }

    }
}
