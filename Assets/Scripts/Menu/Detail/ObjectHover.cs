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

    public void OnPointerEnter(PointerEventData eventData) => SetHoverState(true);

    public void OnPointerExit(PointerEventData eventData) => SetHoverState(false);

    private void SetHoverState(bool hover)
    {
        if (anim)
            anim.SetBool("Hover", hover);

        if (pin)
            pin.SetActive(hover);
    }
}
