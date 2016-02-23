using UnityEngine;

public class XBoxController
{
    private int playerNumber;

    // Last time stick was zeroed
    private float zeroTime;
    // Waiting to zero before next flick
    private bool flicked = false;

    public float flickScale = 0.23f;

    public XBoxController(int playerNumber)
    {
        this.playerNumber = playerNumber;
    }

    public Vector2 LeftStick()
    {
        float lx = Input.GetAxis("P" + playerNumber + "LX");
        float ly = Input.GetAxis("P" + playerNumber + "LY");
        return new Vector2(lx, ly);
    }

    public Vector2 RightStick()
    {
        float rx = Input.GetAxis("P" + playerNumber + "RX");
        float ry = Input.GetAxis("P" + playerNumber + "RY");
        return new Vector2(rx, ry);
    }

    public bool RightTrigger()
    {
        float rt = Input.GetAxis("P" + playerNumber + "RT");
        return rt < -0.5f;
    }

    public Vector2 Flick()
    {
        float magnitude = LeftStick().y;

        if (magnitude < 0.2f)
        {
            flicked = false;
            zeroTime = Time.time;
            return Vector2.zero;
        }
        else if (!flicked && magnitude > 0.9f)
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
