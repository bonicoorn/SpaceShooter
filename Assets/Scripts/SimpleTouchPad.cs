using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    Vector2 origin;
    Vector2 direction;

    private void Awake()
    {
        direction = Vector2.zero;
    }



    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentPosition = eventData.position;
        Vector2 directionRaw = currentPosition - origin;
        direction = directionRaw.normalized;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        origin = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        direction = Vector2.zero;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }
}
