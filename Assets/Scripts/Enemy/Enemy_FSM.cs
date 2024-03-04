using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using Assets.FantasyMonsters.Scripts;

public class Enemy_FSM : MonoBehaviour
{
    Enemy enemy;
    TargetList targetlist;
    public GameObject target;
    float DelayTime = 0.9f;
    float startTime = 0;
    public float currentTime = 0;
    public bool fight = false;
    bool on = false;
    public float shrtDis;
    Monster monster;
    SoundEffect soundeffect;
    AudioSource audiosource;
    AudioSource Se;
    Control control;
    Rigidbody2D rigid2d;
    public bool stop = false;
    public enum CharacterStates
    {
        Idle,
        Move,
        Attack,  //공격
        Stand_by, //공격 후 대기 시간
        Death
    }
    private StateMachine<CharacterStates, StateDriverUnity> fsm;

    void Start()
    {
        targetlist = GameObject.Find("TargetList").GetComponent<TargetList>();
        enemy = gameObject.GetComponent<Enemy>();
        fsm = new StateMachine<CharacterStates, StateDriverUnity>(this);
        fsm.ChangeState(CharacterStates.Idle);
        monster = gameObject.GetComponent<Monster>();
        soundeffect = GameObject.Find("Audio_Source").GetComponent<SoundEffect>();
        audiosource = GetComponent<AudioSource>();
        Se = GameObject.Find("Audio_Source").GetComponent<AudioSource>();
        control = GameObject.Find("Object_control").GetComponent<Control>();
        rigid2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        audiosource.volume = Se.volume;
        Target_Setting();
        fsm.Driver.Update.Invoke();
        currentTime = Time.time - startTime;
        if (target != null)
        {
            if (gameObject.GetComponent<Transform>().position.x < target.GetComponent<Transform>().position.x)
            {
                if (gameObject.GetComponent<Transform>().localScale.x > 0)
                {
                    gameObject.GetComponent<Transform>().localScale = new Vector3(-gameObject.GetComponent<Transform>().localScale.x, gameObject.GetComponent<Transform>().localScale.y, 0);
                }
            }
            else if (gameObject.GetComponent<Transform>().position.x > target.GetComponent<Transform>().position.x)
            {
                if (gameObject.GetComponent<Transform>().localScale.x < 0)
                {
                    gameObject.GetComponent<Transform>().localScale = new Vector3(-gameObject.GetComponent<Transform>().localScale.x, gameObject.GetComponent<Transform>().localScale.y, 0);
                }
            }
        }
    }

    void Idle_Enter()
    {

    }
    void Idle_Update()
    {
        rigid2d.mass = 100;
        if (enemy.nowHp > 0)
        {
            if (fight)
            {
                if (currentTime >= DelayTime)
                {
                    if (on == false)
                    {
                        on = true;
                        fsm.ChangeState(CharacterStates.Attack);
                    }
                }
                else
                {
                    fsm.ChangeState(CharacterStates.Idle);
                    monster.SetState(MonsterState.Idle);
                }
            }
            else
            {
                if (!stop)
                {
                    if (target != null)
                    {
                        fsm.ChangeState(CharacterStates.Move);
                    }
                    else
                    {
                        fsm.ChangeState(CharacterStates.Idle);
                        monster.SetState(MonsterState.Idle);
                    }
                }
            }
        }
    }
    void Move_Enter()
    {
        monster.SetState(MonsterState.Walk);
    }
    void Move_Update()
    {
        rigid2d.mass = 1;
        if (target == null)
        {
            fsm.ChangeState(CharacterStates.Idle);
            monster.SetState(MonsterState.Idle);
        }
        else
        {
            StartCoroutine(Enemy_move());
        }
    }
    void Attack_Enter()
    {
        StopCoroutine(Enemy_move());
        StartCoroutine(Enemy_Attack());
    }
    void Attack_Update()
    {
        rigid2d.mass = 100;
        // 움직이면 MOVE로 바꾸는 코드
        //if (gameObject.GetComponent<Heal_Unitmovement>().arrived == false)
        //{
        //    fsm.ChangeState(CharacterStates.Move);
        //}
    }

