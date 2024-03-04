using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCount : MonoBehaviour
{
    GameObject us;
    UnitSelections unitSelections;
    List<GameObject> unitList;
    GameObject unitObject;
    Text text;       //유닛 수 텍스트
    public int unitCount;
    public string tagName;
    public int listSize;

    private void Start()
    {
        us = GameObject.Find("UnitSelections");
        unitList = new List<GameObject>();
        unitSelections = us.GetComponent<UnitSelections>();
        text = GetComponentInChildren<Text>();
        unitCount = 0;
        listSize = 0;
    }

    private void Update()
    {
        unitList = unitSelections.unitList;
        if(listSize != unitList.Count)
        {
            unitCount = 0;
            listSize = unitList.Count;
            for (int i = 0; i < listSize; i++)
            {
                if (unitList[i].CompareTag(tagName))
                {
                    unitCount++;
                }
            }
        }
        text.text = unitCount.ToString();
    }
}
