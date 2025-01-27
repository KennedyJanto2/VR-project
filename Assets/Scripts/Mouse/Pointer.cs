using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pointer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TRIGGERED");

        IClickable obj = collision.gameObject.GetComponent<IClickable>();
        if (obj != null)
        {
            obj.Click();
        }
    }

    //one minor bug is that if the user moves the mouse and enters the trigger it sets the index, but if they don't click and move it outside of the trigger and just click then the next question still gets loaded.
    //As another easy fix added a OnTriggeExit2D and made it unset the index. 
    private void OnTriggerExit2D(Collider2D collision)
    {
        IClickable obj = collision.gameObject.GetComponent<IClickable>();
        if (obj != null)
        {
            obj.UnClick();
        }
    }

    public void SetUIPosition(Vector2 difference)
    {
        Vector3 mouseUIPos = this.GetComponent<RectTransform>().localPosition;
        Vector3 finalPos = new Vector3(difference.x + mouseUIPos.x, difference.y + mouseUIPos.y, mouseUIPos.z);
        this.GetComponent<RectTransform>().localPosition = finalPos;

        ClampMouseToUI();
    }

    void ClampMouseToUI()
    {
        RectTransform mouse = this.GetComponentInParent<RectTransform>();
        RectTransform canvas = this.transform.parent.gameObject.GetComponent<RectTransform>();

        Vector3 pos = this.GetComponent<RectTransform>().localPosition;

        Vector3 minPosition = canvas.rect.min - mouse.rect.min;
        Vector3 maxPosition = canvas.rect.max - mouse.rect.max;

        pos.x = Mathf.Clamp(mouse.localPosition.x, minPosition.x, maxPosition.x);
        pos.y = Mathf.Clamp(mouse.localPosition.y, minPosition.y, maxPosition.y);

        mouse.localPosition = pos;
    }
}
