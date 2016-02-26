using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {

    XBoxController controls;
    Vector2 rstickDir = Vector2.zero;
    GameObject powerBar;

    [Header("Config")]
    public float HandMoveDistanceMult = .5f;
    public GameObject parentPlayerObj;

    [Header("Status")]
    public bool useMouse = false;
    public bool hasBall = false;
    public GameObject ball = null;
    public float shotCharge = 0;
    public float handCollCooldown = 0f;
    Vector3 defaultPos;

    // VJR(Virtual Joystick Region) Sample 
    // http://forum.unity3d.com/threads/vjr-virtual-joystick-region-sample.116076/
    private Vector2 GetRadius(Vector2 midPoint, Vector2 endPoint, float maxDistance)
    {
        Vector2 distance = endPoint;
        if (Vector2.Distance(midPoint, endPoint) > maxDistance)
        {
            distance = endPoint - midPoint; distance.Normalize();
            return (distance * maxDistance) + midPoint;
        }
        return distance;
    }

    // Use this for initialization
    void Start () {
        defaultPos = gameObject.transform.localPosition;
        gameObject.transform.position = parentPlayerObj.transform.position + new Vector3(0, 0, 0);
        controls = parentPlayerObj.GetComponent<Player>().GetXBoxController();
        powerBar = transform.Find("PowerBar").gameObject;
    }

    void getHandPosition()
    {
        if(useMouse)
        {
            Vector3 mousePos2D = Input.mousePosition;
            mousePos2D.z = -Camera.main.transform.position.z;
            Vector3 mousePos3d = Camera.main.ScreenToWorldPoint(mousePos2D);
            Vector3 dir3 = mousePos3d - parentPlayerObj.transform.position;
            Vector2 dir = new Vector2(dir3.x, dir3.y);
            rstickDir = GetRadius(Vector2.zero, dir, 1f) * HandMoveDistanceMult;
        }
        else
        {
            rstickDir = GetRadius(Vector2.zero, controls.RightStick(), 1f) * HandMoveDistanceMult;
        }
        
        Debug.DrawRay(gameObject.transform.position, rstickDir, Color.cyan);

        if (rstickDir.magnitude < .25f)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, parentPlayerObj.transform.position + new Vector3(rstickDir.x, rstickDir.y, 0), .25f);
        }
        else
        {
            gameObject.transform.position = parentPlayerObj.transform.position + new Vector3(rstickDir.x, rstickDir.y, 0f);
        }
        if(rstickDir != Vector2.zero)
        {
            gameObject.transform.localRotation = Quaternion.LookRotation(rstickDir);
        }    
    }

    void holdBall()
    {
        if(hasBall)
        {
            Debug.DrawRay(ball.transform.position, Vector3.up * 5f, Color.blue);
            ball.transform.position = gameObject.transform.position;
        }
    }

    float computePower()
    {
        return shotCharge > 1 ? 30 : shotCharge * 30;
    }

    void throwBall(Vector2 dir)
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        float magnitude = computePower();
        shotCharge = 0;
        hasBall = false;
        ball.GetComponent<Rigidbody2D>().velocity = dir.normalized * magnitude;
        ball.GetComponent<BallController>().owner = null;
        ball = null;
        handCollCooldown = .1f;
    }
	
	// Update is called once per frame
	void Update () {
        if(handCollCooldown > 0)
        {
            handCollCooldown -= Time.deltaTime;
        } else
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }

        getHandPosition();
        holdBall();

        //temp way to let go of the ball until we decide on how to throw the ball and other physics
        if (hasBall)
        {
            if (controls.RightTrigger() || useMouse && Input.GetMouseButton(0))
            {
                shotCharge += Time.deltaTime;

                //show power bar
                powerBar.GetComponent<LineRenderer>().enabled = true;
                powerBar.transform.localScale = new Vector3(powerBar.transform.localScale.x,
                                                            powerBar.transform.localScale.y,
                                                            computePower() * .25f);
            }
            else if(shotCharge > 0)
            {
                throwBall(rstickDir);
            }        
        } else
        {
            //hide powerbar
            powerBar.GetComponent<LineRenderer>().enabled = false;
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ball")
        {
            ball = other.gameObject;
            if(ball.GetComponent<BallController>().owner != null && ball.GetComponent<BallController>().owner != this.gameObject)
            {
                HandController prevowner = ball.GetComponent<BallController>().owner.GetComponent<HandController>();
                prevowner.hasBall = false;
                prevowner.ball = null;
            }
            ball.GetComponent<BallController>().owner = this.gameObject;
            hasBall = true;
        }
    }
}
