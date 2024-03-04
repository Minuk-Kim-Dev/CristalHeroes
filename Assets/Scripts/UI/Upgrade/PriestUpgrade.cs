using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriestUpgrade : MonoBehaviour
{
    //자식 오브젝트 텍스트 관리 변수
    public Text detailObject;
    Text detail;
    public Text levelObject;
    Text level;
    public Text costObject;
    Text cost;

    public GameObject priestJewerly;    //사제 보석 관련 오브젝트
    public int priestJewelCnt;      //보유한 사제 보석 수

    public Button button;
    public GameObject lackNotice;

    public GameObject managerObject; //계통의 강화 단계, 비용, 보너스를 불러오기 위함
    PriestManager priestManager;

    private void Start()
    {
        button.onClick.AddListener(Upgrade);
        detail = detailObject.GetComponent<Text>();
        level = levelObject.GetComponent<Text>();
        cost = costObject.GetComponent<Text>();
        priestManager = managerObject.GetComponent<PriestManager>();
    }

    private void Update()
    {
        priestJewelCnt = priestJewerly.GetComponent<HealDragDrop>().count;
        level.text = priestManager.priestLevel.ToString();
        cost.text = "강화비용 : " + priestManager.priestCost;
        detail.text = "스킬 효율이 " + (int)(priestManager.priestBonus * 100) + "% → " + (int)((priestManager.priestBonus + 0.1) * 100) + "% 증가합니다.";
    }

    void Upgrade()
    {
        if (priestJewelCnt >= priestManager.priestCost)      //코스트가 충분하다면
        {
            priestJewerly.GetComponent<HealDragDrop>().count = priestJewerly.GetComponent<HealDragDrop>().count - priestManager.priestCost;
            Debug.Log("사제 계통을 업그레이드 했습니다.");
            priestManager.priestLevel += 1;     //레벨업
        }
        else    //비용이 부족하다면
        {
            //경고 UI같은거 띄우면 좋을 듯
            lackNotice.SetActive(true);
            Debug.Log("보석이 부족합니다.");
        }
    }
}
