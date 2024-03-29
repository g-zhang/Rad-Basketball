﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InControl;

public class StageSelectController : MonoBehaviour {

    [System.Serializable]
    public class Stage {
        public string name;
        public string title;
        public Sprite sprite;
    }

    public Stage[] stages;

    public GameObject stageItemPrefab;
    public GameObject cursorCornerPrefab;
    public Text text;

    private int cursorPosition = 0;
    private int mod = 0;

    private GameObject[] cursorCorners;
    private GameObject[] stageItems;
    private bool[] xReset;

    void Awake()
    {
    }

    void Start()
    {
        float x = 0;
        float y = 0;
        float padding = 2;
        float xOffset = this.gameObject.transform.position.x;
        float yOffset = this.gameObject.transform.position.y;

        stageItems = new GameObject[stages.Length];
        for (int i = 0; i < stages.Length; ++i)
        {
            Stage stage = stages[i];
            GameObject item = Instantiate<GameObject>(stageItemPrefab);
            SpriteRenderer renderer = item.GetComponent<SpriteRenderer>();
            renderer.sprite = stage.sprite;

            renderer.transform.position = new Vector3(xOffset + x, yOffset - y, 0);
            x += renderer.bounds.size.x + padding;
            y += padding;

            item.transform.parent = this.gameObject.transform;

            stageItems[i] = item;
        }

        cursorCorners = new GameObject[4];
        for (int i = 0; i < 4; ++i)
        {
            cursorCorners[i] = Instantiate<GameObject>(cursorCornerPrefab);
            cursorCorners[i].transform.rotation = Quaternion.Euler(new Vector3(0, 1, 90 * i));
        }

        xReset = new bool[InputManager.Devices.Count];
        for (int i = 0; i < xReset.Length; i++) {
            xReset[i] = false;
        }
    }

    void Update()
    {
        bool xboxLeft = false;
        bool xboxRight = false;
        bool xboxMenu = false;
        for (int i = 0; i < InputManager.Devices.Count; ++i) {
            InputDevice device = InputManager.Devices[i];
            device.Vibrate(0f);

            if (device.Direction.X < -0.9f && !xReset[i]) {
                xboxLeft = true;
                xReset[i] = true;
            } else if (device.Direction.X > 0.9f && !xReset[i]) {
                xboxRight = true;
                xReset[i] = true;
            }

            if (device.Direction.X < 0.1 && device.Direction.X > -0.1) {
                xReset[i] = false;
            }

            if (device.Action1) {
                xboxMenu = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || xboxLeft)
        {
            mod++;
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || xboxRight)
        {
            mod++;
            MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.Return) || xboxMenu)
        {
            Select();
        }

        UpdateCursorCorners();
    }

    void UpdateCursorCorners()
    {
        GameObject stageItem = stageItems[cursorPosition];
        Bounds bounds = stageItem.GetComponent<SpriteRenderer>().bounds;

        Vector3[] destinations = new Vector3[4];
        destinations[0] = new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y + bounds.extents.y, -1);
        destinations[1] = new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y, -1);
        destinations[2] = new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y - bounds.extents.y, -1);
        destinations[3] = new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y + bounds.extents.y, -1);

        Quaternion[] rotations = new Quaternion[4];
        rotations[0] = Quaternion.Euler(new Vector3(0, 0, 0));
        rotations[1] = Quaternion.Euler(new Vector3(0, 0, 90));
        rotations[2] = Quaternion.Euler(new Vector3(0, 0, 180));
        rotations[3] = Quaternion.Euler(new Vector3(0, 0, 270));

        for (int i = 0; i < 4; ++i)
        {
            int k = (i + mod) % 4;
            cursorCorners[k].transform.position = Vector3.Lerp(cursorCorners[k].transform.position, destinations[i], 0.1f);
            cursorCorners[k].transform.rotation = Quaternion.Lerp(cursorCorners[k].transform.rotation, rotations[i], 0.1f);
        }

        UpdateText();
    }

    private void MoveLeft()
    {
        if (cursorPosition > 0)
        {
            cursorPosition -= 1;
            UpdateText();
        }
    }

    private void MoveRight()
    {
        if (cursorPosition < stages.Length - 1)
        {
            cursorPosition += 1;
            UpdateText();
        }
    }

    private void Select()
    {
        Stage stage = stages[cursorPosition];
        UnityEngine.SceneManagement.SceneManager.LoadScene(stage.name);
    }

    private void UpdateText()
    {
        Stage stage = stages[cursorPosition];
        text.text = stage.title;
    }
}
