using UnityEngine;

public class Player : MonoBehaviour
{
    public int number;
    public bool useKeyboardAndMouse = false;
    public GameObject HandPrefab;
    public Material HandColor;

    private XBoxController controller;
    private HandController Hand;

    private int groundLayerMask;
    private int wallLayerMask;

	private bool double_jump = false;
	private bool double_jumping = false;
	private bool extra_ball = false;

    private Rigidbody2D body;

    void Awake()
    {
        controller = new XBoxController(number);

        groundLayerMask = LayerMask.GetMask("Ground");
        wallLayerMask = LayerMask.GetMask("Wall");

        body = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //Hand = transform.Find("Hand").gameObject.GetComponent<HandController>();
        GameObject handObj = Instantiate(HandPrefab);
        handObj.transform.parent = gameObject.transform;
        handObj.GetComponent<Renderer>().material = HandColor;
        Hand = handObj.GetComponent<HandController>();
        Hand.parentPlayerObj = gameObject;
    }

    void Update()
    {
        controller.InputUpdate();
        controller.useKeyboard = useKeyboardAndMouse;
        Hand.useMouse = useKeyboardAndMouse;
    }

    void FixedUpdate()
    {
        Vector2 velocity = body.velocity;
        Vector2 flick = controller.Flick();

        if (flick != Vector2.zero)
        {
            bool verticalFlick = Mathf.Abs(flick.y) >= Mathf.Abs(flick.x);

			if (Grounded ())
				double_jumping = false;

			if (Grounded() && flick.y > 0)
            {
                velocity.y = flick.magnitude;
            }

			if (!Grounded () && flick.y > 0 && double_jump && !double_jumping) {
				velocity.y = flick.magnitude;
				double_jumping = true;
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

		//Ramp logic
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundLayerMask);
		if (hit.collider != null) {
			if (hit.transform.gameObject.tag == "Ramp") {
				print ("Player is on ramp");
				body.velocity *= 1.1f;
			}
		}
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

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "PowerUp_DoubleJump") {
			print ("got double jump!");
			double_jump = true;
			Destroy (col.gameObject);
		}

		if (col.gameObject.tag == "PowerUp_ExtraBall") {
			print ("got extra ball!");
			extra_ball = true;
			Destroy (col.gameObject);
		}
	}
}
