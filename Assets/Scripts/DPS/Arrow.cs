using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject target;
    bool shoot = false;
    public float dmg;
    float speed = 20f;
    float angle;

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
                    angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
                    this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                }
            }
        }
    }

    public void Target_dmg(GameObject dps_target, float dps_dmg)
    {
        target = dps_target;
        dmg = dps_dmg;
        shoot = true;
        target.GetComponent<Enemy>().projectile.Add(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            if (target == col.gameObject)
            {
                target.GetComponent<Enemy>().TakeDamage(dmg);
                if (target.GetComponent<Enemy>().projectile.Contains(this.gameObject))
                {
                    target.GetComponent<Enemy>().projectile.Remove(this.gameObject);
                }
                Destroy(gameObject);
            }
        }
    }
}
