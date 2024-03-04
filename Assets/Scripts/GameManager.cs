using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public List<GameObject> unitPos = new List<GameObject>();

    //스테이지 및 전투관리 변수
    public int stageIndex;
    public GameObject tutorial;
    public GameObject[] stages;     //2, 5, 8 스테이지일 경우 상점 등장
    public bool isBattle;   //전투단계일 경우 true, 준비단계일 경우 false
    public bool usedShop;
    public bool usedBlessing;
    public bool getInterest;
    public Button battleButton;

    GameObject us;
    UnitSelections unitselections;
    //상점 관리 변수
    public Button shopEndButton;
    public Button blessingEndButton;
    public GameObject shop;
    public GameObject Blessing;
    public GameObject bank;
    public Canvas overlayUI;    //상점 이용 중 다른 UI가 방해하지 않도록 추가
    public BlessingEnd Be;
    public Text stageText; 
    public Image stage;

    GameObject knightManager;
    public GameObject readyArea;

    public Skill skill;
    public Control control;
    public GameObject panel;
    public TargetList targetlist;

    public bool nextStage = true;

    private void Start()
    {
        knightManager = GameObject.Find("KnightManager");
        us = GameObject.Find("UnitSelections");
        unitselections = us.GetComponent<UnitSelections>();
        stageIndex = 0;
        isBattle = false;
        usedShop = false;
        getInterest = false;
        usedBlessing = false;
        battleButton.onClick.AddListener(StartBattle);
        shopEndButton.onClick.AddListener(EndShopping);
        readyArea = GameObject.Find("ReadyArea");
    }

    private void Update()
    {
        if (tutorial.activeSelf)
        {
            stageText.GetComponent<Text>().text = "Tutorial";
        }
        else
        {
            stageText.GetComponent<Text>().text = "Stage " + ((int)((stageIndex / 10) + 1)).ToString() + " - " + ((int)(stageIndex % 10) + 1).ToString();
        }
        int allUnitCnt = us.GetComponent<UnitSelections>().unitList.Count + stages[stageIndex].transform.GetChild(1).childCount;
        for (int i = 0; i < allUnitCnt; i++)
        {
            //for (int j = 0; j < us.GetComponent<UnitSelections>().unitList.Count; j++)
            //{
            //    if (!unitPos.Contains(us.GetComponent<UnitSelections>().unitList[j]))
            //    {
            //        unitPos.Add(us.GetComponent<UnitSelections>().unitList[j]);
            //    }
            //}
            //for (int k = 0; k < stages[stageIndex].transform.GetChild(1).childCount; k++)
            //{
            //    if (!unitPos.Contains(stages[stageIndex].transform.GetChild(1).GetChild(k).gameObject))
            //    {
            //        unitPos.Add(stages[stageIndex].transform.GetChild(1).GetChild(k).gameObject);
            //    }
            //}
        }
        if (stageIndex <= 3)
        {
            unitselections.unitLimit = 4;
        }
        else if (stageIndex > 3)
        {
            unitselections.unitLimit = 6;
        }
        else if (stageIndex > 8)
        {
            unitselections.unitLimit = 7;
        }
        else if (stageIndex > 13)
        {
            unitselections.unitLimit = 8;
        }
        //Cursor.lockState = CursorLockMode.Confined;     //마우스가 화면 밖으로 벗어나지 않도록 함
        if (stages[stageIndex].transform.GetChild(1).childCount <= 0)
        {
            //NextStage();
        }
        if (isBattle == false)       //전투상태가 아닐 경우(준비단계일 경우) 적유닛, 아군유닛의 FSM 비활성화, 버튼활성화
        {
            skill.death.Clear();
            skill.location.Clear();
            foreach (GameObject unit in control.playerlist)
            {
                string tag = unit.tag;
                if (targetlist.targetAttack.Count > 0)
                {
                    if (targetlist.targetAttack.Contains(unit))
                    {
                        targetlist.targetAttack.Remove(unit);
                        targetlist.targetIdle.Add(unit);
                    }
                }
                if (tag.StartsWith("tank"))
                {
                    unit.GetComponent<Tank_fsm>().stop = false;
                    unit.GetComponent<Tank_fsm>().enabled = false;
                    unit.GetComponent<Tank_fsm>().fight = false;
                }
                else if (tag.StartsWith("dps"))
                {
                    unit.GetComponent<DPS_fsm>().stop = false;
                    unit.GetComponent<DPS_fsm>().enabled = false;
                    unit.GetComponent<DPS_fsm>().fight = false;
                }
                else if (tag.StartsWith("heal"))
                {
                    unit.GetComponent<Heal_fsm>().stop = false;
                    unit.GetComponent<Heal_fsm>().enabled = false;
                    unit.GetComponent<Heal_fsm>().fight = false;
                }
            }
            if (!tutorial.activeSelf)
                battleButton.gameObject.SetActive(true);
        }
        else if (isBattle == true)       //전투상태일 경우, 적유닛, 아군유닛의 FSM활성화, 버튼 비활성화
        {
            for (int i = 0; i < stages[stageIndex].transform.GetChild(1).childCount; i++)
            {
                if (stages[stageIndex].transform.GetChild(1).GetChild(i).GetComponent<Enemy>().nowHp > 0)
                {
                    stages[stageIndex].transform.GetChild(1).GetChild(i).GetComponent<Enemy_FSM>().enabled = true;
                    stages[stageIndex].transform.GetChild(1).GetChild(i).GetChild(0).gameObject.SetActive(true);
                    stages[stageIndex].transform.GetChild(1).GetChild(i).GetChild(1).gameObject.SetActive(true);
                }
            }
            for (int i = 0; i < unitselections.unitList.Count; i++)
            {
                if (unitselections.unitList[i].CompareTag("tank") || unitselections.unitList[i].CompareTag("tanktank") ||
                    unitselections.unitList[i].CompareTag("tankdps") || unitselections.unitList[i].CompareTag("tankheal"))
                {
                    unitselections.unitList[i].GetComponent<Tank_fsm>().enabled = true;
                }
                else if (unitselections.unitList[i].CompareTag("dps") || unitselections.unitList[i].CompareTag("dpstank") ||
                    unitselections.unitList[i].CompareTag("dpsdps") || unitselections.unitList[i].CompareTag("dpsheal"))
                {
                    unitselections.unitList[i].GetComponent<DPS_fsm>().enabled = true;
                }
                else if (unitselections.unitList[i].CompareTag("heal") || unitselections.unitList[i].CompareTag("healtank") ||
                    unitselections.unitList[i].CompareTag("healdps") || unitselections.unitList[i].CompareTag("healheal"))
                {
                    unitselections.unitList[i].GetComponent<Heal_fsm>().enabled = true;
                }
            }
            battleButton.gameObject.SetActive(false);


            if (control.playerlist.Count == 0)
            {
                overlayUI.gameObject.SetActive(false);
                panel.gameObject.SetActive(true);
                stage.gameObject.SetActive(false);
            }
        }

        if ((stageIndex % 10 == 1 || stageIndex % 10 == 2 || stageIndex % 10 == 4 || stageIndex % 10 == 5 || stageIndex % 10 == 7 || stageIndex % 10 == 8) && usedShop == false && control.enemylist.Count == 0 && isBattle == true && !tutorial.activeSelf && stages[stageIndex].transform.GetChild(1).childCount <= 0)       //2, 5, 8스테이지일 경우 상점 등장
        {
            nextStage = false;
            for (int i = 0; i < unitselections.unitList.Count; i++)
            {
                unitselections.unitList[i].gameObject.SetActive(false);
            }
            int sum = bank.GetComponent<JewelBanking>().bankedKnightJewel + bank.GetComponent<JewelBanking>().bankedArcherJewel + bank.GetComponent<JewelBanking>().bankedPriestJewel;
            float interestRate = 1.0f;      //이자율
            int interest = (int)(Mathf.Round(((sum * interestRate) / 4.0f)));       //이자 = (저장된보석*이자율)/4.0
            int random = 0;
            if (getInterest == false)        //이자를 못받았으면 받기
            {
                Debug.Log("받을 이자는 " + interest + "개 입니다.");
                for (int i = 0; i < interest; i++)
                {
                    if (bank.GetComponent<JewelBanking>().bankedKnightJewel > bank.GetComponent<JewelBanking>().bankedArcherJewel && bank.GetComponent<JewelBanking>().bankedKnightJewel > bank.GetComponent<JewelBanking>().bankedPriestJewel)
                    {
                        if (bank.GetComponent<JewelBanking>().bankedArcherJewel > bank.GetComponent<JewelBanking>().bankedPriestJewel)
                        {
                            //기사 50%, 궁수 30%, 사제 20%
                            random = Random.Range(1, 101);
                            if (random <= 50)
                            {
                                bank.GetComponent<JewelBanking>().interestKnightJewel++;
                            }
                            else if (random > 50 && random <= 80)
                            {
                                bank.GetComponent<JewelBanking>().interestArcherJewel++;
                            }
                            else if (random > 80 && random <= 100)
                            {
                                bank.GetComponent<JewelBanking>().interestPriestJewel++;
                            }
                        }
                        else if (bank.GetComponent<JewelBanking>().bankedPriestJewel > bank.GetComponent<JewelBanking>().bankedArcherJewel)
                        {
                            //기사 50%, 궁수 20%, 사제 30%
                            random = Random.Range(1, 101);
                            if (random <= 50)
                            {
                                bank.GetComponent<JewelBanking>().interestKnightJewel++;
                            }
                            else if (random > 50 && random <= 70)
                            {
                                bank.GetComponent<JewelBanking>().interestArcherJewel++;
                            }
                            else if (random > 70 && random <= 100)
                            {
                                bank.GetComponent<JewelBanking>().interestPriestJewel++;
                            }
                        }
                        else if (bank.GetComponent<JewelBanking>().bankedPriestJewel == bank.GetComponent<JewelBanking>().bankedArcherJewel)
                        {
                            //기사 50%, 궁수, 사제 25%
                            random = Random.Range(1, 101);
                            if (random <= 50)
                            {
                                bank.GetComponent<JewelBanking>().interestKnightJewel++;
                            }
                            else if (random > 50 && random <= 75)
                            {
                                bank.GetComponent<JewelBanking>().interestArcherJewel++;
                            }
                            else if (random > 75 && random <= 100)
                            {
                                bank.GetComponent<JewelBanking>().interestPriestJewel++;
                            }
                        }
                    }
                    else if (bank.GetComponent<JewelBanking>().bankedArcherJewel > bank.GetComponent<JewelBanking>().bankedKnightJewel && bank.GetComponent<JewelBanking>().bankedArcherJewel > bank.GetComponent<JewelBanking>().bankedPriestJewel)
                    {
                        if (bank.GetComponent<JewelBanking>().bankedPriestJewel > bank.GetComponent<JewelBanking>().bankedKnightJewel)
                        {
                            //기사 20%, 궁수 50%, 사제 30%
                            random = Random.Range(1, 101);
                            if (random <= 50)
                            {
                                bank.GetComponent<JewelBanking>().interestArcherJewel++;
                            }
                            else if (random > 50 && random <= 80)
                            {
                                bank.GetComponent<JewelBanking>().interestPriestJewel++;
                            }
                            else if (random > 80 && random <= 100)
                            {
                                bank.GetComponent<JewelBanking>().interestKnightJewel++;
                            }
                        }
                        else if (bank.GetComponent<JewelBanking>().bankedKnightJewel > bank.GetComponent<JewelBanking>().bankedPriestJewel)
                        {
                            //기사 30%, 궁수 50%, 사제 20%
                            random = Random.Range(1, 101);
                            if (random <= 50)
                            {
                                bank.GetComponent<JewelBanking>().interestArcherJewel++;
                            }
                            else if (random > 50 && random <= 70)
                            {
                                bank.GetComponent<JewelBanking>().interestPriestJewel++;
                            }
                            else if (random > 70 && random <= 100)
                            {
                                bank.GetComponent<JewelBanking>().interestKnightJewel++;
                            }
                        }
                        else if (bank.GetComponent<JewelBanking>().bankedPriestJewel == bank.GetComponent<JewelBanking>().bankedKnightJewel)
                        {
                            //기사 25%, 궁수 50%, 사제 25%
                            random = Random.Range(1, 101);
                            if (random <= 50)
                            {
                                bank.GetComponent<JewelBanking>().interestArcherJewel++;
                            }
                            else if (random > 50 && random <= 75)
                            {
                                bank.GetComponent<JewelBanking>().interestKnightJewel++;
                            }
                            else if (random > 75 && random <= 100)
                            {
                                bank.GetComponent<JewelBanking>().interestPriestJewel++;
                            }
                        }
                    }
                    else if (bank.GetComponent<JewelBanking>().bankedPriestJewel > bank.GetComponent<JewelBanking>().bankedKnightJewel && bank.GetComponent<JewelBanking>().bankedPriestJewel > bank.GetComponent<JewelBanking>().bankedArcherJewel)
                    {
                        if (bank.GetComponent<JewelBanking>().bankedArcherJewel > bank.GetComponent<JewelBanking>().bankedKnightJewel)
                        {
                            //기사 20%, 궁수 30%, 사제 50%
                            random = Random.Range(1, 101);
                            if (random <= 50)
                            {
                                bank.GetComponent<JewelBanking>().interestPriestJewel++;
                            }
                            else if (random > 50 && random <= 80)
                            {
                                bank.GetComponent<JewelBanking>().interestArcherJewel++;
                            }
                            else if (random > 80 && random <= 100)
                            {
                                bank.GetComponent<JewelBanking>().interestKnightJewel++;
                            }
                        }
                        else if (bank.GetComponent<JewelBanking>().bankedKnightJewel > bank.GetComponent<JewelBanking>().bankedPriestJewel)
                        {
                            //기사 30%, 궁수 20%, 사제 50%
                            random = Random.Range(1, 101);
                            if (random <= 50)
                            {
                                bank.GetComponent<JewelBanking>().interestPriestJewel++;
                            }
                            else if (random > 50 && random <= 70)
                            {
                                bank.GetComponent<JewelBanking>().interestArcherJewel++;
                            }
                            else if (random > 70 && random <= 100)
                            {
                                bank.GetComponent<JewelBanking>().interestKnightJewel++;
                            }
                        }
                        else if (bank.GetComponent<JewelBanking>().bankedPriestJewel == bank.GetComponent<JewelBanking>().bankedKnightJewel)
                        {
                            //기사 25%, 궁수 25%, 사제 50%
                            random = Random.Range(1, 101);
                            if (random <= 50)
                            {
                                bank.GetComponent<JewelBanking>().interestPriestJewel++;
                            }
                            else if (random > 50 && random <= 75)
                            {
                                bank.GetComponent<JewelBanking>().interestKnightJewel++;
                            }
                            else if (random > 75 && random <= 100)
                            {
                                bank.GetComponent<JewelBanking>().interestArcherJewel++;
                            }
                        }
                    }
                    else if (bank.GetComponent<JewelBanking>().bankedKnightJewel == bank.GetComponent<JewelBanking>().bankedArcherJewel && bank.GetComponent<JewelBanking>().bankedArcherJewel == bank.GetComponent<JewelBanking>().bankedPriestJewel)
                    {
                        //기사, 궁수, 사제 33.33...%
                        random = Random.Range(1, 4);
                        if (random == 1)
                        {
                            bank.GetComponent<JewelBanking>().interestPriestJewel++;
                        }
                        else if (random == 2)
                        {
                            bank.GetComponent<JewelBanking>().interestKnightJewel++;
                        }
                        else if (random == 3)
                        {
                            bank.GetComponent<JewelBanking>().interestArcherJewel++;
                        }
                    }
                }
                bank.GetComponent<JewelBanking>().bankedKnightJewel = bank.GetComponent<JewelBanking>().bankedKnightJewel + bank.GetComponent<JewelBanking>().interestKnightJewel;
                bank.GetComponent<JewelBanking>().bankedArcherJewel = bank.GetComponent<JewelBanking>().bankedArcherJewel + bank.GetComponent<JewelBanking>().interestArcherJewel;
                bank.GetComponent<JewelBanking>().bankedPriestJewel = bank.GetComponent<JewelBanking>().bankedPriestJewel + bank.GetComponent<JewelBanking>().interestPriestJewel;
                getInterest = true;
            }

            shop.gameObject.SetActive(true);
            overlayUI.gameObject.SetActive(false);
            battleButton.gameObject.SetActive(false);
        }
        else if ((stageIndex % 10 == 3 || stageIndex % 10 == 6) && usedBlessing == false && control.enemylist.Count == 0 && isBattle == true
            && !tutorial.activeSelf && stages[stageIndex].transform.GetChild(1).childCount <= 0)       //4, 7 스테이지일 경우 축복 등장
        {
            Blessing.gameObject.SetActive(true);
            nextStage = false;
            for (int i = 0; i < unitselections.unitList.Count; i++)
            {
                unitselections.unitList[i].gameObject.SetActive(false);
            }
            stage.gameObject.SetActive(false);
            overlayUI.gameObject.SetActive(false);
            battleButton.gameObject.SetActive(false);
        }
        else if (stages[stageIndex].transform.GetChild(1).childCount <= 0 && nextStage == true)
        {
            foreach (GameObject unit in unitselections.unitList)
            {
                if (unit.GetComponent<Unit>().shieldBar != null)
                {
                    unit.GetComponent<Unit>().shield = 0;
                    Destroy(unit.GetComponent<Unit>().shieldBar.gameObject);
                }
            }
            NextStage();
        }
    }

    public void UnitReposition()
    {
        GameObject[] projectileObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in projectileObjects)
        {
            if (obj.name.StartsWith("projectile"))
            {
                Destroy(obj);
            }
        }
        int vertical = 0, horizontal = 0;
        Vector3 repos;
        //스테이지가 이동될 때, 유닛들의 위치를 초기화
        for (int i = 0; i < unitselections.unitList.Count; i++)
        {
            string tag = unitselections.unitList[i].tag;
            repos = new Vector3(-5 + (horizontal * 2), 2 + (vertical * 2), 0);
            if (tag.StartsWith("tank"))
            {
                unitselections.unitList[i].GetComponent<Transform>().localPosition = repos;
                unitselections.unitList[i].GetComponent<Tank_UnitMovement>().FinalNodeList.Clear();
                unitselections.unitList[i].GetComponent<Tank_UnitMovement>().targetPos = new Vector2Int((int)repos.x, (int)repos.y);
                unitselections.unitList[i].GetComponent<Tank_UnitMovement>().TargetTR = new Vector2Int((int)repos.x, (int)repos.y);
            }
            else if (tag.StartsWith("dps"))
            {
                unitselections.unitList[i].GetComponent<Transform>().localPosition = repos;
                unitselections.unitList[i].GetComponent<UnitMovement>().FinalNodeList.Clear();
                unitselections.unitList[i].GetComponent<UnitMovement>().targetPos = new Vector2Int((int)repos.x, (int)repos.y);
                unitselections.unitList[i].GetComponent<UnitMovement>().TargetTR = new Vector2Int((int)repos.x, (int)repos.y);
            }
            else if (tag.StartsWith("heal"))
            {
                unitselections.unitList[i].GetComponent<Transform>().localPosition = repos;
                unitselections.unitList[i].GetComponent<Heal_Unitmovement>().FinalNodeList.Clear();
                unitselections.unitList[i].GetComponent<Heal_Unitmovement>().targetPos = new Vector2Int((int)repos.x, (int)repos.y);
                unitselections.unitList[i].GetComponent<Heal_Unitmovement>().TargetTR = new Vector2Int((int)repos.x, (int)repos.y);
            }
            horizontal++;
            if (horizontal > 5)
            {
                vertical++;
                horizontal = 0;
            }
        }
    }

    public void NextStage()
    {
        GameObject[] projectileObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in projectileObjects)
        {
            if (obj.name.StartsWith("projectile"))
            {
                Destroy(obj);
            }
        }
        isBattle = false;
        UnitReposition();
        //현재 스테이지를 종료하고 다음 스테이지로 이동
        if (stageIndex < stages.Length - 1)
        {
            stages[stageIndex++].SetActive(false);
            for (int i = 0; i < stages[stageIndex].transform.GetChild(1).childCount; i++)
            {
                stages[stageIndex].transform.GetChild(1).GetChild(i).GetComponent<Enemy_FSM>().enabled = false;
            }
            stages[stageIndex].SetActive(true);
            usedShop = false;
            usedBlessing = false;
            readyArea.SetActive(true);
        }
        else
        {
            //모든 스테이지를 클리어(게임 클리어)

        }
    }

    void StartBattle()
    {
        if (unitselections.unitList.Count > 0)
        {
            //전투 시작하면 기사 쉴드 초기화
            for (int i = 0; i < unitselections.unitList.Count; i++)
            {
                if (unitselections.unitList[i].CompareTag("tank") || unitselections.unitList[i].CompareTag("tanktank") ||
                    unitselections.unitList[i].CompareTag("tankdps") || unitselections.unitList[i].CompareTag("tankheal"))
                {
                    unitselections.unitList[i].GetComponent<Unit>().shield = knightManager.GetComponent<KnightManager>().knightBonus;
                }
            }
            Debug.Log("전투를 시작합니다.");
            readyArea.SetActive(false);
            isBattle = true;
        }
    }

    void EndShopping()
    {
        Debug.Log("상점을 나갑니다.");
        usedShop = true;
        getInterest = false;
        shop.gameObject.SetActive(false);
        overlayUI.gameObject.SetActive(true);
        battleButton.gameObject.SetActive(true);
        for (int i = 0; i < unitselections.unitList.Count; i++)
        {
            unitselections.unitList[i].gameObject.SetActive(true);
        }
        nextStage = true;
    }

    public void EndBlessing()
    {
        Debug.Log("축복을 나갑니다.");
        usedBlessing = true;
        Blessing.gameObject.SetActive(false);
        overlayUI.gameObject.SetActive(true);
        battleButton.gameObject.SetActive(true);
        stage.gameObject.SetActive(true);
        for (int i = 0; i < unitselections.unitList.Count; i++)
        {
            unitselections.unitList[i].gameObject.SetActive(true);
        }
        Be.dps();
        Be.heal();
        Be.tank();
        nextStage = true;
    }
}