using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ShootButton : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {
    private Image bgImage;
    private Image joystickImg;
    private Vector3 inputVector;
    private bool shooting = false;
    private bool keyDown = false;
    private bool keyUp = false;

    public bool isKeyDown()
    {
        if (keyDown == true)
        {
            keyDown = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isKeyUp()
    {
        if (keyUp == true)
        {
            keyUp = false;
            return true;
        }
        else
        {
            return false;
        }
    }


    public bool IsPressing() {
        return shooting == true;
    }


    private void Start()
    {
        shooting = false;
        bgImage = GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        
    }


    public virtual void OnPointerDown(PointerEventData ped)
    {
        if (IsInTouchRegion(ped)) {   
            keyDown = true;
            shooting = true;
        }
    }


    public virtual void OnPointerUp(PointerEventData ped)
    {
        if (IsInTouchRegion(ped))
        {
            keyUp = true;
            shooting = false;
        }
    }

    private bool IsInTouchRegion(PointerEventData ped)
    {
        Vector2 pos = new Vector2(-100, -100);
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform,
            ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImage.rectTransform.sizeDelta.y);

            if (pos.x <= 0.5 && pos.y >= -0.5 && pos.y <= 0.5 && pos.y >= -0.5)
                return true;
            
        }
        return false;
    }
}