    IEnumerator Enemy_move()
    {
        //monster.SetState(MonsterState.Walk);
        this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, target.transform.position, 3 * Time.deltaTime);
        yield return 0;
    }

    IEnumerator Enemy_Attack()
    {
        stop = true;
        startTime = Time.time;
        on = false;
        // 공격하는 시간
        yield return new WaitForSeconds(0.5f);        // 타겟이 있다면 타겟은 데미지를 받음
        monster.Attack();
        string name = gameObject.name;
        if (name.StartsWith("Scarab") || name.StartsWith("Caterpillar"))
        {
            StartCoroutine(soundeffect.bugAtkE(audiosource));
        }
        else if (name.StartsWith("Wolf"))
        {
            StartCoroutine(soundeffect.wolfAtkE(audiosource));
        }
        if (target != null)
        {
            if (!target.GetComponent<Unit>().attacking_enemy.Contains(this.gameObject))
            {
                target.GetComponent<Unit>().attacking_enemy.Add(this.gameObject);
            }
            target.GetComponent<Unit>().TakeDamage(enemy.dmg);
        }
        yield return new WaitForSeconds(1f);  //이시간동안 움직이지 못함(공격하면서 이동 방지)
        stop = false;
        fsm.ChangeState(CharacterStates.Idle);
    }

    public void Target_Setting()
    {
        if (targetlist.targetAttack.Count > 0)
        {
            if (target == null)
            {
                shrtDis = Vector3.Distance(gameObject.transform.position, targetlist.targetAttack[0].transform.position);
                target = targetlist.targetAttack[0];
                foreach (GameObject found in targetlist.targetAttack)
                {
                    float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                    if (Distance < shrtDis)
                    {
                        target = found;
                        shrtDis = Distance;
                    }
                }
                if (shrtDis >= 1000) //거리가 너무 멀면 쫒아가지 않기
                {
                    target = null;
                }
            }
            else // 
            {
                if (!fight)
                {
                    shrtDis = Vector3.Distance(gameObject.transform.position, targetlist.targetAttack[0].transform.position);
                    target = targetlist.targetAttack[0];
                    foreach (GameObject found in targetlist.targetAttack)
                    {
                        float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                        if (Distance < shrtDis)
                        {
                            target = found;
                            shrtDis = Distance;
                        }
                    }
                    if (shrtDis >= 1000) //거리가 너무 멀면 쫒아가지 않기
                    {
                        target = null;
                    }
                }
            }
        }
        else //공격중인 아군이 없을 경우
        {
            if (control.playerlist.Count > 0)
            {
                if (target == null)
                {
                    shrtDis = Vector3.Distance(gameObject.transform.position, targetlist.targetIdle[0].transform.position);
                    target = targetlist.targetIdle[0];
                    foreach (GameObject found in targetlist.targetIdle)
                    {
                        float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                        if (Distance < shrtDis)
                        {
                            target = found;
                            shrtDis = Distance;
                        }
                    }
                    if (shrtDis >= 1000) //거리가 너무 멀면 쫒아가지 않기
                    {
                        target = null;
                    }
                }
            }
        }
    }
    //public void IdleTarget_Setting()
    //{
    //    if (target == null)
    //    {
    //        shrtDis = Vector3.Distance(gameObject.transform.position, targetlist.targetIdle[0].transform.position);
    //        target = targetlist.targetIdle[0];
    //        foreach (GameObject found in targetlist.targetIdle)
    //        {
    //            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

    //            if (Distance < shrtDis)
    //            {
    //                target = found;
    //                shrtDis = Distance;
    //            }
    //        }
    //    }
    //}

    public void FSM_Move()
    {
        fsm.ChangeState(CharacterStates.Move);
    }

    public void FSM_Idle()
    {
        fsm.ChangeState(CharacterStates.Idle);
    }
}
