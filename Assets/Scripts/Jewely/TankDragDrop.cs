using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Spine.Unity;

public class TankDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject effect;
    GameObject drag;
    GameObject box;
    public Image limit;
    public Image lack;

    float moveRate;
    public bool Dragging;
    public GameObject prfUnit;      //��ȯ�� ���� ������Ʈ
    public GameObject evolutionTank;    //��ȭ�� ��Ŀ��Ŀ ������
    public GameObject evolutionDps;     //��ȭ�� ��Ŀ���� ������
    public GameObject evolutionHeal;    //��ȭ�� ��Ŀ���� ������
    public Vector3 startPos;    //������ ���� ��ġ
    public Vector3 endPos;      //������ ��ȯ�� ��ġ 
    public GameObject slot;
    public bool canMove;    //������ �巡�װ� ������ ��������
    public int count;       //���� ����
    public bool reinforce;    //true�϶� ���� ��ȭ
    public bool evolve;     //true�϶� ���� ��ȭ
    public GameObject evolveTarget;
    public Vector3 evolvePos;       //��ȭ���� ��ġ ������ ����
    public GameObject reinforceTarget;
    public Text jewelyCount;    //���� ���� ����� UI -  �������� ����
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
        //���� ���� ī��Ʈ
        jewelyCount.text = count.ToString();

        //���� �巡�� �� ���콺 ��Ŭ���ϸ� ���
        if (Dragging == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                rectTransform.position = startPos;
                canMove = false;

                //���� �ʱ� �������� �ʱ�ȭ
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
                rectTransform.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
                count++;
            }
        }

        if (count > 0)
        {
            this.gameObject.GetComponent<TankDragDrop>().enabled = true;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canMove = true;
        rectTransform.anchoredPosition = rectTransform.anchoredPosition + eventData.delta * moveRate;

        //�巡�� �ڽ� ��Ȱ��ȭ
        drag.SetActive(false);
        box.SetActive(false);

        Dragging = true;

        //���� �巡�� �� �� �������� �ϱ�
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

        //���� �巡�� �� ũ�� �۾�����
        rectTransform.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 1);

        //���� ���� ����
        count--;

        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canMove)
        {
            Debug.Log("OnDrag");

            //���� ��ġ �巡�� �̵�
            rectTransform.anchoredPosition = rectTransform.anchoredPosition + eventData.delta * moveRate;

            //endPos == ������ ��ȯ�� ��ġ
            endPos = transform.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Dragging = false;
        Debug.Log("OnEndDrag");

        //���� �ʱ� �������� �ʱ�ȭ
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
                if(count >= 0)
                {
                    //���� ����
                    if (slot.GetComponent<JewerlySlot>().canSummon == true && unitSelections.GetComponent<UnitSelections>().unitOver == false && canMove == true && reinforce == false && evolve == false)
                    {
                        GameObject unit = (GameObject)Instantiate(prfUnit);
                        unit.transform.position = endPos;
                        UnitSelections.Instance.unitList.Add(unit.gameObject);

                        //������Ʈ ����
                        unit.transform.GetComponent<Unit>().canvas = GameObject.Find("UI(Overlay)");
                        unit.transform.GetComponent<Unit>().newChar = true;
                        unit.transform.GetComponent<Tank_UnitMovement>().Des = GameObject.Find("Destination");
                        unit.transform.GetComponent<Tank_PlayerTarget>().Des = GameObject.Find("Destination");
                    }
                    else if (slot.GetComponent<JewerlySlot>().canSummon == false && canMove == true)    //���� UI������ �巡���� ��� �巡�� ���
                    {
                        count++;
                    }
                    else if (canMove == true && reinforce == true)   //���� ��ȭ
                    {
                        if (reinforceTarget.GetComponent<Unit>().upgradeCnt == 0)   //���� ��ȭ�� 1�ܰ��� ���
                        {
                            reinforceTarget.GetComponent<Unit>().maxHp += 20;     //�ִ�ü�� 50 ����
                            reinforceTarget.GetComponent<Unit>().nowHp += 20;     //�ִ�ü�� 50 ����
                            reinforceTarget.GetComponent<Unit>().upgradeCnt++;
                            GameObject upgrade = Instantiate(effect);
                            upgrade.GetComponent<Transform>().position = new Vector3(reinforceTarget.GetComponent<Transform>().position.x, reinforceTarget.GetComponent<Transform>().position.y + 1.5f, 0);
                            Destroy(reinforceTarget.GetComponent<Unit>().upgradeBar.gameObject);
                            reinforceTarget.GetComponent<Unit>().upgradeBar = Instantiate(reinforceTarget.GetComponent<Unit>().prfUpgradeLevel[reinforceTarget.GetComponent<Unit>().upgradeCnt], reinforceTarget.GetComponent<Unit>().canvas.transform).GetComponent<RectTransform>();
                        }
                        else if (reinforceTarget.GetComponent<Unit>().upgradeCnt == 1)   //���� ��ȭ�� 2�ܰ��� ���
                        {
                            reinforceTarget.GetComponent<Unit>().dmg += 4;     //������ ����
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
                    else if (evolve == true)        //���� ��ȭ
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
                            unit.GetComponent<Unit>().SetUnitStatus(200, 8.5f, 2.4f, 1.5f, 3.8f, 0, 0);
                            UnitSelections.Instance.unitList.Add(unit.gameObject);

                            GameObject upgrade = Instantiate(effect);
                            upgrade.GetComponent<Transform>().position = new Vector3(unit.GetComponent<Transform>().position.x, unit.GetComponent<Transform>().position.y + 1.5f, 0);
                            //object_control.GetComponent<Control>().playerlist.Add(unit.gameObject);

                            //������Ʈ ����
                            unit.transform.GetComponent<Unit>().canvas = GameObject.Find("UI(Overlay)");
                            unit.transform.GetComponent<Unit>().newChar = true;
                            unit.transform.GetComponent<Tank_UnitMovement>().Des = GameObject.Find("Destination");
                            unit.transform.GetComponent<Tank_PlayerTarget>().Des = GameObject.Find("Destination");
                            unit.transform.GetComponent<Unit>().evolved = true;
                        }
                        else if (evolveTarget.CompareTag("dps") && evolveTarget.GetComponent<Unit>().evolved == false) //��������
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
                            unit.GetComponent<Unit>().SetUnitStatus(85, 25, 2.2f, 40f, 4.5f, 0, 0);
                            UnitSelections.Instance.unitList.Add(unit.gameObject);
                            //object_control.GetComponent<Control>().playerlist.Add(unit.gameObject);

                            //������Ʈ ����
                            unit.transform.GetComponent<UnitMovement>().Des = GameObject.Find("Destination");
                            unit.transform.GetComponent<Unit>().canvas = GameObject.Find("UI(Overlay)");
                            unit.transform.GetComponent<Unit>().newChar = true;
                            unit.transform.GetComponent<PlayerTarget>().Des = GameObject.Find("Destination");
                            unit.transform.GetComponent<Unit>().evolved = true;
                        }
                        else if (evolveTarget.CompareTag("heal") && evolveTarget.GetComponent<Unit>().evolved == false) //ũ�缼�̴�
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
                            unit.GetComponent<Unit>().SetUnitStatus(200, 7, 2.2f, 1.75f, 4.7f, 0, 60);
                            UnitSelections.Instance.unitList.Add(unit.gameObject);
                            //object_control.GetComponent<Control>().playerlist.Add(unit.gameObject);

                            //������Ʈ ����
                            unit.transform.GetComponent<Heal_Unitmovement>().Des = GameObject.Find("Destination");
                            unit.transform.GetComponent<Unit>().newChar = true;
                            unit.transform.GetComponent<Unit>().canvas = GameObject.Find("UI(Overlay)");
                            unit.transform.GetComponent<Heal_PlayerTarget>().Des = GameObject.Find("Destination");
                            unit.transform.GetComponent<Unit>().evolved = true;
                        }
                    }
                    else if (slot.GetComponent<JewerlySlot>().canSummon == true && unitSelections.GetComponent<UnitSelections>().unitOver == true && canMove == true && reinforce == false)    //���� �� �ʰ��� ������ �Ұ��� �� ���
                    {
                        count++;        //���� ��ȯ
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
        //�ʱ� �������� �ʱ�ȭ
        rectTransform.position = startPos;
        box.SetActive(true);
        drag.SetActive(true);

        //if (count <= 0)
        //{
        //    this.gameObject.GetComponent<TankDragDrop>().enabled = false;
        //}
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        rectTransform.anchoredPosition += eventData.delta;
    }


    //���� ��ȭ ����  
    void OnTriggerEnter2D(Collider2D col)
    {
        //Ʈ���� ������ ������ ��ȭ �Ұ����� ��Ŀ�� ��� ��ȭ
        if (col.gameObject.layer == LayerMask.NameToLayer("Clickable") && col.gameObject.CompareTag("tank") && col.gameObject.GetComponent<Unit>().canEvolution == false)   
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