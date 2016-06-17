using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;


public class VirtualStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {
    private Image bgImage;
    private Image joystickImg;
    private Vector3 inputVector;

    private void Start()
    {
        bgImage = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
    }


    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform, 
            ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImage.rectTransform.sizeDelta.y);



            if (pos.x < -0.485 || pos.x > 1 || pos.y < -0.5 || pos.y > 1)
            {
                pos.x = 0;
                pos.y = 0;
            }

            inputVector = new Vector3(pos.x*2, 0, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystickImg.rectTransform.anchoredPosition = new Vector3(
                inputVector.x * (bgImage.rectTransform.sizeDelta.x/3), 
                inputVector.z * (bgImage.rectTransform.sizeDelta.y/3));
        }
    }


    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }


    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }


    public float Horizontal()
    {
        if (inputVector.x != 0)
        {
            return inputVector.x;
            /*
            if (inputVector.z >= 0f)
                return inputVector.x;
            else
                return -inputVector.x;
                */
        }
        else
        {
            return Input.GetAxis("Horizontal1");
            /*
            if (Input.GetAxis("Vertical1") >= 0.0)
            {
                return Input.GetAxis("Horizontal1");
            }
            else
            {
                return -Input.GetAxis("Horizontal1");
            }
            */
        }
    }


    public float Vertical()
    {
        if (inputVector.z != 0)
            return inputVector.z;
        else
            return Input.GetAxis("Vertical1");
    }
}

