using UnityEngine;
using UnityEngine.EventSystems;
public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    // When highlighted with mouse.
    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<AudioManager>().Play("Menu Scroll");
    }
}
