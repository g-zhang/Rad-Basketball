using UnityEngine;

public class Player : MonoBehaviour
{
    public int number;

    private XBoxController controller;

    // Last time stick was zeroed
    private float zeroTime;
    // Waiting to zero before next flick
    private bool flicked = false;

    void Awake()
    {
        controller = new XBoxController(number);
    }

    void FixedUpdate()
    {
        transform.position = transform.position + (Vector3) (0.3f * controller.LeftStick());

        float power = Flick();
        if (power > 0.0f)
        {
            print("Flick " + power);
        }

        if (controller.RightTrigger())
        {
            print("Right trigger");
        }
    }

    private float Flick()
    {
        float magnitude = controller.RightStick().magnitude;

        if (magnitude < 0.2f)
        {
            flicked = false;
            zeroTime = Time.time;
            return 0.0f;
        }
        else if (!flicked && magnitude > 0.9f)
        {
            flicked = true;
            float duration = Time.time - zeroTime;
            float power = 1.0f / duration;
            return power;
        }
        else
        {
            return 0.0f;
        }
    }
}
