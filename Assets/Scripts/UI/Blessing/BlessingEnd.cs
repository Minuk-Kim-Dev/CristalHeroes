using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessingEnd : MonoBehaviour
{
    Control control;
    public Blessing dpsblessing;
    public Blessing healblessing;
    public Blessing tankblessing;

    public GameObject lack;
    public GameObject blessingUI;
    public JewerlyCount jc;

    public GameManager Gm;

    public DpsDragDrop ddd;
    public HealDragDrop hdd;
    public TankDragDrop tdd;
    public bool confirmed = false;
    void Start()
    {
        control = GameObject.Find("Object_control").GetComponent<Control>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jc.djCount < dpsblessing.blessingJewerly || jc.hjCount < healblessing.blessingJewerly || jc.tjCount < tankblessing.blessingJewerly)
        {
            confirmed = false;
        }
        else
        {
            confirmed = true;
        }
    }

    public void Blessing()
    {
        if (confirmed == true)
        {
            Gm.EndBlessing();
        }
        else
        {
            lack.SetActive(true);
        }
    }

    public void dps()
    {
        if (dpsblessing.select)
        {
            for (int i = control.debuff.Count - 1; i >= 0; i--)
            {
                string tag = control.debuff[i].tag;
                if (tag.StartsWith("dps"))
                {
                    control.debuff[i].GetComponent<Unit>().DurabilityReset();
                }
                control.debuff.Remove(control.debuff[i]);
            }
            ddd.count -= dpsblessing.blessingJewerly;
            dpsblessing.select = false;
        }
    }

    public void heal()
    {
        if (healblessing.select)
        {
            for (int i = control.debuff.Count - 1; i >= 0; i--)
            {
                string tag = control.debuff[i].tag;
                if (tag.StartsWith("heal"))
                {
                    control.debuff[i].GetComponent<Unit>().DurabilityReset();
                    control.debuff.Remove(control.debuff[i]);
                }
            }
            hdd.count -= healblessing.blessingJewerly;
            healblessing.select = false;
        }
    }

    public void tank()
    {
        if (tankblessing.select)
        {
            for (int i = control.debuff.Count - 1; i >= 0; i--)
            {
                string tag = control.debuff[i].tag;
                if (tag.StartsWith("tank"))
                {
                    control.debuff[i].GetComponent<Unit>().DurabilityReset();
                }
                control.debuff.Remove(control.debuff[i]);
            }
            tdd.count -= tankblessing.blessingJewerly;
            tankblessing.select = false;
        }
    }

}
