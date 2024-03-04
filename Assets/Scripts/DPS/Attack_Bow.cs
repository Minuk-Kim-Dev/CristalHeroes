using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Bow : MonoBehaviour
{
    public GameObject arrow;
    public GameObject penetrateArrow;
    DPS_fsm dps_fsm;
    Vector2 bow_vector;
    public GameObject target;
    Unit unit;
    ArcherManager archerManager;

    void Start()
    {
        archerManager = GameObject.Find("ArcherManager").GetComponent<ArcherManager>();
        dps_fsm = gameObject.transform.parent.GetComponent<DPS_fsm>();
        unit = gameObject.transform.parent.GetComponent<Unit>();
    }
    private void Update()
    {
        target = dps_fsm.target;
        bow_vector.x = gameObject.transform.position.x;
        bow_vector.y = gameObject.transform.position.y;
    }
    public void Shoot(int i)
    {
        if (i == 1)
        {
            GameObject copyArrow = Instantiate(arrow, new Vector2(bow_vector.x, bow_vector.y + 1.0f), Quaternion.identity); //obj.transform.rotation - 회전값
            if (unit.attackCnt >= 2)
            {
                copyArrow.GetComponent<Arrow>().Target_dmg(target, unit.dmg + archerManager.archerBonus);
                unit.attackCnt = 0;
            }
            else
            {
                copyArrow.GetComponent<Arrow>().Target_dmg(target, unit.dmg);
                unit.attackCnt++;
            }
            unit.nowMp += 10;
        }
        else
        {
            GameObject copyArrow = Instantiate(arrow, new Vector2(bow_vector.x, bow_vector.y + 1.0f), Quaternion.identity); //obj.transform.rotation - 회전값
            if (unit.attackCnt >= 2)
            {
                copyArrow.GetComponent<Arrow>().Target_dmg(target, 8 + archerManager.archerBonus);
                unit.attackCnt = 0;
            }
            else
            {
                copyArrow.GetComponent<Arrow>().Target_dmg(target, 8);
                unit.attackCnt++;
            }
        }
    }

    public void Penetrate_Shoot()
    {
        GameObject copyArrow = Instantiate(penetrateArrow, new Vector2(bow_vector.x, bow_vector.y + 1.0f), Quaternion.identity); //obj.transform.rotation - 회전값
        if (unit.attackCnt >= 2)
        {
            copyArrow.GetComponent<PiercingArrow>().Target_dmg(target, unit.dmg + archerManager.archerBonus);
            unit.attackCnt = 0;
        }
        else
        {
            copyArrow.GetComponent<PiercingArrow>().Target_dmg(target, unit.dmg);
            unit.attackCnt++;
        }
    }
}
