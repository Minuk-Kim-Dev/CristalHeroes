using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkRange : MonoBehaviour
{
    Enemy_FSM enemy_fsm;
    Enemy enemy;

    private void Start()
    {
        enemy = this.transform.parent.GetComponent<Enemy>();
        enemy_fsm = this.transform.parent.GetComponent<Enemy_FSM>();
    }
    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    string tag = col.gameObject.tag;
    //    if (tag.StartsWith("dps") || tag.StartsWith("tank") || tag.StartsWith("heal"))
    //    {
    //        if(enemy_fsm.target == col.gameObject)
    //        {
    //            this.transform.parent.GetComponent<Enemy_FSM>().fight = true;
    //            this.transform.parent.GetComponent<Enemy_FSM>().FSM_Idle();
    //        }
    //    }
    //}

    private void OnTriggerStay2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if (tag.StartsWith("dps") || tag.StartsWith("tank") || tag.StartsWith("heal"))
        {
            if (enemy_fsm.target == col.gameObject)
            {
                this.transform.parent.GetComponent<Enemy_FSM>().fight = true;
                this.transform.parent.GetComponent<Enemy_FSM>().FSM_Idle();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if (tag.StartsWith("dps") || tag.StartsWith("tank") || tag.StartsWith("heal"))
              this.transform.parent.GetComponent<Enemy_FSM>().fight = false;
           
    }
}
