using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public GameObject target;
    public bool shoot = false;
    public float dmg;
    float speed = 15f;
    void Update()
    {
        if (target != null)
        {
            if (!target.GetComponent<Enemy>().projectile.Contains(this.gameObject))
            {
                Destroy(gameObject);
            }
            else
            {
                if (shoot)
                {
                    gameObject.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Target_dmg(GameObject heal_target, float heal_dmg)
    {
        target = heal_target;
        dmg = heal_dmg;
        shoot = true;
        target.GetComponent<Enemy>().projectile.Add(gameObject);
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            if (target == col.gameObject)
            {
                if (target.GetComponent<Enemy>().projectile.Contains(this.gameObject))
                {
                    target.GetComponent<Enemy>().projectile.Remove(this.gameObject);
                }
                target.GetComponent<Enemy>().TakeDamage(dmg);
                Destroy(gameObject);
            }
        }
    }
}
