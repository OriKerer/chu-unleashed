/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using UnityEngine;
using UnityEngine.EventSystems;

namespace TouchControlsKit
{
    public class TCKButton : ControllerBase,
        IPointerExitHandler, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler
    {
        public bool swipeOut = false;

        [Label( "Normal Button" )]
        public Sprite normalSprite;
        [Label( "Pressed Button" )]
        public Sprite pressedSprite;

        public Color32 pressedColor = new Color32( 255, 255, 255, 165 );
        private const int hoverDelay = 3;
        private bool hovered = false;

        int pressedFrame = -1
            , releasedFrame = -1
            , clickedFrame = -1
            , hoverFrame = -1 - hoverDelay;

        public bool enableHover = true;

        bool pushedEffect = false;

        // isPRESSED
        internal bool isPRESSED {  get { return touchDown; } }
        // isDOWN
        internal bool isDOWN { get { return ( pressedFrame == Time.frameCount - 1 ); } }
        // isUP
        internal bool isUP { get { return ( releasedFrame == Time.frameCount - 1 ); } }
        // isCLICK
        internal bool isCLICK { get { return ( clickedFrame == Time.frameCount - 1 ); } }
        
        internal bool isHover {  get { return Time.frameCount - hoverDelay <= hoverFrame; } }

                
        // Update Position
        protected override void UpdatePosition( Vector2 touchPos )
        {
            base.UpdatePosition( touchPos );

            if( touchDown == false )
            {
                touchDown = true;
                touchPhase = ETouchPhase.Began;
                pressedFrame = Time.frameCount;

                ButtonDown();
            }            
        }


        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            bool mouseDown = Input.GetMouseButton(0);

            // Fixes problem when onPointerExit wont be triggered
            hovered &= mouseDown;

            if (hovered && mouseDown)
            {
                hoverFrame = Time.frameCount;
                if (enableHover)
                {
                    ButtonDown();
                }

            }

            if (Time.frameCount - hoverDelay > hoverFrame)
            {
                ControlReset();
            }
        }
        // Button Down
        protected void ButtonDown()
        {
            if (!pushedEffect)
            {
                baseImage.sprite = pressedSprite;
                baseImage.color = visible ? pressedColor : ( Color32 )Color.clear;
            }
            pushedEffect = true;
        }

        // Button Up
        protected void ButtonUp()
        {
            if(pushedEffect)
            {
                baseImage.sprite = normalSprite;
                baseImage.color = visible ? baseImageColor : (Color32)Color.clear;
            }
 
            pushedEffect = false;
        }

        // Control Reset
        protected override void ControlReset()
        {
            base.ControlReset();
            releasedFrame = Time.frameCount;
            ButtonUp();   
        }        

        // OnPointer Down
        public void OnPointerDown( PointerEventData pointerData )
        {
            if( touchDown == false )
            {
                touchId = pointerData.pointerId;
                UpdatePosition( pointerData.position );
            }
        }

        // OnDrag
        public virtual void OnDrag( PointerEventData pointerData )
        {
            if( Input.touchCount >= touchId && touchDown )
            {
                UpdatePosition( pointerData.position );
            }
        }

        // OnPointer Exit
        public void OnPointerExit( PointerEventData pointerData )
        {
            //if( swipeOut == false ) {
            //    OnPointerUp( pointerData );
            //}
            hovered = false;
        }

        // OnPointer Up
        public void OnPointerUp( PointerEventData pointerData )
        {
            ControlReset();
        }

        // OnPointer Click
        public void OnPointerClick( PointerEventData pointerData )
        {
            clickedFrame = Time.frameCount;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            hovered = true;
        }
    }
}