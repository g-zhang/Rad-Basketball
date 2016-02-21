using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {

    XBoxController controls = new XBoxController(1);
    public float HandMoveDistanceMult = .5f;

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
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 dir = GetRadius(Vector2.zero, controls.RightStick(), 1f) * HandMoveDistanceMult;
        Debug.DrawRay(gameObject.transform.position, dir, Color.cyan);

        gameObject.transform.position = parentPlayerObj.transform.position + new Vector3(dir.x, dir.y, -1f);
	}
}
