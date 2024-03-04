using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blessing : MonoBehaviour
{
    public DebuffButton debuffB1;
    public DebuffButton debuffB2;
    public DebuffButton debuffB3;
    public DebuffButton debuffB4;
    public Sprite selected;
    public Sprite unSelected;
    Image image;

    Button bless;
    public bool select;

    public Text cost;
    public int blessingJewerly = 0;
    public Blessing[] other;

    public bool[] blessingcheck = new bool[4];
    void Start()
    {
        image = GetComponent<Image>();
        select = false;
        bless = GetComponent<Button>();
        bless.onClick.AddListener(ReleaseDebuff);
    }

    // Update is called once per frame
    void Update()
    {
        cost.text = blessingJewerly.ToString();
        if (select)
        {
            if (debuffB1.select == true)
            {
                blessingcheck[0] = true;
            }
            if (debuffB2.select == true)
            {
                blessingcheck[1] = true;
            }
            if (debuffB3.select == true)
            {
                blessingcheck[2] = true;
            }
            if (debuffB4.select == true)
            {
                blessingcheck[3] = true;
            }
            image.sprite = selected;
        }
        else
        {
            blessingcheck[0] = false;
            blessingcheck[1] = false;
            blessingcheck[2] = false;
            blessingcheck[3] = false;
            image.sprite = unSelected;
        }
    }

    public void BlessingOn()
    {
        blessingJewerly += 1;
    }

    public void BlessingOff()
    {
        blessingJewerly = 0;
    }

    void ReleaseDebuff()
    {
        select = !select;
        if(select == true)
        {
            BlessingOn();
            for(int i = 0; i < 2; i++)
            {
                if(other[i].select == true)
                {
                    other[i].blessingJewerly += 1;
                }
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                if (other[i].select == true)
                {
                    if(blessingJewerly == 1)
                    {
                        if (other[i].blessingJewerly == 2)
                            other[i].blessingJewerly = 1;
                        else if (other[i].blessingJewerly == 3)
                            other[i].blessingJewerly = 2;
                    }
                    else if(blessingJewerly == 2)
                    {
                        if (other[i].blessingJewerly == 3)
                            other[i].blessingJewerly = 2;
                    }
                }
            }
            BlessingOff();
        }
    }
}
