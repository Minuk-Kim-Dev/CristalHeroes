using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    GameObject parent;

    private void Start()
    {
        parent = transform.parent.gameObject;
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        string tag = parent.tag;
        if (tag.StartsWith("dps"))
        {
            DPS_fsm fsm = parent.GetComponent<DPS_fsm>();
            if (col.gameObject.CompareTag("enemy"))
            {
                if (!fsm.targetList.Contains(col.gameObject))
                    fsm.targetList.Add(col.gameObject);
                if (fsm.target == col.gameObject)
                {
                    fsm.fight = true;
                }
                // aclickÀÌ¸é ½Î¿ò
                //if (parent.GetComponent<UnitMovement>().only_move == false)
                //{
                //    fsm.fight = true;
                //    fsm.Idle();
                //}
            }
        }
        else if (tag.StartsWith("heal"))
        {
            Heal_fsm fsm = parent.GetComponent<Heal_fsm>();
            if (col.gameObject.CompareTag("enemy"))
            {
                if (!fsm.targetList.Contains(col.gameObject))
                    fsm.targetList.Add(col.gameObject);
                if (fsm.target == col.gameObject)
                {
                    fsm.fight = true;
                }
                // aclickÀÌ¸é ½Î¿ò
                //if (parent.GetComponent<Heal_Unitmovement>().only_move == false)
                //{
                //    fsm.fight = true;
                //    fsm.Idle();
                //}
            }
        }
        else if (tag.StartsWith("tank"))
        {
            Tank_fsm fsm = parent.GetComponent<Tank_fsm>();
            if (col.gameObject.CompareTag("enemy"))
            {
                if (!fsm.targetList.Contains(col.gameObject))
                    fsm.targetList.Add(col.gameObject);
                if (fsm.target == col.gameObject)
                {
                    fsm.fight = true;
                }
                // aclickÀÌ¸é ½Î¿ò
                //if (parent.GetComponent<Tank_UnitMovement>().only_move == false)
                //{
                //    fsm.fight = true;
                //    fsm.Idle();
                //}
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        string tag = parent.tag;
        if (tag.StartsWith("dps"))
        {
            DPS_fsm fsm = parent.GetComponent<DPS_fsm>();
            fsm.targetList.Remove(col.gameObject);
            if (fsm.targetList.Count == 0)
            {
                fsm.fight = false;
            }
        }
        else if (tag.StartsWith("heal"))
        {
            Heal_fsm fsm = parent.GetComponent<Heal_fsm>();
            fsm.targetList.Remove(col.gameObject);
            if (fsm.targetList.Count == 0)
            {
                fsm.fight = false;
            }
        }
        else if (tag.StartsWith("tank"))
        {
            Tank_fsm fsm = parent.GetComponent<Tank_fsm>();
            fsm.targetList.Remove(col.gameObject);
            if (fsm.targetList.Count == 0)
            {
                fsm.fight = false;
            }
        }
    }

}