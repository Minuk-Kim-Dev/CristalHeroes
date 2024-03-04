using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BankedJewelText : MonoBehaviour
{
    Text knightText;
    Text archerText;
    Text priestText;
    Text knightInterest;
    Text archerInterest;    
    Text priestInterest;

    // Start is called before the first frame update
    void Start()
    {
        knightText = gameObject.transform.GetChild(0).GetComponent<Text>();
        archerText = gameObject.transform.GetChild(1).GetComponent<Text>();
        priestText = gameObject.transform.GetChild(2).GetComponent<Text>();
        knightInterest = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        archerInterest = gameObject.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        priestInterest = gameObject.transform.GetChild(2).GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        knightText.text = "저장된 기사 보석 : " + gameObject.GetComponentInParent<JewelBanking>().bankedKnightJewel + "개";
        archerText.text = "저장된 궁수 보석 : " + gameObject.GetComponentInParent<JewelBanking>().bankedArcherJewel + "개";
        priestText.text = "저장된 사제 보석 : " + gameObject.GetComponentInParent<JewelBanking>().bankedPriestJewel + "개";
        knightInterest.text = "이자로 받은 보석 : " + gameObject.GetComponentInParent<JewelBanking>().interestKnightJewel + "개";
        archerInterest.text = "이자로 받은 보석 : " + gameObject.GetComponentInParent<JewelBanking>().interestArcherJewel + "개";
        priestInterest.text = "이자로 받은 보석 : " + gameObject.GetComponentInParent<JewelBanking>().interestPriestJewel + "개";
    }
}
