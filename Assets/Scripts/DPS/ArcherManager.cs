using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherManager : MonoBehaviour
{
    //계통 강화 관리 변수
    public int archerLevel;     //궁수 계통 강화 레벨
    public int archerCost;
    public int archerBonus;

    void Start()
    {
        archerLevel = 0;    //나중에 0으로 수정
    }

    void Update()
    {
        archerCost = 1 + (archerLevel * 2);     //레벨마다 업그레이드 비용 2씩 증가
        archerBonus = 0 + (archerLevel * 2);       //기본 10, 레벨마다 보너스 2씩 증가
    }
}
