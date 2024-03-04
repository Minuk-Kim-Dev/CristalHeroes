using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class Attack_Sword : MonoBehaviour
{
    public GameObject target;
    float x;
    Tank_fsm tank_fsm;
    Unit unit;

    void Start()
    {
        tank_fsm = gameObject.transform.parent.GetComponent<Tank_fsm>();
        unit = gameObject.transform.parent.GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        x = GetComponentInParent<Transform>().parent.transform.localScale.x;
        target = tank_fsm.target;
    }

    public void Swing()
    {
        StartCoroutine(Attack_Dmg());
    }

    IEnumerator Attack_Dmg()
    {
        yield return new WaitForSeconds(1.0f);
        if (target != null)
            target.GetComponent<Enemy>().TakeDamage(unit.dmg);
        StopCoroutine(Attack_Dmg());
    }
}
