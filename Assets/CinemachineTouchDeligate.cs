using Cinemachine;
using UnityEngine;
using System.Linq;


public class  CinemachineTouchDeligate : MonoBehaviour
{

    public float TouchSensitivity_x = 10f;
    public float TouchSensitivity_y = 10f;
    private  ButtonHover pad;

    // Use this for initialization
    void Start()
    {
        pad = GameObject.FindObjectOfType<ButtonHover>();
        CinemachineCore.GetInputAxis = HandleAxisInputDelegate;
    }

    float HandleAxisInputDelegate(string axisName)
    {
        //if (Input.mousePosition.x < Screen.width / 2)
        //{
        //    return 0f;
        //}
        
        switch (axisName)
        {
            case "Mouse X":
                return pad.DragX;// Input.GetAxis(axisName);

            case "Mouse Y":
                return pad.DragY; //Input.GetAxis(axisName);

            default:
                Debug.LogError("Input <" + axisName + "> not recognyzed.", this);
                break;
        }

        return 0f;
    }
}