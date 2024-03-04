using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D cursorNormal;    //평상시
    [SerializeField] Texture2D clickA;  //A눌렀을때
    [SerializeField] Texture2D onEnemy;    //적 유닛에 마우스를 올렸을 때

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorNormal, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseEnter(GameObject other)
    {
        if(other.tag == "enemy")
        {
            Cursor.SetCursor(onEnemy, Vector2.zero, CursorMode.Auto);
        }
    }
    void AKeyDown()
    {
        Cursor.SetCursor(clickA, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(cursorNormal, Vector2.zero, CursorMode.Auto);
    }
}
