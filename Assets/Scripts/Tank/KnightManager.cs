using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightManager : MonoBehaviour
{
    //계통 강화 관리 변수
    public int knightLevel;     //기사 계통 강화 레벨
    public int knightCost;
    public int knightBonus;

    void Start()
    {
        knightLevel = 0;    //나중에 0으로 수정
    }

    void Update()
    {
        knightCost = 1 + (knightLevel * 2);     //레벨마다 업그레이드 비용 2씩 증가
        knightBonus = 0 + (knightLevel * 2);       //기본 0, 레벨마다 보너스 2씩 증가
    }
}
