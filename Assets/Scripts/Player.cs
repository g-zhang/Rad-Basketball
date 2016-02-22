using UnityEngine;

public class Player : MonoBehaviour
{
    public int number;

    private XBoxController controller;

    void Awake()
    {
        controller = new XBoxController(number);
    }

    void FixedUpdate()
    {
        transform.position = transform.position + (Vector3) (0.3f * controller.LeftStick());

        float power = controller.Flick();
        if (power > 0.0f)
        {
            print("Flick " + power);
        }

        if (controller.RightTrigger())
        {
            print("Right trigger");
        }
    }
}
