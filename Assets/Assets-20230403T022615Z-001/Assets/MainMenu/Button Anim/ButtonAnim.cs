using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Button button;
    [SerializeField] Animator buttonAnim;

    private Color deSelectColor = Color.white;
    private Color pressedColor = Color.gray;

    private ColorBlock cb; //colors used for buttons

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonAnim.SetBool("isSelected", true);
        buttonAnim.SetBool("isDeselected", false);
        buttonAnim.SetBool("isPressed", false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonAnim.SetBool("isDeselected", true);
        buttonAnim.SetBool("isSelected", false);
        buttonAnim.SetBool("isPressed", false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonAnim.SetBool("isPressed", true);
        buttonAnim.SetBool("isSelected", false);
        buttonAnim.SetBool("isDeselected", false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonAnim.SetBool("isDeselected", true);
        buttonAnim.SetBool("isSelected", false);
        buttonAnim.SetBool("isPressed", false);
    }

    public void WasClicked()
    {
        Debug.Log("Button was Clicked");
    }
}
