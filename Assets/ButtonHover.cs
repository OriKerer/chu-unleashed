using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TouchControlsKit;

public class ButtonHover : TCKButton
{

    private bool pressed = false;
    private Vector2 drag;

    public float DragX
    {
        get { 
            var tmp = drag.x;
            drag.x = 0;
            return tmp / 2;
    }
    }

    public float DragY
    {
        get
        {
            var tmp = drag.y;
            drag.y = 0;
            return tmp / 5;
        }
    }
    public bool Pressed { get => pressed; }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void  OnMouseOver() 
    {
        pressed = true;   
    }
    public override void OnDrag(PointerEventData pointerData)
    {
        drag += pointerData.delta;
    }
    private void OnMouseEnter()
    {
        pressed = false;
    }
}
