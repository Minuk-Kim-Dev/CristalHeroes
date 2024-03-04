using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingArrow : MonoBehaviour
{
    public GameObject target;
    bool shoot = false;
    public float dmg;
    float speed = 10f;
    float extraDistance = 5f;
    Vector3 extraMove;
    Vector3 initialPosition;
    public bool collided = false; // 충돌 여부
    float angle;

    void Start()
    {
        initialPosition = transform.position;
    }
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
                if (shoot && !collided)
                {
                    // 타겟 방향으로 이동
                    gameObject.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
                    angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
                    this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                }
                else if (collided)
                {
                    // 추가 이동
                    Vector3 direction = target.transform.position - initialPosition;
                    extraMove = target.transform.position + direction.normalized * extraDistance;
                    gameObject.transform.position = Vector3.MoveTowards(transform.position, extraMove, Time.deltaTime * speed);
                }
            }
        }
        //if (shoot && !collided)
        //{
        //    // 타겟 방향으로 이동
        //    gameObject.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
        //    angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        //    this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        //}
        //else if (collided)
        //{
        //    // 추가 이동
        //    Vector3 direction = target.transform.position - initialPosition;
        //    extraMove = target.transform.position + direction.normalized * extraDistance;
        //    gameObject.transform.position = Vector3.MoveTowards(transform.position, extraMove, Time.deltaTime * speed);
        //}

        if (gameObject.transform.position == extraMove)
        {
            if (target.GetComponent<Enemy>().projectile.Contains(this.gameObject))
            {
                target.GetComponent<Enemy>().projectile.Remove(this.gameObject);
            }
            Destroy(this.gameObject);
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
        if (collided == false)
        {
            if (col.gameObject.tag == "enemy")
            {
                if (target == col.gameObject)
                {
                    target.GetComponent<Enemy>().TakeDamage(dmg);
                    collided = true;
                }
            }
        }
        else
        {
            if (col.gameObject.tag == "enemy")
                col.GetComponent<Enemy>().TakeDamage(dmg);
        }
    }
}


