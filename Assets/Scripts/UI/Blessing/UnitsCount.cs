using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitsCount : MonoBehaviour
{
    Control control;
    public string tagdps = "dps";
    public string tagheal = "heal";
    public string tagtank = "tank";
    public int dpsCount = 0;
    public int healCount = 0;
    public int tankCount = 0;
    public Text dpstext;
    public Text healtext;
    public Text tanktext;

    void Start()
    {
        control = GameObject.Find("Object_control").GetComponent<Control>();
        unitCount();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void unitCount()
    {
        foreach (GameObject unit in control.playerlist)
        {
            if (unit.tag == "dps")
            {
                dpsCount++;
            }
            else if (unit.tag == "heal")
            {
                healCount++;
            }
            else
            {
                tankCount++;
            }
        }
        dpstext.text = dpsCount.ToString();
        healtext.text = healCount.ToString();
        tanktext.text = tankCount.ToString();
    }
}
