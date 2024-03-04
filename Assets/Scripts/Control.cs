using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public List<GameObject> playerlist = new List<GameObject>();
    public List<GameObject> enemylist = new List<GameObject>();
    public List<GameObject> attacking = new List<GameObject>();

    public List<GameObject> debuff = new List<GameObject>();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject unit in playerlist)
        {
            if (unit.GetComponent<Unit>().durability == 15)
            {
                if (!debuff.Contains(unit))
                    debuff.Add(unit);
            }
        }
    }
}
