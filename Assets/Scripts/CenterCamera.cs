using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CenterCamera : MonoBehaviour {

    //Based on the script posted on Unity Answers:
    //http://answers.unity3d.com/questions/45029/keep-player-party-andor-targeted-enemies-in-camera.html

    float minOrthoSize = 5f;
    float camHeight = 1f;
    float speed = 1f;
    float zoomSpeed = 7f;


    List<GameObject> targets = new List<GameObject>();
    float currentDistance;
    float largestDistance;
    Camera theCamera;
    float height = 1.0f;
    float avgDistance;
    float distance = 0.0f;                    // Default Distance 
    float offset;


    void Start()
    {
        theCamera = GetComponent<Camera>();
        targets.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        targets.AddRange(GameObject.FindGameObjectsWithTag("Ball"));
    }

    void OnGUI()
    {
        //GUILayout.Label(" ");
        //GUILayout.Label("Objects of Interest = " + targets.Count.ToString());
    }

    void setCameraSize()
    {
        float largestDifference = returnLargestDifference();
        theCamera.orthographicSize = Mathf.Lerp(theCamera.orthographicSize, Mathf.Max(largestDifference * .6f, minOrthoSize), Time.deltaTime * zoomSpeed);
    }

    void LateUpdate()
    {
        targets.Clear();
        targets.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        targets.AddRange(GameObject.FindGameObjectsWithTag("Ball"));
        if (!GameObject.FindWithTag("Player"))
            return;
        Vector3 sum = new Vector3(0, 0, 0);
        for (var n = 0; n < targets.Count; n++)
        {
            sum += targets[n].transform.position;
        }
        var avgDistance = sum / targets.Count;


        //    Debug.Log(avgDistance);

        height = Mathf.Lerp(height, avgDistance.y + camHeight, Time.deltaTime * speed);
        Vector3 camPos = theCamera.transform.position;

        camPos.x = Mathf.Lerp(camPos.x, avgDistance.x, Time.deltaTime * speed);
        camPos.y = height;
        setCameraSize();

        theCamera.transform.position = camPos;
    }

    float returnLargestDifference()
    {
        currentDistance = 0.0f;
        largestDistance = 0.0f;
        for (var i = 0; i < targets.Count; i++)
        {
            for (var j = 0; j < targets.Count; j++)
            {
                currentDistance = Vector3.Distance(targets[i].transform.position, targets[j].transform.position);
                if (currentDistance > largestDistance)
                {
                    largestDistance = currentDistance;
                }
            }
        }
        return largestDistance;
    }
}
