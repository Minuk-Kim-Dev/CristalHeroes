using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BankJewelMove : MonoBehaviour
{
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.GetComponent<RectTransform>().CompareTag("knightJewel"))
            text = GameObject.Find("BankedKnightJewel");
        else if(transform.GetComponent<RectTransform>().CompareTag("archerJewel"))
            text = GameObject.Find("BankedArcherJewel");
        else if (transform.GetComponent<RectTransform>().CompareTag("priestJewel"))
            text = GameObject.Find("BankedPriestJewel");
        Invoke("OnDestroy", 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<RectTransform>().position = Vector2.MoveTowards(transform.GetComponent<RectTransform>().position, text.GetComponent<RectTransform>().position, 20.0f * Time.deltaTime);
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
