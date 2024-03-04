using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuffCount : MonoBehaviour
{
    Control control;
    public Text dd1;
    public Text dd2;
    public Text dd3;
    public Text dd4;
    public Text dh1;
    public Text dh2;
    public Text dh3;
    public Text dh4;
    public Text dt1;
    public Text dt2;
    public Text dt3;
    public Text dt4;
    public int[] debuff = new int[12];
    void Start()
    {
        control = GameObject.Find("Object_control").GetComponent<Control>();
        DebuffOn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DebuffOn()
    {
        for(int i = 0; i < debuff.Length; i++)
        {
            debuff[i] = 0;
        }
        foreach (GameObject unit in control.debuff)
        {
            string tag = unit.tag;
            if (tag.StartsWith("dps"))
            {
                if (unit.GetComponent<Unit>().random_debuff == 1)
                {
                    debuff[0]++;
                }
                else if (unit.GetComponent<Unit>().random_debuff == 2)
                {
                    debuff[1]++;
                }
                else if (unit.GetComponent<Unit>().random_debuff == 3)
                {
                    debuff[2]++;
                }
                else if (unit.GetComponent<Unit>().random_debuff == 4)
                {
                    debuff[3]++;
                }
            }
            else if (tag.StartsWith("heal"))
            {
                if (unit.GetComponent<Unit>().random_debuff == 1)
                {
                    debuff[4]++;
                }
                else if (unit.GetComponent<Unit>().random_debuff == 2)
                {
                    debuff[5]++;
                }
                else if (unit.GetComponent<Unit>().random_debuff == 3)
                {
                    debuff[6]++;
                }
                else if (unit.GetComponent<Unit>().random_debuff == 4)
                {
                    debuff[7]++;
                }
            }
            else if (tag.StartsWith("tank"))
            {
                if (unit.GetComponent<Unit>().random_debuff == 1)
                {
                    debuff[8]++;
                }
                else if (unit.GetComponent<Unit>().random_debuff == 2)
                {
                    debuff[9]++;
                }
                else if (unit.GetComponent<Unit>().random_debuff == 3)
                {
                    debuff[10]++;
                }
                else if (unit.GetComponent<Unit>().random_debuff == 4)
                {
                    debuff[11]++;
                }
            }
        }

        dd1.text = debuff[0].ToString();
        dd2.text = debuff[1].ToString();
        dd3.text = debuff[2].ToString();
        dd4.text = debuff[3].ToString();
        dh1.text = debuff[4].ToString();
        dh2.text = debuff[5].ToString();
        dh3.text = debuff[6].ToString();
        dh4.text = debuff[7].ToString();
        dt1.text = debuff[8].ToString();
        dt2.text = debuff[9].ToString();
        dt3.text = debuff[10].ToString();
        dt4.text = debuff[11].ToString();
    }

    private void OnEnable()
    {
        DebuffOn();
    }
}
