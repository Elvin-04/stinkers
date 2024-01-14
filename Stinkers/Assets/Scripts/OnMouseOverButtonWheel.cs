using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseOverButtonWheel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private string action;

    public void OnPointerEnter(PointerEventData eventData)
    {
        controller.selectedButtonWheel = action;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        controller.selectedButtonWheel = "";
    }

}
