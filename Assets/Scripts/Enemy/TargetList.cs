using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetList : MonoBehaviour
{

    public List<GameObject> targetAttack = new List<GameObject>();
    public List<GameObject> targetIdle = new List<GameObject>();

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (targetAttack.Contains(this.gameObject))
        {
            return;
        }
        if (targetIdle.Contains(this.gameObject))
        {
            return;
        }
    }
}
