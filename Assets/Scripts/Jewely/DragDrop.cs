using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    GameObject drag;
    GameObject box;

    public bool Dragging;
    public GameObject prfUnit;      //소환될 유닛 오브젝트
    public Vector3 startPos;    //보석의 기존 위치
    public Vector3 endPos;      //유닛이 소환될 위치 
    public GameObject slot;
    public bool canMove;    //보석이 드래그가 가능한 상태인지
    public int count;       //보석 개수
    public Text jewelyCount;    //보석 개수 출력할 UI -  수동으로 설정

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    void Start()
    {
        Dragging = false;
        count = 3;
        canMove = false;

        drag = GameObject.Find("UnitSelectionSystem");
        box = GameObject.Find("BoxSelectCanvas");

        startPos = transform.position;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
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
                rectTransform.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                count++;
            }
        }

        if(count > 0)
        {
            this.gameObject.GetComponent<DragDrop>().enabled = true;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canMove = true;

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

        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canMove)
        {
            Debug.Log("OnDrag");

            //보석 위치 드래그 이동
            rectTransform.anchoredPosition += eventData.delta;

            //endPos == 유닛이 소환될 위치
            endPos = transform.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Dragging = false;
        Debug.Log("OnEndDrag");

        //보석 초기 세팅으로 초기화
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        rectTransform.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        //유닛 생성
        if (slot.GetComponent<JewerlySlot>().canSummon == true && canMove == true)
        {
            GameObject unit = (GameObject)Instantiate(prfUnit);
            unit.transform.position = endPos;
            UnitSelections.Instance.unitList.Add(unit.gameObject);

            //컴포넌트 설정
            unit.transform.GetComponent<UnitMovement>().Des = GameObject.Find("Destination");
            unit.transform.GetComponent<Unit>().canvas = GameObject.Find("Canvas");
            unit.transform.GetComponent<PlayerTarget>().Des = GameObject.Find("Destination");
        }
        else if(slot.GetComponent<JewerlySlot>().canSummon == false && canMove == true)
        {
            count++;
        }

        //초기 세팅으로 초기화
        rectTransform.position = startPos;
        box.SetActive(true);
        drag.SetActive(true);

        if (count <= 0)
        {
            this.gameObject.GetComponent<DragDrop>().enabled = false;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
}
