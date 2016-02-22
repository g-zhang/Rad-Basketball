using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {

    XBoxController controls = new XBoxController(1);

    public float HandMoveDistanceMult = .5f;

    [Header("Status")]
    public bool hasBall = false;

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
        gameObject.transform.position = parentPlayerObj.transform.position + new Vector3(0, 0, -1f);

    }

    void getHandPosition()
    {
        //Vector2 dir = GetRadius(Vector2.zero, controls.RightStick(), 1f) * HandMoveDistanceMult;
        Vector2 dir = new Vector2(1, 0) * HandMoveDistanceMult;
        Debug.DrawRay(gameObject.transform.position, dir, Color.cyan);

        if (dir.magnitude < .25f)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, parentPlayerObj.transform.position + new Vector3(dir.x, dir.y, -1f), .25f);
        }
        else
        {
            gameObject.transform.position = parentPlayerObj.transform.position + new Vector3(dir.x, dir.y, -1f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        getHandPosition();


	}

    void OnTriggerEnter2D(Collider2D other)
    {
        print(other.tag);
    }
}
