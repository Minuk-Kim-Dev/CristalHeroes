using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_PlayerTarget : MonoBehaviour
{
    Tank_UnitMovement tank_unitMove;
    public GameObject Des;
    public bool move_attack = false;
    Tank_fsm tank_fsm;
    void Start()
    {
        tank_unitMove = GetComponent<Tank_UnitMovement>();
        tank_fsm = GetComponent<Tank_fsm>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            tank_unitMove.aClick = true;
            tank_fsm.aclick = true;
        }
    }
}
