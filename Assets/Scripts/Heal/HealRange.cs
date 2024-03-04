using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealRange : MonoBehaviour
{
    Heal_fsm heal_fsm;
    void Start()
    {
        heal_fsm = gameObject.transform.parent.gameObject.GetComponent<Heal_fsm>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        string tag = col.tag;
        if (tag.StartsWith("dps") || tag.StartsWith("heal") || tag.StartsWith("tank"))
        {
            if (!heal_fsm.crusaderHeal.Contains(col.gameObject))
            {
                heal_fsm.crusaderHeal.Add(col.gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if (tag.StartsWith("dps") || tag.StartsWith("heal") || tag.StartsWith("tank"))
        {
            if (heal_fsm.crusaderHeal.Contains(col.gameObject))
            {
                heal_fsm.crusaderHeal.Remove(col.gameObject);
            }
        }
    }
}
