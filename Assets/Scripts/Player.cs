using UnityEngine;

public class Player : MonoBehaviour
{
    public int number;

    private XBoxController controller;

    private int groundLayerMask;

    private Rigidbody2D body;

    void Awake()
    {
        controller = new XBoxController(number);

        groundLayerMask = LayerMask.GetMask("Ground");

        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 velocity = body.velocity;

        float power = controller.Flick();
        if (Grounded() && power > 0.0f)
        {
            velocity.y = power;
        }

        float lerpScale = 0.05f;
        if (Grounded())
        {
            lerpScale = 0.2f;
        }

        velocity.x = Mathf.Lerp(velocity.x, 10 * controller.LeftStick().x, lerpScale);

        body.velocity = velocity;
    }

    bool Grounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundLayerMask);
    }

    public XBoxController GetXBoxController()
    {
        return controller;
    }
}
