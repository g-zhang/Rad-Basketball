using UnityEngine;

public class Player : MonoBehaviour
{
    public int number;

    private XBoxController controller;

    private int groundLayerMask;
    private int wallLayerMask;

    private Rigidbody2D body;

    void Awake()
    {
        controller = new XBoxController(number);

        groundLayerMask = LayerMask.GetMask("Ground");
        wallLayerMask = LayerMask.GetMask("Wall");

        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        controller.InputUpdate();
    }

    void FixedUpdate()
    {
        Vector2 velocity = body.velocity;
        Vector2 flick = controller.Flick();

        if (flick != Vector2.zero)
        {
            bool verticalFlick = Mathf.Abs(flick.y) >= Mathf.Abs(flick.x);

            if (Grounded() && flick.y > 0)
            {
                velocity.y = flick.magnitude;
            }

            float angle = Vector2.Angle(new Vector2(Mathf.Abs(flick.x), Mathf.Abs(flick.y)), Vector2.up);

            if (angle > 20f)
            {
                bool rightFlick = flick.x > 0;

                if (rightFlick && Walled(Vector2.left))
                {
                    velocity = flick * 0.85f;
                }
                else if (!rightFlick && Walled(Vector2.right))
                {
                    velocity = flick * 0.85f;
                }
            }
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

    private bool Walled(Vector2 direction)
    {
        return Physics2D.Raycast(transform.position, direction, 0.6f, groundLayerMask);
    }

    public XBoxController GetXBoxController()
    {
        return controller;
    }
}
