using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
    private Camera myCam;
    float MaxDistance = 15f;
    Vector3 MousePosition;
    private MeshRenderer rend;
    void Start()
    {
        myCam = Camera.main;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePosition = Input.mousePosition;
            MousePosition = myCam.ScreenToWorldPoint(MousePosition);

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, Vector2.up, MaxDistance);
            Physics2D.queriesHitTriggers = false;

            if (hit)
            {
                int clickableLayerIndex = LayerMask.NameToLayer("Clickable");
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Clickable"))
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        UnitSelections.Instance.ShiftClickSelect(hit.collider.gameObject);
                    }
                    else
                    {
                        UnitSelections.Instance.ClickSelect(hit.collider.gameObject);
                    }

                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        UnitSelections.Instance.ControlClickSelect(hit.collider.gameObject);
                    }
                }
                //Clickable이 아닌 적들을 선택했을 경우
                else
                {
                    UnitSelections.Instance.DeselectAll();
                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelections.Instance.DeselectAll();
                }
            }
        }
    }
}
