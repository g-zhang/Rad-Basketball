using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {

    XBoxController controls;
    Vector2 rstickDir = Vector2.zero;

    public float HandMoveDistanceMult = .5f;

    [Header("Status")]
    public bool hasBall = false;
    public GameObject ball = null;

    public GameObject parentPlayerObj;
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
        controls = parentPlayerObj.GetComponent<SceneController>().GetXBoxController();
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
            ball.transform.position = gameObject.transform.position;
        }
    }

    void throwBall(Vector2 dir)
    {
        if(hasBall)
        {
            hasBall = false;
            ball.GetComponent<Rigidbody2D>().velocity = dir;
            ball = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
        getHandPosition();

        holdBall();

        //temp way to let go of the ball until we decide on how to throw the ball and other physics
        if(controls.RightTrigger())
        {
            throwBall(rstickDir * 10f);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ball")
        {
            hasBall = true;
            ball = other.gameObject;
        }
    }
}
