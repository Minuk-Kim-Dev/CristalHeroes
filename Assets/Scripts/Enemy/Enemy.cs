using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.FantasyMonsters.Scripts;

public class Enemy : MonoBehaviour
{
    public GameObject hitEffect;

    public GameObject prfHpBar;
    public GameObject canvas;
    RectTransform hpBar;
    Image nowHpBar;
    public List<GameObject> projectile = new List<GameObject>();
    //public List<GameObject> player = new List<GameObject>();
    public int height = 2;
    Control control;
    private Camera myCam;
    float MaxDistance = 15f;
    Vector3 MousePosition;
    public string enemyname;
    public float maxHp;
    public float nowHp;
    public float dmg;
    public float atkSpeed;
    public bool ifdie = false;
    GameObject TankJewely;
    GameObject DPSJewely;
    GameObject HealJewely;
    int dropNum;
    int dropRate;
    Monster monster;
    UnitSelections us;
    private void SetEnemyStatus(string _name, int _maxHp, int _dmg, float _atkSpeed)
    {
        enemyname = _name;
        maxHp = _maxHp;
        nowHp = _maxHp;
        dmg = _dmg;
        atkSpeed = _atkSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("UI(Overlay)");
        us = GameObject.Find("UnitSelections").GetComponent<UnitSelections>();
        myCam = Camera.main;
        enemyname = gameObject.name;
        dropNum = 0;
        dropRate = 0;
        TankJewely = GameObject.Find("TankJewely");
        DPSJewely = GameObject.Find("DPSJewely");
        HealJewely = GameObject.Find("HealJewely");
        control = GameObject.Find("Object_control").GetComponent<Control>();
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();

        if (enemyname.StartsWith("Scarab"))
        {
            SetEnemyStatus("enemy", 140, 4, 1);
        }
        else if (enemyname.StartsWith("Caterpillar"))
        {
            SetEnemyStatus("enemy", 180, 7, 1);
        }

        else if (enemyname.StartsWith("Boar"))
        {
            SetEnemyStatus("enemy", 380, 25, 1);
        }

        else if (enemyname.StartsWith("Wolf"))
        {
            SetEnemyStatus("enemy", 480, 35, 1);
        }

        else if (enemyname.StartsWith("GreenOgre"))
        {
            SetEnemyStatus("enemy", 5000, 100, 1);
        }
        else if (enemyname.StartsWith("tutomob"))
        {
            SetEnemyStatus("enemy", 60, 7, 1);
        }

        nowHpBar = hpBar.transform.GetChild(0).GetComponent<Image>();
        control.enemylist.Add(this.gameObject);
        monster = gameObject.GetComponent<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        if(us.unitsSelected.Count > 0)
        {
            MousePosition = Input.mousePosition;
            MousePosition = myCam.ScreenToWorldPoint(MousePosition);
            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance);
            if (Input.GetMouseButtonDown(1))
            {
                if (hit)
                {
                    if (hit.collider.gameObject.tag == "enemy")
                        hit.collider.gameObject.transform.GetChild(4).gameObject.SetActive(true);
                }
                Invoke("TargetingEffectTime", 0.5f);
            }
        }

        if (!ifdie)
        {
            Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
            hpBar.position = _hpBarPos;
            nowHpBar.fillAmount = (float)nowHp / (float)maxHp;
        }

        if (nowHp <= 0 && ifdie == false)
        {
            nowHpBar.gameObject.SetActive(false);
            hpBar.gameObject.SetActive(false);
            ifdie = true;
            Projectile_destroy();
            StartCoroutine(Die());
        }
        //if (player.Count > 0)
        //{
        //    foreach (GameObject unit in player)
        //    {
        //        if (unit == enemy_fsm.target)
        //        {
        //            playerRun = false;
        //        }
        //    }
        //    if (playerRun)
        //    {
        //        enemy_fsm.target = null;
        //    }
        //    else
        //    {
        //        playerRun = true;
        //    }
        //}
    }

    public void TakeDamage(float damage)
    {
        GameObject hit = Instantiate(hitEffect);
        hit.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y + 0.5f, 0);
        if (nowHp - damage < 0)
        {
            nowHp = 0;
        }
        else
        {
            nowHp = nowHp - damage;
        }
    }

    IEnumerator Die()
    {
        foreach (GameObject target in control.playerlist)
        {
            string tag = target.gameObject.tag;
            if (tag.StartsWith("dps"))
            {
                target.GetComponent<DPS_fsm>().target = null;
                target.GetComponent<DPS_fsm>().targetList.Remove(this.gameObject);
            }
            else if (tag.StartsWith("tank"))
            {
                target.GetComponent<Tank_fsm>().target = null;
                target.GetComponent<Tank_fsm>().targetList.Remove(this.gameObject);
            }
            else
            {
                target.GetComponent<Heal_fsm>().target = null;
                target.GetComponent<Heal_fsm>().targetList.Remove(this.gameObject);
            }
            target.GetComponent<Unit>().attacking_enemy.Remove(this.gameObject);
        }
        control.enemylist.Remove(this.gameObject);
        GetComponent<Enemy_FSM>().enabled = false;
        gameObject.tag = "Untagged";
        gameObject.layer = default;
        monster.Die();
        Invoke("Unit_Destroy", 1.0f);

        //dropRate가 0이면 기사, 1이면 궁수, 2이면 사제 보석 드랍
        //dropNum만큼 보석 획득
        dropRate = Random.Range(0, 3);
        dropNum = Random.Range(1, 4);
        if (dropRate == 0)
        {
            Debug.Log("기사 보석을 " + dropNum + "개 만큼 획득했습니다.");
            TankJewely.GetComponent<TankDragDrop>().count += dropNum;
        }
        else if (dropRate == 1)
        {
            Debug.Log("궁수 보석을 " + dropNum + "개 만큼 획득했습니다.");
            DPSJewely.GetComponent<DpsDragDrop>().count += dropNum;
        }
        else if (dropRate == 2)
        {
            Debug.Log("사제 보석을 " + dropNum + "개 만큼 획득했습니다.");
            HealJewely.GetComponent<HealDragDrop>().count += dropNum;
        }
        yield return 0;
    }

    void Unit_Destroy()
    {
        Destroy(nowHpBar);
        Destroy(hpBar.gameObject);
        Destroy(gameObject);
    }

    void Projectile_destroy()
    {
        for (int i = projectile.Count - 1; i >= 0; i--)
        {
            Destroy(projectile[i]);
        }
    }

    void TargetingEffectTime()
    {
        transform.GetChild(4).gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Projectile_destroy();
    }
}
