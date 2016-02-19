using UnityEngine;

public class XBoxController
{
    private int playerNumber;

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
}
