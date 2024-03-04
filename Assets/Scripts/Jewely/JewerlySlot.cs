using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JewerlySlot : MonoBehaviour
{
    public bool jewelryDrag;    //드래그중인 보석이 있는지?
    private Camera myCam;
    float MaxDistance = 15f;
    Vector3 MousePosition;
    public bool canSummon;
    

    GameObject drag;
    GameObject box;
    GameObject dragBox;
    public GameObject tJ;
    public GameObject dJ;
    public GameObject hJ;
    DragDrop dragDrop;

    private void Start()
    {
        canSummon = false;
        jewelryDrag = false;

        myCam = Camera.main;
        drag = GameObject.Find("UnitSelectionSystem");
        box = GameObject.Find("BoxSelectCanvas");
        dragBox = GameObject.Find("DragClick");
    }

    void Update()
    {
        jewelryDrag = tJ.GetComponent<TankDragDrop>().Dragging || dJ.GetComponent<DpsDragDrop>().Dragging || hJ.GetComponent<HealDragDrop>().Dragging;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            MousePosition = Input.mousePosition;
            MousePosition = myCam.ScreenToWorldPoint(MousePosition);

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance);

            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("Jewerly"))  //마우스가 보석 슬롯에 올려져 있을 경우
                {
                    canSummon = false;      //보석 소환 불가

                    if(dragBox.GetComponent<UnitDrag>().onDrag == false)    //유닛을 드래그하는 중이 아닐 경우
                    {
                        drag.SetActive(false);
                        box.SetActive(false);
                    }
                }
            }
        }
        else
        {
            canSummon=true;
            if (jewelryDrag == false) //보석을 드래그 중이 아니라면
            {
                drag.SetActive(true);
                box.SetActive(true);
            }
        }
    }
}
