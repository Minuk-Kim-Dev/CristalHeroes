using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DebuffButton : MonoBehaviour
{
    public bool select = false;
    Image buttonImage;
    public Blessing blessing;
    public Sprite redImage;
    public Sprite baseImage;

    Text childText;
    void Start()
    {
        buttonImage = GetComponent<Image>();
        childText = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (childText.text == "0" || childText.text == "")
        {
            childText.text = "";
            buttonImage.sprite = baseImage;
            select = false;
        }
        else
        {
            select = true;
            buttonImage.sprite = redImage;
        }
    }
}
