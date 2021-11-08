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
        IPointerExitHandler, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerClickHandler
    {
        public bool swipeOut = false;

        [Label( "Normal Button" )]
        public Sprite normalSprite;
        [Label( "Pressed Button" )]
        public Sprite pressedSprite;

        public Color32 pressedColor = new Color32( 255, 255, 255, 165 );

        int pressedFrame = -1
            , releasedFrame = -1
            , clickedFrame = -1;

        bool hovered = false;

        static int someone_hovered = 0;
        // isPRESSED
        internal bool isPRESSED {  get { return touchDown; } }
        // isDOWN
        internal bool isDOWN { get { return ( pressedFrame == Time.frameCount - 1 ); } }
        // isUP
        internal bool isUP { get { return ( releasedFrame == Time.frameCount - 1 ); } }
        // isCLICK
        internal bool isCLICK { get { return ( clickedFrame == Time.frameCount - 1 ); } }
        
        internal bool isHover {  get { return hovered || touchDown; } }


                
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
        private void OnMouseOver()
        {
            hovered = true;
            if (touchDown == false)
            {
                touchDown = true;
                touchPhase = ETouchPhase.Began;
                pressedFrame = Time.frameCount;

                ButtonDown();
            }

        }

        private void OnMouseEnter()
        {
            someone_hovered++;
            OnMouseOver();
        }

        private void OnMouseExit()
        {
            someone_hovered--;
            if (someone_hovered > 0)
            {
                ControlReset();
            } 
            
            hovered = false;
        }


        private void Update()
        {
            if (!hovered && !touchDown && someone_hovered > 0)
            {
                ControlReset();
            }
        }
        // Button Down
        protected void ButtonDown()
        {
            baseImage.sprite = pressedSprite;
            baseImage.color = visible ? pressedColor : ( Color32 )Color.clear;
        }

        // Button Up
        protected void ButtonUp()
        {
            baseImage.sprite = normalSprite;
            baseImage.color = visible ? baseImageColor : ( Color32 )Color.clear;
        }

        // Control Reset
        protected override void ControlReset()
        {
            base.ControlReset();

            hovered = false;
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
            if( swipeOut == false ) {
                OnPointerUp( pointerData );
            }                
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
    };
}