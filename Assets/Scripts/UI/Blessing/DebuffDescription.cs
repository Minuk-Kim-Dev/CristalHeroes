using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DebuffDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject descriptionUI; // 설명창 UI

    void Start()
    {
        descriptionUI.SetActive(false); // 설명창 UI를 비활성화
    }

    // 마우스를 올렸을 때 호출되는 함수
    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionUI.SetActive(true); // 설명창 UI를 활성화
    }

    // 마우스를 내렸을 때 호출되는 함수
    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionUI.SetActive(false); // 설명창 UI를 비활성화
    }
}
