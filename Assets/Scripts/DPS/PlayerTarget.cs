using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerTarget : MonoBehaviour
{
    UnitMovement unitMove;
    public GameObject Des;
    public bool move_attack = false;
    DPS_fsm dps_fsm;
    void Start()
    {
        unitMove = GetComponent<UnitMovement>();
        dps_fsm = GetComponent<DPS_fsm>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            unitMove.aClick = true;
            dps_fsm.aclick = true;
        }
    }
}
