using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JewelBanking : MonoBehaviour
{
    TankDragDrop tankDragDrop;
    DpsDragDrop dpsDragDrop;
    HealDragDrop healDragDrop;

    public int bankedKnightJewel;   //저축된 기사 보석 수
    public int bankedArcherJewel;   //저축된 궁수 보석 수
    public int bankedPriestJewel;   //저축된 사제 보석 수
    public int interestKnightJewel;   //기사 보석 이자
    public int interestArcherJewel;   //궁수 보석 이자
    public int interestPriestJewel;   //사제 보석 이자

    public Button knightButton;     //기사보석 버튼
    public Button archerButton;     //궁수보석 버튼
    public Button priestButton;     //사제보석 버튼
    public Button getBackButton;       //보석 돌려받기 버튼
    public GameObject lackNotice;

    public GameObject knightJewelPrefab;
    public GameObject archerJewelPrefab;
    public GameObject priestJewelPrefab;


    // Start is called before the first frame update
    void Start()
    {
        tankDragDrop = GameObject.Find("TankJewely").GetComponent<TankDragDrop>();
        dpsDragDrop = GameObject.Find("DPSJewely").GetComponent<DpsDragDrop>();
        healDragDrop = GameObject.Find("HealJewely").GetComponent<HealDragDrop>();
        knightButton.onClick.AddListener(KnightBanking);
        archerButton.onClick.AddListener(ArcherBanking);
        priestButton.onClick.AddListener(PriestBanking);
        getBackButton.onClick.AddListener(GetBack);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void KnightBanking()        //기사 보석이 1개 이상이라면, 기사보석 1 뱅킹, 제단으로 보석이 이동하는 애니메이션
    {
        if(tankDragDrop.count > 0)
        {
            Debug.Log("기사 보석을 저축하셨습니다.");
            bankedKnightJewel++;
            tankDragDrop.count--;
            GameObject jewel = (GameObject)Instantiate(knightJewelPrefab, GameObject.Find("UI(Camera)").transform);
            jewel.transform.position = knightButton.GetComponent<RectTransform>().position;
        }
        else    //보석 모자라면 경고
        {
            Debug.Log("보석이 모자랍니다.");
            lackNotice.SetActive(true);
        }
    }

    void ArcherBanking()        //궁수 보석이 1개 이상이라면, 궁수보석 1 뱅킹, 제단으로 보석이 이동하는 애니메이션
    {
        if (dpsDragDrop.count > 0)
        {
            Debug.Log("궁수 보석을 저축하셨습니다.");
            bankedArcherJewel++;
            dpsDragDrop.count--;
            GameObject jewel = (GameObject)Instantiate(archerJewelPrefab, GameObject.Find("UI(Camera)").transform);
            jewel.transform.position = archerButton.GetComponent<RectTransform>().position;
        }
        else    //보석 모자라면 경고
        {
            Debug.Log("보석이 모자랍니다.");
            lackNotice.SetActive(true);
        }
    }

    void PriestBanking()        //사제 보석이 1개 이상이라면, 사제보석 1 뱅킹, 제단으로 보석이 이동하는 애니메이션
    {
        if (healDragDrop.count > 0)
        {
            Debug.Log("사제 보석을 저축하셨습니다.");
            bankedPriestJewel++;
            healDragDrop.count--;
            GameObject jewel = (GameObject)Instantiate(priestJewelPrefab, GameObject.Find("UI(Camera)").transform);
            jewel.transform.position = priestButton.GetComponent<RectTransform>().position;
        }
        else    //보석 모자라면 경고
        {
            Debug.Log("보석이 모자랍니다.");
            lackNotice.SetActive(true);
        }
    }

    void GetBack()      //누르면 제단에 저장해두었던 모든 보석이 이자와 함께 반환됨
    {
        Debug.Log("모아두었던 보석을 가져가셨습니다.");

        tankDragDrop.count = tankDragDrop.count + bankedKnightJewel;
        dpsDragDrop.count = dpsDragDrop.count + bankedArcherJewel;
        healDragDrop.count = healDragDrop.count + bankedPriestJewel;

        bankedKnightJewel = 0;
        bankedArcherJewel = 0;
        bankedPriestJewel = 0;
        interestKnightJewel = 0;
        interestArcherJewel = 0;
        interestPriestJewel = 0;
    }
}
