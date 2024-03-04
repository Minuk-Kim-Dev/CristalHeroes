using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    DPS_fsm dps_fsm;
    private void Start()
    {
        dps_fsm = gameObject.transform.parent.gameObject.GetComponent<DPS_fsm>();
    }
    void OnTriggerStay2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if (tag.StartsWith("dps") || tag.StartsWith("heal") || tag.StartsWith("tank"))
        {
            if (!dps_fsm.sacredBuff.Contains(col.gameObject))
            {
                dps_fsm.sacredBuff.Add(col.gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if (tag.StartsWith("dps") || tag.StartsWith("heal") || tag.StartsWith("tank"))
        {
            if (dps_fsm.sacredBuff.Contains(col.gameObject))
            {
                dps_fsm.sacredBuff.Remove(col.gameObject);
            }
        }
    }
}
