using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JewelLackNotice : MonoBehaviour
{
    GameObject lackJewel;
    public Button close;

    // Start is called before the first frame update
    void Start()
    {
        close.onClick.AddListener(Close);
        lackJewel = GameObject.Find("LackJewel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Close()
    {
        //닫기를 누르면 LackJewel(보석이 부족하다고 알리는 창)을 비활성화
        lackJewel.SetActive(false);
    }
}
