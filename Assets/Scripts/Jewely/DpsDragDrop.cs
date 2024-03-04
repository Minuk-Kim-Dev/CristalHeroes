using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DpsDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject effect;
    GameObject drag;
    GameObject box;
    public Image limit;
    public Image lack;

    float moveRate;
    public bool Dragging;
    public GameObject prfUnit;      //소환될 유닛 오브젝트
    public GameObject evolutionTank;    //진화될 딜러탱커 프리팹
    public GameObject evolutionDps;     //진화될 딜러딜러 프리팹
    public GameObject evolutionHeal;    //진화될 딜러힐러 프리팹
    public Vector3 startPos;    //보석의 기존 위치
    public Vector3 endPos;      //유닛이 소환될 위치 
    public GameObject slot;
    public bool canMove;    //보석이 드래그가 가능한 상태인지
    public int count;       //보석 개수
    public bool reinforce;    //true일때 유닛 강화
    public bool evolve;     //true일때 유닛 진화
    public GameObject evolveTarget;
    public Vector3 evolvePos;       //진화유닛 위치 저장할 변수
    public GameObject reinforceTarget;
    public Text jewelyCount;    //보석 개수 출력할 UI -  수동으로 설정
    public GameObject unitSelections;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    GameObject object_control;
    GameObject targetList;
    GameObject gameManager;

    float MaxDistance = 15f;
    Vector3 MousePosition;
    private Camera myCam;

    void Start()
    {
        moveRate = 0.666f;
        myCam = Camera.main;
        evolve = false;
        reinforce = false;
        Dragging = false;
        count = 2;
        canMove = false;

        unitSelections = GameObject.Find("UnitSelections");
        drag = GameObject.Find("UnitSelectionSystem");
        box = GameObject.Find("BoxSelectCanvas");

        startPos = transform.position;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        object_control = GameObject.Find("Object_control");
        targetList = GameObject.Find("TargetList");
        gameManager = GameObject.Find("GameManager");
    }
    void Update()
    {
        //보석 갯수 카운트
        jewelyCount.text = count.ToString();

        //보석 드래그 중 마우스 우클릭하면 취소
        if (Dragging == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                rectTransform.position = startPos;
                canMove = false;

                //보석 초기 세팅으로 초기화
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
                rectTransform.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
                count++;
            }
        }

        if (count > 0)
        {
            this.gameObject.GetComponent<DpsDragDrop>().enabled = true;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canMove = true;
        rectTransform.anchoredPosition = rectTransform.anchoredPosition + eventData.delta * moveRate;

        //드래그 박스 비활성화
        drag.SetActive(false);
        box.SetActive(false);

        Dragging = true;

        //보석 드래그 중 색 연해지게 하기
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        //보석 드래그 중 크기 작아지기
        rectTransform.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 1);

        //보석 개수 감소
        count--;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canMove)
        {

            //보석 위치 드래그 이동
            rectTransform.anchoredPosition = rectTransform.anchoredPosition + eventData.delta * moveRate;

            //endPos == 유닛이 소환될 위치
            endPos = transform.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Dragging = false;

        //보석 초기 세팅으로 초기화
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        rectTransform.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
        if (gameManager.GetComponent<GameManager>().isBattle == false)
        {
            MousePosition = Input.mousePosition;
            MousePosition = myCam.ScreenToWorldPoint(MousePosition);
            LayerMask layerMask = LayerMask.GetMask("ReadyArea");
            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance, layerMask);
            Physics2D.queriesHitTriggers = true;
            if (hit.collider != null)
            {
                if (count >= 0)
                {
                    //유닛 생성
                    if (slot.GetComponent<JewerlySlot>().canSummon == true && unitSelections.GetComponent<UnitSelections>().unitOver == false && canMove == true && reinforce == false && evolve == false)
                    {
                        GameObject unit = (GameObject)Instantiate(prfUnit);
                        unit.transform.position = endPos;
                        UnitSelections.Instance.unitList.Add(unit.gameObject);

                        //컴포넌트 설정
                        unit.transform.GetComponent<UnitMovement>().Des = GameObject.Find("Destination");
                        unit.transform.GetComponent<Unit>().canvas = GameObject.Find("UI(Overlay)");
                        unit.transform.GetComponent<Unit>().newChar = true;
                        unit.transform.GetComponent<PlayerTarget>().Des = GameObject.Find("Destination");
                    }
                    else if (slot.GetComponent<JewerlySlot>().canSummon == false && canMove == true)
                    {
                        count++;
                    }
                    else if (canMove == true && reinforce == true)   //유닛 강화
                    {
                        if (reinforceTarget.GetComponent<Unit>().upgradeCnt == 0)   //유닛 강화가 1단계 이하일 경우
                        {
                            reinforceTarget.GetComponent<Unit>().dmg += 3.5f;        //공격력 5 증가
                            reinforceTarget.GetComponent<Unit>().upgradeCnt++;
                            GameObject upgrade = Instantiate(effect);
                            upgrade.GetComponent<Transform>().position = new Vector3(reinforceTarget.GetComponent<Transform>().position.x, reinforceTarget.GetComponent<Transform>().position.y + 1.5f, 0);
                            Destroy(reinforceTarget.GetComponent<Unit>().upgradeBar.gameObject);
                            reinforceTarget.GetComponent<Unit>().upgradeBar = Instantiate(reinforceTarget.GetComponent<Unit>().prfUpgradeLevel[reinforceTarget.GetComponent<Unit>().upgradeCnt], reinforceTarget.GetComponent<Unit>().canvas.transform).GetComponent<RectTransform>();
                        }
                        else if (reinforceTarget.GetComponent<Unit>().upgradeCnt == 1)   //유닛 강화가 2단계 이하일 경우
                        {
                            reinforceTarget.GetComponent<Unit>().atkSpeed -= 0.5f;        //공격속도 증가
                            reinforceTarget.GetComponent<Unit>().upgradeCnt++;
                            GameObject upgrade = Instantiate(effect);
                            upgrade.GetComponent<Transform>().position = new Vector3(reinforceTarget.GetComponent<Transform>().position.x, reinforceTarget.GetComponent<Transform>().position.y + 1.5f, 0);
                            Destroy(reinforceTarget.GetComponent<Unit>().upgradeBar.gameObject);
                            reinforceTarget.GetComponent<Unit>().upgradeBar = Instantiate(reinforceTarget.GetComponent<Unit>().prfUpgradeLevel[reinforceTarget.GetComponent<Unit>().upgradeCnt], reinforceTarget.GetComponent<Unit>().canvas.transform).GetComponent<RectTransform>();
                        }
                        else if (reinforceTarget.GetComponent<Unit>().upgradeCnt >= 2)
                        {
                            count++;
                        }
                    }
                    else if (evolve == true)        //유닛 진화
                    {
                        unitSelections.GetComponent<UnitSelections>().unitsSelected.Remove(evolveTarget.gameObject);
                        if (evolveTarget.CompareTag("tank") && evolveTarget.GetComponent<Unit>().evolved == false)
                        {
                            evolvePos = evolveTarget.transform.position;
                            Destroy(evolveTarget.GetComponent<Unit>().hpBar.gameObject);
                            Destroy(evolveTarget.GetComponent<Unit>().mpBar.gameObject);
                            if (evolveTarget.GetComponent<Unit>().shieldBar != null)
                            {
                                Destroy(evolveTarget.GetComponent<Unit>().shieldBar.gameObject);
                            }
                            Destroy(evolveTarget.GetComponent<Unit>().upgradeBar.gameObject);
                            object_control.GetComponent<Control>().playerlist.Remove(evolveTarget.gameObject);
                            object_control.GetComponent<Control>().attacking.Remove(evolveTarget.gameObject);
                            targetList.GetComponent<TargetList>().targetIdle.Remove(evolveTarget.gameObject);
                            targetList.GetComponent<TargetList>().targetAttack.Remove(evolveTarget.gameObject);
                            Destroy(evolveTarget);
                            UnitSelections.Instance.unitList.Remove(evolveTarget);
                            UnitSelections.Instance.unitsSelected.Remove(evolveTarget);
                            GameObject unit = (GameObject)Instantiate(evolutionTank);
                            unit.transform.position = evolvePos;
                            unit.GetComponent<Unit>().SetUnitStatus(160, 10, 1.8f, 2.5f, 4.8f, 0, 40);
                            UnitSelections.Instance.unitList.Add(unit.gameObject);

                            GameObject upgrade = Instantiate(effect);
                            upgrade.GetComponent<Transform>().position = new Vector3(unit.GetComponent<Transform>().position.x, unit.GetComponent<Transform>().position.y + 1.5f, 0);

                            //컴포넌트 설정
                            unit.transform.GetComponent<Unit>().canvas = GameObject.Find("UI(Overlay)");
                            unit.transform.GetComponent<Unit>().newChar = true;
                            unit.transform.GetComponent<Tank_UnitMovement>().Des = GameObject.Find("Destination");
                            unit.transform.GetComponent<Tank_PlayerTarget>().Des = GameObject.Find("Destination");
                            unit.transform.GetComponent<Unit>().evolved = true;
                        }
                        else if (evolveTarget.CompareTag("dps") && evolveTarget.GetComponent<Unit>().evolved == false)
                        {
                            evolvePos = evolveTarget.transform.position;
                            Destroy(evolveTarget.GetComponent<Unit>().hpBar.gameObject);
                            Destroy(evolveTarget.GetComponent<Unit>().mpBar.gameObject);
                            if (evolveTarget.GetComponent<Unit>().shieldBar != null)
                            {
                                Destroy(evolveTarget.GetComponent<Unit>().shieldBar.gameObject);
                            }
                            Destroy(evolveTarget.GetComponent<Unit>().upgradeBar.gameObject);
                            object_control.GetComponent<Control>().playerlist.Remove(evolveTarget.gameObject);
                            object_control.GetComponent<Control>().attacking.Remove(evolveTarget.gameObject);
                            targetList.GetComponent<TargetList>().targetIdle.Remove(evolveTarget.gameObject);
                            targetList.GetComponent<TargetList>().targetAttack.Remove(evolveTarget.gameObject);
                            Destroy(evolveTarget);
                            UnitSelections.Instance.unitList.Remove(evolveTarget);
                            UnitSelections.Instance.unitsSelected.Remove(evolveTarget);
                            GameObject unit = (GameObject)Instantiate(evolutionDps);
                            unit.transform.position = evolvePos;
                            unit.GetComponent<Unit>().SetUnitStatus(50, 10, 1.1f, 38.7f, 5.3f, 0, 70);
                            UnitSelections.Instance.unitList.Add(unit.gameObject);
                            //object_control.GetComponent<Control>().playerlist.Add(unit.gameObject);

                            //컴포넌트 설정
                            unit.transform.GetComponent<UnitMovement>().Des = GameObject.Find("Destination");
                            unit.transform.GetComponent<Unit>().canvas = GameObject.Find("UI(Overlay)");
                            unit.transform.GetComponent<Unit>().newChar = true;
                            unit.transform.GetComponent<PlayerTarget>().Des = GameObject.Find("Destination");
                            unit.transform.GetComponent<Unit>().evolved = true;
                            count++;
                        }
                        else if (evolveTarget.CompareTag("heal") && evolveTarget.GetComponent<Unit>().evolved == false) //이클립스
                        {
                            evolvePos = evolveTarget.transform.position;
                            Destroy(evolveTarget.GetComponent<Unit>().hpBar.gameObject);
                            Destroy(evolveTarget.GetComponent<Unit>().mpBar.gameObject);
                            if (evolveTarget.GetComponent<Unit>().shieldBar != null)
                            {
                                Destroy(evolveTarget.GetComponent<Unit>().shieldBar.gameObject);
                            }
                            Destroy(evolveTarget.GetComponent<Unit>().upgradeBar.gameObject);
                            object_control.GetComponent<Control>().playerlist.Remove(evolveTarget.gameObject);
                            object_control.GetComponent<Control>().attacking.Remove(evolveTarget.gameObject);
                            targetList.GetComponent<TargetList>().targetIdle.Remove(evolveTarget.gameObject);
                            targetList.GetComponent<TargetList>().targetAttack.Remove(evolveTarget.gameObject);
                            Destroy(evolveTarget);
                            UnitSelections.Instance.unitList.Remove(evolveTarget);
                            UnitSelections.Instance.unitsSelected.Remove(evolveTarget);
                            GameObject unit = (GameObject)Instantiate(evolutionHeal);
                            unit.transform.position = evolvePos;
                            unit.GetComponent<Unit>().SetUnitStatus(60, 7, 2, 40, 4.3f, 0, 40);
                            UnitSelections.Instance.unitList.Add(unit.gameObject);

                            //컴포넌트 설정
                            unit.transform.GetComponent<Heal_Unitmovement>().Des = GameObject.Find("Destination");
                            unit.transform.GetComponent<Unit>().newChar = true;
                            unit.transform.GetComponent<Unit>().canvas = GameObject.Find("UI(Overlay)");
                            unit.transform.GetComponent<Heal_PlayerTarget>().Des = GameObject.Find("Destination");
                            unit.transform.GetComponent<Unit>().evolved = true;
                        }
                    }
                    else if (slot.GetComponent<JewerlySlot>().canSummon == true && unitSelections.GetComponent<UnitSelections>().unitOver == true && canMove == true && reinforce == false)
                    {
                        count++;
                        limit.GetComponent<FadeInOut>().resetAnim();
                    }
                }
                else
                {
                    count++;
                    lack.GetComponent<FadeInOut>().resetAnim();
                }
            }
            else
            {
                count++;
            }
        }
        else if (gameManager.GetComponent<GameManager>().isBattle == true)
        {
            count++;
        }
        //초기 세팅으로 초기화
        rectTransform.position = startPos;
        box.SetActive(true);
        drag.SetActive(true);

        //if (count <= 0)
        //{
        //    this.gameObject.GetComponent<DpsDragDrop>().enabled = false;
        //}
    }
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Clickable") && col.gameObject.CompareTag("dps") && col.gameObject.GetComponent<Unit>().canEvolution == false)   //트리거 감지된 유닛이 진화 불가능한 탱커일 경우 강화
        {
            reinforce = true;
            reinforceTarget = col.gameObject;
            reinforceTarget.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Clickable") && col.gameObject.GetComponent<Unit>().canEvolution == true)
        {
            evolve = true;
            evolveTarget = col.gameObject;
            evolveTarget.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        reinforce = false;
        if (reinforceTarget != null)
        {
            reinforceTarget.transform.GetChild(1).gameObject.SetActive(false);
            reinforceTarget = null;
        }
        else if (evolveTarget != null)
        {
            evolveTarget.transform.GetChild(1).gameObject.SetActive(false);
            evolveTarget = null;
        }
        evolve = false;
    }
}
