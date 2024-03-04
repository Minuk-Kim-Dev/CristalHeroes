using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRange : MonoBehaviour
{
    public Enemy_FSM enemy_fsm;
    Enemy enemy;
    TargetList targetlist;

    private void Start()
    {
        enemy = this.transform.parent.GetComponent<Enemy>();
        enemy_fsm = this.transform.parent.GetComponent<Enemy_FSM>();
        targetlist = GameObject.Find("TargetList").GetComponent<TargetList>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if (tag.StartsWith("dps") || tag.StartsWith("tank") || tag.StartsWith("heal"))
        {
            if (enemy_fsm.target == null)  //target이 없으면 
            {
                enemy_fsm.target = col.gameObject;
            }
            enemy_fsm.FSM_Move();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
    }
}

