using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class Skill : MonoBehaviour
{
    public GameObject sacredParticle;

    public GameObject healEffect;
    public GameObject healParticle;

    Control control;
    Unit unit;
    public List<string> death = new List<string>();
    public List<Vector3> location = new List<Vector3>();
    public GameObject dps;
    public GameObject dpsdps;
    public GameObject dpstank;
    public GameObject dpsheal;
    public GameObject heal;
    public GameObject healdps;
    public GameObject healtank;
    public GameObject healheal;
    public GameObject tank;
    public GameObject tankdps;
    public GameObject tanktank;
    public GameObject tankheal;

    GameObject InstantiateUnit;

    void Start()
    {
        unit = GetComponent<Unit>();
        control = GameObject.Find("Object_control").GetComponent<Control>();
    }


    public IEnumerator Heal_Skill()
    {
        float lowHp = 2;
        GameObject healUnit = null;
        foreach (GameObject units in control.playerlist)
        {
            if (lowHp >= units.GetComponent<Unit>().nowHp / units.GetComponent<Unit>().maxHp)
            {
                lowHp = units.GetComponent<Unit>().nowHp / units.GetComponent<Unit>().maxHp;
                healUnit = units;
            }
        }
        GameObject healEffect1 = Instantiate(healEffect);
        healEffect1.GetComponent<Transform>().position = new Vector3(healUnit.gameObject.GetComponent<Transform>().position.x, healUnit.gameObject.GetComponent<Transform>().position.y, 0);
        GameObject healEffect2 = Instantiate(healParticle);
        healEffect2.GetComponent<Transform>().position = new Vector3(healUnit.gameObject.GetComponent<Transform>().position.x, healUnit.gameObject.GetComponent<Transform>().position.y + 2.0f, 0);
        healEffect1.GetComponent<SpriteRenderer>().sortingOrder = healUnit.GetComponent<MeshRenderer>().sortingOrder;
        healEffect2.GetComponent<SpriteRenderer>().sortingOrder = healUnit.GetComponent<MeshRenderer>().sortingOrder;
        healEffect1.GetComponent<FollowCharacter>().target = healUnit;
        healEffect2.GetComponent<FollowCharacter>().target = healUnit;

        healUnit.GetComponent<Unit>().nowHp += 10;

        StopCoroutine(Heal_Skill());
        yield return 0;
    }

    public IEnumerator Crusader_Skill(List<GameObject> heal)
    {
        foreach (GameObject i in heal)
        {
            i.GetComponent<Unit>().nowHp += 12;
            GameObject healEffect1 = Instantiate(healEffect);
            healEffect1.GetComponent<Transform>().position = new Vector3(i.gameObject.GetComponent<Transform>().position.x, i.gameObject.GetComponent<Transform>().position.y, 0);
            GameObject healEffect2 = Instantiate(healParticle);
            healEffect2.GetComponent<Transform>().position = new Vector3(i.gameObject.GetComponent<Transform>().position.x, i.gameObject.GetComponent<Transform>().position.y + 2.0f, 0);
            healEffect1.GetComponent<SpriteRenderer>().sortingOrder = i.GetComponent<MeshRenderer>().sortingOrder;
            healEffect2.GetComponent<SpriteRenderer>().sortingOrder = i.GetComponent<MeshRenderer>().sortingOrder;
        }
        StopCoroutine(Crusader_Skill(heal));
        yield return 0;
    }

    public IEnumerator Eclipse_Skill()
    {
        float lowHp = 500;
        GameObject healUnit = null;
        foreach (GameObject units in control.playerlist)
        {
            if (units.GetComponent<Unit>().nowHp < units.GetComponent<Unit>().maxHp) //풀피인 애들 빼고
            {
                if (lowHp >= units.GetComponent<Unit>().nowHp)
                {
                    lowHp = units.GetComponent<Unit>().nowHp;
                    healUnit = units;
                }
            }
        }
        healUnit.GetComponent<Unit>().nowHp += 20;

        GameObject healEffect1 = Instantiate(healEffect);
        healEffect1.GetComponent<Transform>().position = new Vector3(healUnit.gameObject.GetComponent<Transform>().position.x, healUnit.gameObject.GetComponent<Transform>().position.y, 0);
        GameObject healEffect2 = Instantiate(healParticle);
        healEffect2.GetComponent<Transform>().position = new Vector3(healUnit.gameObject.GetComponent<Transform>().position.x, healUnit.gameObject.GetComponent<Transform>().position.y + 2.0f, 0);
        healEffect1.GetComponent<SpriteRenderer>().sortingOrder = healUnit.GetComponent<MeshRenderer>().sortingOrder;
        healEffect2.GetComponent<SpriteRenderer>().sortingOrder = healUnit.GetComponent<MeshRenderer>().sortingOrder;
        StopCoroutine(Eclipse_Skill());
        yield return 0;
    }

    public IEnumerator Bishop_Skill()
    {
        if (death[0] == "dps")
        {
            InstantiateUnit = (GameObject)Instantiate(dps);
            InstantiateUnit.GetComponent<Unit>().SetUnitStatus(50, 9.5f, 1.8f, 40f, 5, 0, 0);
            InstantiateUnit.GetComponent<Unit>().shield = 20;
            InstantiateUnit.transform.position = location[0];
            fun(InstantiateUnit);
        }
        else if (death[0] == "dpsdps")
        {
            InstantiateUnit = (GameObject)Instantiate(dpsdps);
            InstantiateUnit.transform.position = location[0];
            InstantiateUnit.GetComponent<Unit>().SetUnitStatus(50, 8, 1.1f, 38.7f, 5.3f, 0, 70);
            InstantiateUnit.GetComponent<Unit>().shield = 20;
            InstantiateUnit.transform.GetComponent<Unit>().evolved = true;
            fun(InstantiateUnit);
        }
        else if (death[0] == "dpstank")
        {
            InstantiateUnit = (GameObject)Instantiate(dpstank);
            InstantiateUnit.transform.position = location[0];
            InstantiateUnit.GetComponent<Unit>().SetUnitStatus(50, 25, 2.8f, 40f, 4.5f, 0, 0);
            InstantiateUnit.GetComponent<Unit>().shield = 20;
            InstantiateUnit.transform.GetComponent<Unit>().evolved = true;
            fun(InstantiateUnit);
        }
        else if (death[0] == "dpsheal")
        {
            InstantiateUnit = (GameObject)Instantiate(dpsheal);
            InstantiateUnit.transform.position = location[0];
            InstantiateUnit.GetComponent<Unit>().SetUnitStatus(50, 8, 1.6f, 40, 4.7f, 0, 80);
            InstantiateUnit.GetComponent<Unit>().shield = 20;
            InstantiateUnit.transform.GetComponent<Unit>().evolved = true;
            fun(InstantiateUnit);
        }
        else if (death[0] == "tank")
        {
            InstantiateUnit = (GameObject)Instantiate(tank);
            InstantiateUnit.GetComponent<Unit>().SetUnitStatus(50, 7f, 2.2f, 1.5f, 4.8f, 0, 0);
            InstantiateUnit.GetComponent<Unit>().shield = 20;
            InstantiateUnit.transform.position = location[0];
            fun(InstantiateUnit);
        }
        else if (death[0] == "tankdps")
        {
            InstantiateUnit = (GameObject)Instantiate(tankdps);
            InstantiateUnit.transform.position = location[0];
            InstantiateUnit.GetComponent<Unit>().SetUnitStatus(50, 8, 1.8f, 25, 4.8f, 0, 40);
            InstantiateUnit.GetComponent<Unit>().shield = 20;
            InstantiateUnit.transform.GetComponent<Unit>().evolved = true;
            fun(InstantiateUnit);
        }
        else if (death[0] == "tanktank")
        {
            InstantiateUnit = (GameObject)Instantiate(tanktank);
            InstantiateUnit.transform.position = location[0];
            InstantiateUnit.GetComponent<Unit>().SetUnitStatus(50, 8.5f, 2.4f, 1.5f, 3.8f, 0, 0);
            InstantiateUnit.GetComponent<Unit>().shield = 20;
            InstantiateUnit.transform.GetComponent<Unit>().evolved = true;
            fun(InstantiateUnit);
        }
        else if (death[0] == "tankheal")
        {
            InstantiateUnit = (GameObject)Instantiate(tankheal);
            InstantiateUnit.transform.position = location[0];
            InstantiateUnit.GetComponent<Unit>().SetUnitStatus(50, 6, 2, 1.5f, 4.8f, 0, 0);
            InstantiateUnit.GetComponent<Unit>().shield = 20;
            InstantiateUnit.transform.GetComponent<Unit>().evolved = true;
            fun(InstantiateUnit);
        }
        else if (death[0] == "heal")
        {
            InstantiateUnit = (GameObject)Instantiate(heal);
            InstantiateUnit.GetComponent<Unit>().SetUnitStatus(50, 4.5f, 2, 35, 4.5f, 0, 50);
            InstantiateUnit.GetComponent<Unit>().shield = 20;
            InstantiateUnit.transform.position = location[0];
            fun(InstantiateUnit);
        }
        else if (death[0] == "healdps")
        {
            InstantiateUnit = (GameObject)Instantiate(healdps);
            InstantiateUnit.transform.position = location[0];
            InstantiateUnit.GetComponent<Unit>().SetUnitStatus(50, 7, 2, 40, 4.3f, 0, 50);
            InstantiateUnit.GetComponent<Unit>().shield = 20;
            InstantiateUnit.transform.GetComponent<Unit>().evolved = true;
            fun(InstantiateUnit);
        }
        else if (death[0] == "healtank")
        {
            InstantiateUnit = (GameObject)Instantiate(healtank);
            InstantiateUnit.transform.position = location[0];
            InstantiateUnit.GetComponent<Unit>().SetUnitStatus(50, 7, 2.0f, 2f, 4.7f, 0, 70);
            InstantiateUnit.GetComponent<Unit>().shield = 20;
            InstantiateUnit.transform.GetComponent<Unit>().evolved = true;
            fun(InstantiateUnit);
        }
        else if (death[0] == "healheal")
        {
            InstantiateUnit = (GameObject)Instantiate(healheal);
            InstantiateUnit.transform.position = location[0];
            InstantiateUnit.GetComponent<Unit>().SetUnitStatus(50, 4.5f, 2, 37.5f, 4.9f, 0, 70);
            InstantiateUnit.GetComponent<Unit>().shield = 20;
            InstantiateUnit.transform.GetComponent<Unit>().evolved = true;
            fun(InstantiateUnit);
        }
        death.RemoveAt(0);
        location.RemoveAt(0);
        yield return 0;
    }

    public IEnumerator Sacred_Skill(List<GameObject> dmgBuff)
    {
        foreach (GameObject i in dmgBuff)
        {
            StartCoroutine(i.GetComponent<Unit>().DmgBuff());
            GameObject Effect2 = Instantiate(sacredParticle);
            Effect2.GetComponent<Transform>().position = new Vector3(i.gameObject.GetComponent<Transform>().position.x, i.gameObject.GetComponent<Transform>().position.y + 2, 0);
            Effect2.GetComponent<SpriteRenderer>().sortingOrder = i.GetComponent<MeshRenderer>().sortingOrder + 1;
            Effect2.GetComponent<FollowCharacter>().target = i;
        }
        StopCoroutine(Sacred_Skill(dmgBuff));
        yield return 0;
    }

    public IEnumerator Ranger_Skill(Attack_Bow ab)
    {
        ab.Shoot(2);
        yield return new WaitForSeconds(0.6f);
        ab.Shoot(2);
    }

    public IEnumerator Tempest_Skill(GameObject unit)
    {
        float past_atkSpeed;
        unit.GetComponent<Tank_fsm>().stopMp = true;
        past_atkSpeed = unit.GetComponent<Unit>().atkSpeed;
        unit.GetComponent<Unit>().atkSpeed = 1.2f;
        yield return new WaitForSeconds(6f);
        unit.GetComponent<Unit>().atkSpeed = past_atkSpeed;
        unit.GetComponent<Tank_fsm>().stopMp = false;

    }

    void fun(GameObject unit)
    {
        UnitSelections.Instance.unitList.Add(unit.gameObject);
        unit.transform.GetComponent<Unit>().canvas = GameObject.Find("UI(Overlay)");
        unit.transform.GetComponent<Unit>().newChar = true;
        unit.transform.GetComponent<Tank_UnitMovement>().Des = GameObject.Find("Destination");
        unit.transform.GetComponent<Tank_PlayerTarget>().Des = GameObject.Find("Destination");
    }
}

