using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    public GameObject target;

    public float x = 0;
    public float y = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            Follow(target);
        }
    }

    public void Follow(GameObject unit)
    {
        GetComponent<Transform>().position = new Vector3(unit.transform.position.x + x, unit.transform.position.y + y, 0);
        GetComponent<SpriteRenderer>().sortingOrder = unit.GetComponent<MeshRenderer>().sortingOrder + 1;
    }
}
