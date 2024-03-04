using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_PlayerTarget : MonoBehaviour
{
    Heal_Unitmovement heal_unitMove;
    public GameObject Des;
    public bool move_attack = false;
    Heal_fsm heal_fsm;

    void Start()
    {
        heal_unitMove = GetComponent<Heal_Unitmovement>();
        heal_fsm = GetComponent<Heal_fsm>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            heal_unitMove.aClick = true;
            heal_fsm.aclick = true;
        }
    }
}
