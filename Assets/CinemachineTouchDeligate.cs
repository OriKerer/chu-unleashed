using Cinemachine;
using UnityEngine;
using System.Linq;


public class  CinemachineTouchDeligate : MonoBehaviour
{

    public float TouchSensitivity_x = 10f;
    public float TouchSensitivity_y = 10f;

    // Use this for initialization
    void Start()
    {
        CinemachineCore.GetInputAxis = HandleAxisInputDelegate;
    }

    float HandleAxisInputDelegate(string axisName)
    {
        var touches = Input.touches.Where(t => t.rawPosition.x > Screen.width);
        switch (axisName)
        {

            case "Mouse X":

                if (Input.touchCount > 0 && touches.Count() > 0)
                {
                    return touches.ElementAt(0).deltaPosition.x / TouchSensitivity_x;
                }
                else
                {
                    return Input.GetAxis(axisName);
                }

            case "Mouse Y":
                if (Input.touchCount > 0 && touches.Count() > 0)
                {
                    return touches.ElementAt(0).deltaPosition.y / TouchSensitivity_y;
                }
                else
                {
                    return Input.GetAxis(axisName);
                }

            default:
                Debug.LogError("Input <" + axisName + "> not recognyzed.", this);
                break;
        }

        return 0f;
    }
}