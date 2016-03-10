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
    public bool useKeyboard = false;

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
        if(!useKeyboard) //use xbox controller
        {
            float lx = 0f;
            float ly = 0f;
            if (inputDevice != null)
            {
                lx = inputDevice.Direction.X;
                ly = inputDevice.Direction.Y;
            }
            return new Vector2(lx, ly);
        }
        else //use WASD keys
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                dir.y += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                dir.y -= 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                dir.x -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                dir.x += 1;
            }
            return dir;
        }

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

    public bool LeftTrigger()
    {
        if (inputDevice == null)
        {
            return false;
        }
        return inputDevice.LeftTrigger.State;
    }

    public bool RightTrigger()
    {
        if (inputDevice == null)
        {
            return false;
        }
        return inputDevice.RightTrigger.State;
    }

    public bool LeftBumper()
    {
        if (useKeyboard) {
            return Input.GetKey(KeyCode.Q);
        } else {
            if (inputDevice == null)
            {
                return false;
            }
            return inputDevice.LeftBumper.State;
        }
    }

    public bool RightBumper()
    {
        if (useKeyboard) {
            return Input.GetKey(KeyCode.E);
        } else {
            if (inputDevice == null)
            {
                return false;
            }
            return inputDevice.RightBumper.State;
        }
    }

    public bool Select()
    {
        if (useKeyboard) {
            return Input.GetKey(KeyCode.Escape);
        } else {
            if (inputDevice == null) {
                return false;
            }
            return inputDevice.MenuWasPressed;
        }
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
