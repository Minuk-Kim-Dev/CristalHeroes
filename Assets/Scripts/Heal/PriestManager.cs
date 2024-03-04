using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestManager : MonoBehaviour
{
    //계통 강화 관리 변수
    public int priestLevel;     //사제 계통 강화 레벨
    public int priestCost;
    public float priestBonus;

    void Start()
    {
        priestLevel = 0;    //나중에 0으로 수정
    }

    void Update()
    {
        priestCost = 1 + (priestLevel * 2);     //레벨마다 업그레이드 비용 2씩 증가
        priestBonus = 0.0f + ((float)priestLevel * 0.1f);       //레벨마다 보너스 10%씩 증가
    }
}
