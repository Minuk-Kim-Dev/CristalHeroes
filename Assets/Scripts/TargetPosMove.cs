using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosMove : MonoBehaviour
{
    Vector2 MousePosition;
    Camera Camera;
    UnitSelections unitSelections;
    public bool aclick = false;
    Vector2 prePos;
    Vector2 nextPos;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        unitSelections = GameObject.FindGameObjectWithTag("UnitSelection").transform.GetChild(0).GetComponent<UnitSelections>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            MousePosition = Input.mousePosition;
            MousePosition = Camera.ScreenToWorldPoint(MousePosition);
            transform.position = MousePosition;
        }
    }
}
