using UnityEngine;
using InControl;

public class XBoxController
{
    private int playerNumber;
    private InputDevice inputDevice;

    // Last time stick was zeroed
    private float zeroTime;
    // Waiting to zero before next flick
    private bool flicked = false;

    public float flickScale = 0.23f;

    public XBoxController(int playerNumber)
    {
        this.playerNumber = playerNumber;
    }

    //this needs to be called in Update() of the monobehavior creating the instance of XBoxController
    public void InputUpdate()
    {
        inputDevice = (InputManager.Devices.Count > playerNumber) ? InputManager.Devices[playerNumber] : null;
    }

    public Vector2 LeftStick()
    {
        if(inputDevice == null)
        {
            return Vector2.zero;
        }
        return inputDevice.Direction.Vector;
    }

    public Vector2 RightStick()
    {
        if (inputDevice == null)
        {
            return Vector2.zero;
        }
        float rx = inputDevice.RightStick.X;
        float ry = inputDevice.RightStick.Y;
        return new Vector2(rx, ry);
    }

    public bool RightTrigger()
    {
        if (inputDevice == null)
        {
            return false;
        }
        return inputDevice.RightTrigger.State;
    }

    public Vector2 Flick()
    {
        float magnitude = LeftStick().y;
        float xmag = Mathf.Abs(LeftStick().x);

        if (magnitude < 0.2f)
        {
            flicked = false;
            zeroTime = Time.time;
            return Vector2.zero;
        }
        else if (!flicked && (magnitude > 0.8f || (magnitude > .3f && xmag < .85f)))
        {
            flicked = true;
            float duration = Time.time - zeroTime;
            float power = 1.0f / duration;
            return power * flickScale * LeftStick().normalized;
        }
        else
        {
            return Vector2.zero;
        }
    }
}
