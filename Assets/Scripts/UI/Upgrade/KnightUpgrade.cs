using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightUpgrade : MonoBehaviour
{
    //자식 오브젝트 텍스트 관리 변수
    public Text detailObject;
    Text detail;
    public Text levelObject;
    Text level;
    public Text costObject;
    Text cost;

    public GameObject knightJewerly;    //기사 보석 관련 오브젝트
    public int knightJewelCnt;      //보유한 기사 보석 수

    public Button button;
    public GameObject lackNotice;

    public GameObject managerObject; //계통의 강화 단계, 비용, 보너스를 불러오기 위함
    KnightManager knightManager;

    private void Start()
    {
        button.onClick.AddListener(Upgrade);
        detail = detailObject.GetComponent<Text>();
        level = levelObject.GetComponent<Text>();
        cost = costObject.GetComponent<Text>();
        knightManager = managerObject.GetComponent<KnightManager>();
    }

    private void Update()
    {
        knightJewelCnt = knightJewerly.GetComponent<TankDragDrop>().count;
        level.text = knightManager.knightLevel.ToString();
        cost.text = "강화비용 : " + knightManager.knightCost;
        detail.text = "전투 시작 시 " + knightManager.knightBonus + " → " + (knightManager.knightBonus + 2) + " 의 보호막을 가집니다.";
    }

    void Upgrade()
    {
        if(knightJewelCnt >= knightManager.knightCost)      //코스트가 충분하다면
        {
            Debug.Log("기사 계통을 업그레이드 했습니다.");
            knightJewerly.GetComponent<TankDragDrop>().count = knightJewerly.GetComponent<TankDragDrop>().count - knightManager.knightCost;
            knightManager.knightLevel += 1;     //레벨업
        }
        else    //비용이 부족하다면
        {
            //경고 UI같은거 띄우면 좋을 듯
            lackNotice.SetActive(true);
            Debug.Log("보석이 부족합니다.");
        }
    }
}
