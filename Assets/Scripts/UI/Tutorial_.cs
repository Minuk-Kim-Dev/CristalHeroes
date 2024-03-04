using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_ : MonoBehaviour
{
    public int start = 0;
    public GameObject[] pop_up = new GameObject[18];
    public Control control;
    public TankDragDrop tdd;
    public DpsDragDrop ddd;
    public HealDragDrop hdd;
    public UnitSelections unitselections;
    public GameManager gm;
    public TargetList targetlist;
    public GameObject monster;
    public GameObject battlebutton;
    public GameObject shop;
    public GameObject blessing;
    public GameObject stage1;
    public GameObject overlay;
    void Start()
    {
        start = PlayerPrefs.GetInt("0");
    }

    // Update is called once per frame
    void Update()
    {
        if (start == 0) //ÆË¾÷1
        {
            pop_up[0].SetActive(true);
            battlebutton.SetActive(false);
            tdd.count = 1; ddd.count = 1; hdd.count = 1;
        }
        else if (start == 1) //ÆË¾÷2
        {
            if (control.playerlist.Count > 2)
            {
                NextPopup();
            }
        }
        else if (start == 2) //ÆË¾÷3
        {
            if (unitselections.unitsSelected.Count > 0)
            {
                NextPopup();
            }
        }
        else if (start == 3)//ÆË¾÷4
        {
            if (unitselections.unitsSelected.Count > 0)
            {
                if (Input.GetMouseButton(1))
                {
                    NextPopup();
                }
            }
        }
        else if (start == 4)//ÆË¾÷5
        {
            if (gm.isBattle)
            {
                NextPopup();
            }
        }
        else if (start == 5)//ÆË¾÷6
        {

        }
        else if (start == 6)//ÆË¾÷7 °ø°Ý ½ÃÀÛ
        {
            if (targetlist.targetAttack.Count > 0)
            {
                NextPopup();
            }
        }
        else if (start == 7)//ÆË¾÷8  Àû »ç»ì
        {
            if (control.enemylist.Count == 0)
            {
                NextPopup();
            }
        }
        else if (start == 8)//ÆË¾÷9-- »óÁ¡µîÀå
        {
        }
        else if (start == 9)//ÆË¾÷10
        {
        }
        else if (start == 10)//ÆË¾÷11
        {
        }
        else if (start == 11)//ÆË¾÷12
        {
        }
        else if (start == 12)//ÆË¾÷13
        {
        }
        else if (start == 13)//ÆË¾÷14
        {
        }
        else if (start == 14)//ÆË¾÷15
        {
        }
        else if (start == 15)//ÆË¾÷16
        {
        }
        else if (start == 16)//ÆË¾÷17
        {
        }
        else if (start == 17)//ÆË¾÷18
        {
        }
        else if (start == 18)
        {
            this.gameObject.SetActive(false); stage1.SetActive(true);
        }


    }

    public void rightClick()
    {
        start++;
        PlayerPrefs.SetInt("123", start);
        PopupManager();
    }

    public void Click()
    {
        pop_up[start].SetActive(false);
        Time.timeScale = 1;
    }
    void NextPopup()
    {
        rightClick();
    }

    void PopupManager()
    {
        if (start == 0) //ÆË¾÷1
        {
            pop_up[0].SetActive(true);
            Time.timeScale = 0;
        }
        else if (start == 1) //ÆË¾÷2
        {
            pop_up[1].SetActive(true); pop_up[0].SetActive(false);
            Time.timeScale = 0;
        }
        else if (start == 2) //ÆË¾÷3 ¼±ÅÃÇßÀ»¶§
        {
            pop_up[2].SetActive(true); pop_up[1].SetActive(false);
            Time.timeScale = 0;
        }
        else if (start == 3)//ÆË¾÷4 ¿òÁ÷¿´À»¶§ 
        {
            Time.timeScale = 0;
            pop_up[3].SetActive(true); pop_up[2].SetActive(false);
        }
        else if (start == 4)//ÆË¾÷5 ¹èÆ² ½ÃÀÛ ´­·¶À»¶§
        {
            battlebutton.SetActive(true);
            pop_up[4].SetActive(true); pop_up[3].SetActive(false);
        }
        else if (start == 5)//ÆË¾÷6 ¸ó½ºÅÍ »ý¼º
        {
            pop_up[5].SetActive(true); pop_up[4].SetActive(false); monster.SetActive(true);
            Time.timeScale = 0;
        }
        else if (start == 6)//ÆË¾÷7
        {
            pop_up[6].SetActive(true); pop_up[5].SetActive(false);
            Time.timeScale = 0;
        }
        else if (start == 7)//ÆË¾÷8
        {
            pop_up[7].SetActive(true); pop_up[6].SetActive(false);
            Time.timeScale = 0;
        }
        else if (start == 8)//ÆË¾÷9-- »óÁ¡µîÀå
        {
            shop.SetActive(true); overlay.SetActive(false);
            pop_up[8].SetActive(true); pop_up[7].SetActive(false);
            gm.isBattle = false;
        }
        else if (start == 9)//ÆË¾÷10
        {
            pop_up[9].SetActive(true); pop_up[8].SetActive(false);
            Time.timeScale = 0;
        }
        else if (start == 10)//ÆË¾÷11 -- Ãàº¹µîÀå
        {
            shop.SetActive(false); blessing.SetActive(true);
            pop_up[10].SetActive(true); pop_up[9].SetActive(false);
            Time.timeScale = 0;
        }
        else if (start == 11)//ÆË¾÷12
        {
            pop_up[11].SetActive(true); pop_up[10].SetActive(false);
        }
        else if (start == 12)//ÆË¾÷13
        {

            pop_up[12].SetActive(true); pop_up[11].SetActive(false);
        }
        else if (start == 13)//ÆË¾÷14
        {
            pop_up[13].SetActive(true); pop_up[12].SetActive(false);
        }
        else if (start == 14)//ÆË¾÷15
        {
            pop_up[14].SetActive(true); pop_up[13].SetActive(false);
        }
        else if (start == 15)//ÆË¾÷16
        {
            blessing.SetActive(false);
            pop_up[15].SetActive(true); pop_up[14].SetActive(false);
        }
        else if (start == 16)//ÆË¾÷17
        {
            pop_up[16].SetActive(true); pop_up[15].SetActive(false);
        }
        else if (start == 17)//ÆË¾÷18
        {
            pop_up[17].SetActive(true); pop_up[16].SetActive(false);
            Time.timeScale = 1;
        }
        else if (start == 18)
        {
            tdd.enabled = true;
            ddd.enabled = true;
            hdd.enabled = true;
            gm.UnitReposition();
            tdd.count = 2; ddd.count = 2; hdd.count = 2;
            overlay.SetActive(true); gm.readyArea.SetActive(true);
            foreach (GameObject unit in control.playerlist)
            {
                unit.GetComponent<Unit>().nowHp = 0;
                unit.GetComponent<Unit>().Die();
            }
            pop_up[17].SetActive(false);
        }
    }
}
