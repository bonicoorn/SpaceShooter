using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    Vector2 origin;
    Vector2 direction;
    Vector2 smothDirection;

    int pointerID;
    bool touched;


    public float smothing;

    private void Awake()
    {
        direction = Vector2.zero;
        touched = false;
    }



    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerID)
        {
            Vector2 currentPosition = eventData.position;
            Vector2 directionRaw = currentPosition - origin;
            direction = directionRaw.normalized;
        }
            
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!touched)
        {
            origin = eventData.position;
            touched = true;
            pointerID = eventData.pointerId;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.pointerId == pointerID)
        {
            direction = Vector2.zero;
            touched = false;
        }
    }

    public Vector2 GetDirection()
    {
        smothDirection = Vector2.MoveTowards(smothDirection, direction, smothing); 
        return smothDirection;
    }
}
