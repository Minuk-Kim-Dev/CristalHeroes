using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcherUpgrade : MonoBehaviour
{
    //자식 오브젝트 텍스트 관리 변수
    public Text detailObject;
    Text detail;
    public Text levelObject;
    Text level;
    public Text costObject;
    Text cost;

    public GameObject archerJewerly;    //궁수 보석 관련 오브젝트
    public int archerJewelCnt;      //보유한 궁수 보석 수

    public Button button;
    public GameObject lackNotice;

    public GameObject managerObject; //계통의 강화 단계, 비용, 보너스를 불러오기 위함
    ArcherManager archerManager;

    private void Start()
    {
        button.onClick.AddListener(Upgrade);
        detail = detailObject.GetComponent<Text>();
        level = levelObject.GetComponent<Text>();
        cost = costObject.GetComponent<Text>();
        archerManager = managerObject.GetComponent<ArcherManager>();
    }

    private void Update()
    {
        archerJewelCnt = archerJewerly.GetComponent<DpsDragDrop>().count;
        level.text = archerManager.archerLevel.ToString();
        cost.text = "강화비용 : " + archerManager.archerCost;
        detail.text = "적에게 세 번 기본 공격이 적중할 때 마다\n" + archerManager.archerBonus + " → " + (archerManager.archerBonus + 2) + " 의 추가 피해를 입힙니다.";
    }

    void Upgrade()
    {
        if (archerJewelCnt >= archerManager.archerCost)      //코스트가 충분하다면
        {
            archerJewerly.GetComponent<DpsDragDrop>().count = archerJewerly.GetComponent<DpsDragDrop>().count - archerManager.archerCost;
            Debug.Log("궁수 계통을 업그레이드 했습니다.");
            archerManager.archerLevel += 1;     //레벨업
        }
        else    //비용이 부족하다면
        {
            //경고 UI같은거 띄우면 좋을 듯
            lackNotice.SetActive(true);
            Debug.Log("보석이 부족합니다.");
        }
    }
}
