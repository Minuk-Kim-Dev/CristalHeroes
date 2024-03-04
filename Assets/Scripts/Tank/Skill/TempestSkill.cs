using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempestSkill : MonoBehaviour
{
    Unit unit;


    // Start is called before the first frame update
    void Start()
    {
        unit = gameObject.GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<Tank_fsm>().DelayTime == 3.0f && unit.attackCnt >= 3)
        {
            //공격 속도 증가
            unit.attackCnt = 0;
            unit.usingSkill = true;
            gameObject.GetComponent<Tank_fsm>().DelayTime = 1.5f;
        }
        else if(gameObject.GetComponent<Tank_fsm>().DelayTime == 1.5f && unit.attackCnt >= 6)
        {
            //기본 공격 속도로 전환
            unit.attackCnt = 0;
            unit.usingSkill = false;
            gameObject.GetComponent<Tank_fsm>().DelayTime = 3.0f;
        }
    }
}
