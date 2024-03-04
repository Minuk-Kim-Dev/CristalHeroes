using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class Unit : MonoBehaviour
{
    public GameObject blessing;
    public GameObject guardianHit1;
    public GameObject hitEffect;
    public GameObject wearOut;
    public GameObject pollution;
    public GameObject corrosion;
    public GameObject breakDebuff;
    GameObject debuff;

    public GameObject[] prfUpgradeLevel;
    public GameObject prfHpBar;
    public GameObject prfMpBar;
    public GameObject prfShieldBar;
    public GameObject prfUnitStats;
    public GameObject canvas;
    Image nowHpBar;
    Image nowMpBar;

    Control control;
    TargetList targetlist;
    public RectTransform hpBar;
    public RectTransform mpBar;
    public RectTransform shieldBar;
    public RectTransform upgradeBar;

    public RectTransform unitStats;

    UnitSelections unitSelection;
    public bool attacking = false;
    public bool switching = false;
    public float height = 1;
    private bool debuffOn = false;
    public int random_debuff = 0;
    //계통별 보너스
    public float shield;
    public int attackCnt;
    public float healBonus;

    public int durability = 0;      //max는 50
    public float maxHp;
    public float nowHp;
    public float maxMp = 100f;
    public float nowMp = 0;
    public float dmg;
    public float atkSpeed;
    public float speed;
    public int upgradeCnt;      //강화 횟수
    public bool canEvolution;       //진화 가능한지
    public bool evolved;        //진화한 개체인지
    public bool newChar = false;
    public bool die;
    string tagname;
    public bool haveWindow;
    public static int statWindowCnt = 0;
    public bool healOneTime = false;

    public bool usingSkill = false;

    public bool durabilltyReset = false;
    public bool stopMp = false;

    CircleCollider2D circleCollider;
    Skill skill;
    //계통 관리 오브젝트
    public GameObject knightManager;
    public GameObject archerManager;
    public GameObject priestManager;

    public List<GameObject> attacking_enemy = new List<GameObject>();

    public void SetUnitStatus(float _nowHp, float _dmg, float _atkSpeed, float _atkRange, float _speed, int _durability, int _maxMp)
    {
        nowHp = _nowHp;
        dmg = _dmg;
        atkSpeed = _atkSpeed;
        speed = _speed;
        durability = _durability;
        circleCollider.radius = _atkRange;
        maxMp = _maxMp;
    }

    private void Awake()
    {
        skill = GameObject.Find("Object_control").GetComponent<Skill>();
        targetlist = GameObject.Find("TargetList").GetComponent<TargetList>();
        control = GameObject.Find("Object_control").GetComponent<Control>();
        circleCollider = transform.Find("AttackRange").gameObject.GetComponent<CircleCollider2D>();
        canvas = GameObject.Find("UI(Overlay)");
        blessing = GameObject.Find("UI(Camera)").transform.GetChild(4).gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        haveWindow = false;
        tag = gameObject.tag;
        knightManager = GameObject.Find("KnightManager");
        archerManager = GameObject.Find("ArcherManager");
        priestManager = GameObject.Find("PriestManager");

        unitSelection = GameObject.Find("UnitSelections").GetComponent<UnitSelections>();
        control = GameObject.Find("Object_control").GetComponent<Control>();
        die = false;
        upgradeCnt = 0;
        canEvolution = false;
        if (gameObject.tag == "tank")
        {
            SetUnitStatus(100f, 7f, 2.2f, 1.5f, 4.8f, 0, 0);
        }
        else if (gameObject.tag == "dps")
        {
            SetUnitStatus(60, 9.5f, 1.5f, 40f, 5, 0, 0);
        }
        else if (gameObject.tag == "heal")
        {
            SetUnitStatus(80, 4.0f, 2, 35, 4.5f, 0, 40);
        }
        if (evolved == false)
        {
            upgradeBar = Instantiate(prfUpgradeLevel[0], canvas.transform).GetComponent<RectTransform>();
        }
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        mpBar = Instantiate(prfMpBar, canvas.transform).GetComponent<RectTransform>();
        nowHpBar = hpBar.transform.GetChild(0).GetComponent<Image>();
        nowMpBar = mpBar.transform.GetChild(0).GetComponent<Image>();

        UnitSelections.Instance.unitList.Add(this.gameObject);
    }

    void Update()
    {
        if (shield <= 0 && shieldBar != null)
        {
            Destroy(shieldBar.gameObject);
            shieldBar = null;
        }
        else if(shield > 0 && shieldBar == null)
        {
            GetShield();
        }
        SetLayer();
        tagname = gameObject.tag;

        if (nowHp > maxHp)
        {
            nowHp = maxHp;
        }
        if (nowMp > maxMp)
        {
            nowMp = maxMp;
        }

        if (!unitSelection.unitsSelected.Contains(this.gameObject) && haveWindow == true)
        {
            haveWindow = false;
            Destroy(unitStats.gameObject);
        }

        if (newChar == true)
        {
            Invoke("CreateAnimTime", 1.8f);
        }
        if (durability == 15 && debuffOn == false)
        {
            Durability();
            debuffOn = true;
        }
        if (durabilltyReset == true)
        {
            DurabilityReset();
        }

        nowHpBar.fillAmount = (float)nowHp / (float)maxHp;
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + (float)4.5, 0));
        hpBar.position = _hpBarPos;

        if (shieldBar != null)
        {
            Vector3 _shieldBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + (float)4.5, 0));
            shieldBar.position = _shieldBarPos;
        }

        nowMpBar.fillAmount = (float)nowMp / (float)maxMp;
        Vector3 _mpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + (float)4.3, 0));
        mpBar.position = _mpBarPos;

        if (evolved == false)
        {
            Vector3 _upgradeBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + (float)4.1, 0));
            upgradeBar.position = _upgradeBarPos;
        }

        if (attacking == true)
        {
            Duplication_att();
            if (switching)
            {
                switching = false;
                targetlist.targetIdle.Remove(this.gameObject);
                targetlist.targetAttack.Add(this.gameObject);
            }
        }
        else
        {
            Duplication_idle();

            if (switching)
            {
                switching = false;
                targetlist.targetAttack.Remove(this.gameObject);
                targetlist.targetIdle.Add(this.gameObject);
            }
        }

        if (upgradeCnt >= 2)  //강화 2회 이상일 경우
        {
            canEvolution = true;
        }
        else
        {
            canEvolution = false;
        }
    }

    private void OnDestroy()
    {
        UnitSelections.Instance.unitList.Remove(this.gameObject);
        skill.death.Add(this.gameObject.tag);
        skill.location.Add(this.gameObject.transform.position);
        control.debuff.Remove(this.gameObject);
    }

    void CreateAnimTime()
    {
        newChar = false;
    }

    public void GetShield()
    {
        if (shield > 0)
        {
            shieldBar = Instantiate(prfShieldBar, canvas.transform).GetComponent<RectTransform>();
        }
    }

    public void TakeDamage(float damage)
    {
        GameObject hit = Instantiate(hitEffect);
        hit.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y + 1.0f, 0);
        hit.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<MeshRenderer>().sortingOrder + 1;
        if (gameObject.tag == "tanktank")
        {
            GameObject hit1 = Instantiate(guardianHit1);
            hit1.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y + 5.5f, 0);
            hit1.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<MeshRenderer>().sortingOrder + 1;
        }

        if (random_debuff == 2)
        {
            nowHp = nowHp - (damage + 5);
        }
        else
        {
            if (gameObject.tag == "tanktank")       //가디언 데미지 감소 패시브
            {
                if (shield > 0)
                {
                    shield = shield - damage;
                }
                else
                {
                    nowHp = nowHp - (Mathf.FloorToInt((float)damage * 0.5f));
                }
            }
            else
            {
                if (shield > 0)
                {
                    shield = shield - damage;
                }
                else
                {
                    nowHp = nowHp - damage;
                }
            }
        }
        if (gameObject.tag == "tank" || gameObject.tag == "tanktank" || gameObject.tag == "tankdps" || gameObject.tag == "tankheal")
        {
            durability += 1;
        }
        else if (gameObject.tag == "heal" || gameObject.tag == "healtank" || gameObject.tag == "healdps" || gameObject.tag == "healheal")
        {
            if (healOneTime == false)
            {
                healOneTime = true;
            }
            else
            {
                healOneTime = false;
                durability += 1;
            }
        }
        if (!stopMp)
            nowMp += 5;
        Die();
    }

    public void Die()
    {
        if (nowHp <= 0 && die == false)
        {
            die = true;
            skill.death.Add(this.gameObject.tag);
            skill.location.Add(this.gameObject.transform.position);
            control.debuff.Remove(this.gameObject);
            string tag = gameObject.tag;
            if (tag.StartsWith("dps"))
            {
                GetComponent<DPS_fsm>().enabled = false;
            }
            else if (tag.StartsWith("tank"))
            {
                GetComponent<Tank_fsm>().enabled = false;
            }
            else if (tag.StartsWith("heal"))
            {
                GetComponent<Heal_fsm>().enabled = false;
            }
            if (targetlist.targetAttack.Contains(this.gameObject))
            {
                targetlist.targetAttack.Remove(this.gameObject);
            }
            Invoke("DeleteChar", 1.2f);
        }
    }

    void DeleteChar()
    {
        unitSelection.unitList.Remove(this.gameObject);
        unitSelection.unitsSelected.Remove(this.gameObject);

        if (attacking == true)
        {
            targetlist.targetAttack.Remove(this.gameObject);
        }
        else
        {
            targetlist.targetIdle.Remove(this.gameObject);
        }
        control.playerlist.Remove(this.gameObject);
        Destroy(hpBar.gameObject);
        Destroy(mpBar.gameObject);
        if(shieldBar != null)
        {
            Destroy(shieldBar.gameObject);
        }
        if (evolved == false)
        {
            Destroy(upgradeBar.gameObject);
        }
        Destroy(gameObject);
        Debug.Log("유닛이 죽었습니다.");
    }

    void Duplication_att()
    {
        if (targetlist.targetAttack.Contains(this.gameObject))
        {
            switching = false;
        }
        else
        {
            switching = true;
        }
    }

    void Duplication_idle()
    {
        if (targetlist.targetIdle.Contains(this.gameObject))
        {
            switching = false;
        }
        else
        {
            switching = true;
        }
    }

    void TargetReset()
    {

    }

    void Durability()
    {
        // 1 - 파손 breakDebuff / 2 - 마모(TakeDmg)에 구현 wearOut / 3 - 오염 pollution / 4 - 침식 corrosion
        random_debuff = Random.Range(1, 5);
        if (tagname.StartsWith("tank") || gameObject.tag == "dps" || gameObject.tag == "dpsdps" || gameObject.tag == "dpstank")
        {
            while (random_debuff == 3)
            {
                random_debuff = Random.Range(1, 5);
            }
        }
        if (random_debuff == 1)
        {
            dmg = dmg - 2;
            debuff = Instantiate(breakDebuff);
            debuff.GetComponent<Transform>().position = new Vector3(0, 5.0f, 0);
            debuff.transform.SetParent(gameObject.transform, false);
            debuff.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<MeshRenderer>().sortingOrder;
        }
        else if (random_debuff == 2)
        {
            debuff = Instantiate(wearOut);
            debuff.GetComponent<Transform>().position = new Vector3(0, 5.0f, 0);
            debuff.transform.SetParent(gameObject.transform, false);
            debuff.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<MeshRenderer>().sortingOrder;
        }
        else if (random_debuff == 3)
        {
            maxMp = maxMp + 20;
            debuff = Instantiate(pollution);
            debuff.GetComponent<Transform>().position = new Vector3(0, 3.0f, 0);
            debuff.transform.SetParent(gameObject.transform, false);
            debuff.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<MeshRenderer>().sortingOrder;
            debuff.GetComponent<Animator>().SetTrigger("Entry");
            debuff.GetComponent<Animator>().SetBool("Pollution", true);
        }
        else if (random_debuff == 4)
        {
            if (speed - 1 <= 3)
            {
                speed = 3;
            }
            else
            {
                speed = speed - 1;
            }
            if (circleCollider.radius - 1 <= 1.5f)
            {
                circleCollider.radius = 1.5f;
            }
            else
            {
                circleCollider.radius = circleCollider.radius - 1f;
            }
            debuff = Instantiate(corrosion);
            debuff.GetComponent<Transform>().position = new Vector3(0, 3.0f, 0);
            debuff.transform.SetParent(gameObject.transform, false);
            debuff.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<MeshRenderer>().sortingOrder;
            debuff.GetComponent<Animator>().SetTrigger("Entry");
            debuff.GetComponent<Animator>().SetBool("Corrosion", true);
        }
    }
    public void DurabilityReset()
    {
        durabilltyReset = false;
        durability = 0;
        if (random_debuff == 1)
        {
            dmg = dmg + 2;
        }
        else if (random_debuff == 2)
        {

        }
        else if (random_debuff == 3)
        {
            maxMp = maxHp - 20;
        }
        else if (random_debuff == 4)
        {
            if (tagname.StartsWith("dps"))
            {
                if (speed + 1 >= 5)
                    speed = 5;
                else
                    speed = speed + 1;

                if (circleCollider.radius + 1 >= 6f)
                    circleCollider.radius = 6f;
                else
                    circleCollider.radius = circleCollider.radius + 1f;
            }
            else if (tagname.StartsWith("heal"))
            {
                if (speed + 1 >= 5)
                    speed = 5;
                else
                    speed = speed + 1;

                if (circleCollider.radius + 1 >= 4f)
                    circleCollider.radius = 4f;
                else
                    circleCollider.radius = circleCollider.radius + 1f;
            }
            else
            {
                if (speed + 1 >= 5)
                    speed = 5;
                else
                    speed = speed + 1;

                if (circleCollider.radius + 1 >= 1.5f)
                    circleCollider.radius = 1.5f;
                else
                    circleCollider.radius = circleCollider.radius + 1f;
            }
        }
        random_debuff = 0;
        Destroy(debuff.gameObject);
    }

    public IEnumerator DmgBuff()
    {
        dmg += 10;
        yield return new WaitForSeconds(5f);
        dmg -= 10;
    }

    void SetLayer()
    {
        int layer = (int)gameObject.GetComponent<Transform>().position.y * -10;
        GetComponent<MeshRenderer>().sortingOrder = layer + 100;
    }
}