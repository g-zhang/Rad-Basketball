using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {

    XBoxController controls;
    Vector2 rstickDir = Vector2.zero;

    public float HandMoveDistanceMult = .5f;

    [Header("Status")]
    public bool hasBall = false;
    public GameObject ball = null;
    public float shotCharge = 0;
    public GameObject parentPlayerObj;
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
    }

    void getHandPosition()
    {
        Vector2 dir = GetRadius(Vector2.zero, controls.RightStick(), 1f) * HandMoveDistanceMult;
        //Vector2 dir = new Vector2(1, 0) * HandMoveDistanceMult;
        rstickDir = dir;
        Debug.DrawRay(gameObject.transform.position, dir, Color.cyan);
        

        if (dir.magnitude < .25f)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, parentPlayerObj.transform.position + new Vector3(dir.x, dir.y, 0), .25f);
        }
        else
        {
            gameObject.transform.position = parentPlayerObj.transform.position + new Vector3(dir.x, dir.y, 0f);
        }
    }

    void holdBall()
    {
        if(hasBall)
        {
            Debug.DrawRay(ball.transform.position, Vector3.up * 5f, Color.blue);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ball"), true);
            ball.transform.position = gameObject.transform.position;
        } else
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ball"), false);
        }
    }

    void throwBall(Vector2 dir)
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        float magnitude = shotCharge > 1 ? 30 : shotCharge * 30;
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
            if (controls.RightTrigger())
            {
                shotCharge += Time.deltaTime;
                print("hello");
            }
            else if(shotCharge > 0)
                throwBall(rstickDir);
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
