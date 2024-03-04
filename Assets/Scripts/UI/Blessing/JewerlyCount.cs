using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JewerlyCount : MonoBehaviour
{
    public DpsDragDrop ddd;
    public HealDragDrop hdd;
    public TankDragDrop tdd;

    public int djCount = 0;
    public int hjCount = 0;
    public int tjCount = 0;

    public Text dj;
    public Text hj;
    public Text tj;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        djCount = ddd.count;
        hjCount = hdd.count;
        tjCount = tdd.count;

        dj.text = djCount.ToString();
        hj.text = hjCount.ToString();
        tj.text = tjCount.ToString();
    }
}
