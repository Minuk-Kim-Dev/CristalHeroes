using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class TankAnimControler : MonoBehaviour
{
    //public GameObject cut;
    //public GameObject sting;
    //bool effecting;
    //GameObject effect;
    //public float effectTime;

    SkeletonAnimation skeletonAnimation;
    bool arrived;
    Vector2Int startPos, targetPos;
    float xScale;
    Tank_fsm fsm;
    public string currentAnim;
    string changeAnim;
    Unit unit;
    public int attackMotion;
    public string currentSkin;

    void Start()
    {
        //effecting = false;
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        fsm = GetComponent<Tank_fsm>();
        unit = GetComponent<Unit>();
        attackMotion = UnityEngine.Random.Range(0, 2);
    }

    void Update()
    {
        xScale = transform.localScale.x;
        arrived = GetComponent<Tank_UnitMovement>().arrived;
        startPos = GetComponent<Tank_UnitMovement>().startPos;
        targetPos = GetComponent<Tank_UnitMovement>().targetPos;
        currentAnim = skeletonAnimation.AnimationName;
        currentSkin = skeletonAnimation.Skeleton.Skin.ToString();

        if (unit.newChar == false && arrived == false && unit.die == false)
        {
            //fsm.fight = false;
            if (startPos.x > targetPos.x && startPos.y == targetPos.y)
            {
                changeAnim = "knight_sf/knight_sf_run_sword_down";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("knight_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    //skeletonAnimation.AnimationName = "knight_sf/knight_sf_run_sword_down";
                    skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_run_sword_down", true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x < targetPos.x && startPos.y == targetPos.y)
            {
                changeAnim = "knight_sf/knight_sf_run_sword_down";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("knight_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    //skeletonAnimation.AnimationName = "knight_sf/knight_sf_run_sword_down";
                    skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_run_sword_down", true);
                }
                if (xScale > 0) { ReverseScale(); }
            }
            else if (startPos.x == targetPos.x && startPos.y > targetPos.y)
            {
                changeAnim = "knight_f/knight_f_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("knight_f");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    //skeletonAnimation.AnimationName = "knight_f/knight_f_run";
                    skeletonAnimation.AnimationState.SetAnimation(0, "knight_f/knight_f_run", true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x == targetPos.x && startPos.y < targetPos.y)
            {
                changeAnim = "knight_b/knight_b_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("knight_b");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    //skeletonAnimation.AnimationName = "knight_b/knight_b_run";
                    skeletonAnimation.AnimationState.SetAnimation(0, "knight_b/knight_b_run", true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x < targetPos.x && startPos.y < targetPos.y)
            {
                changeAnim = "knight_sb/knight_sb_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("knight_sb");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    //skeletonAnimation.AnimationName = "knight_sb/knight_sb_run";
                    skeletonAnimation.AnimationState.SetAnimation(0, "knight_sb/knight_sb_run", true);
                }
                if (xScale > 0) { ReverseScale(); }
            }
            else if (startPos.x > targetPos.x && startPos.y < targetPos.y)
            {
                changeAnim = "knight_sb/knight_sb_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("knight_sb");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    //skeletonAnimation.AnimationName = "knight_sb/knight_sb_run";
                    skeletonAnimation.AnimationState.SetAnimation(0, "knight_sb/knight_sb_run", true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x > targetPos.x && startPos.y > targetPos.y)
            {
                changeAnim = "knight_sf/knight_sf_run_sword_down";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("knight_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    //skeletonAnimation.AnimationName = "knight_sf/knight_sf_run_sword_down";
                    skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_run_sword_down", true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x < targetPos.x && startPos.y > targetPos.y)
            {
                changeAnim = "knight_sf/knight_sf_run_sword_down";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("knight_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    //skeletonAnimation.AnimationName = "knight_sf/knight_sf_run_sword_down";
                    skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_run_sword_down", true);
                }
                if (xScale > 0) { ReverseScale(); }
            }
        }

        if (unit.newChar == false && arrived == true && fsm.fight == false && unit.die == false)
        {
            if (currentSkin == "knight_f")
            {
                changeAnim = "knight_f/knight_f_idle";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
            }
            else if (currentSkin == "knight_sf")
            {
                changeAnim = "knight_sf/knight_sf_idle";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
            }
            else if (currentSkin == "knight_b")
            {
                changeAnim = "knight_b/knight_b_idle";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
            }
            else if (currentSkin == "knight_sb")
            {
                changeAnim = "knight_sb/knight_sb_idle";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
            }
        }

        if (unit.newChar == false && unit.die == true)
        {
            changeAnim = "knight_sf/knight_sf_die";
            gameObject.GetComponent<Tank_UnitMovement>().enabled = false;
            gameObject.GetComponent<Tank_fsm>().enabled = false;
            gameObject.GetComponent<Tank_PlayerTarget>().enabled = false;
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.Skeleton.SetSkin("knight_sf");
                skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                //skeletonAnimation.AnimationName = "knight_sf/knight_sf_die";
                skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_die", false);
            }
        }

        if (unit.newChar == false && fsm.fight == true && arrived == false)
        {
            targetPos = GetComponent<Tank_UnitMovement>().targetPos;
        }

        if (unit.newChar == false && fsm.fight == true)
        {
            if ((fsm.currentTime < 0.7f || fsm.currentTime > 1.5f) && fsm.currentTime < fsm.DelayTime)
            {
                GameObject target = GetComponent<Tank_fsm>().target;
                Vector3 playerPos = GetComponent<Transform>().position;
                if (unit.die == false && arrived == true && fsm.fight == true && target != null)
                {
                    var position = new Vector2Int((int)target.GetComponent<Transform>().position.x, (int)target.GetComponent<Transform>().position.y);
                    targetPos = position;
                    if (playerPos.x > targetPos.x && playerPos.y == targetPos.y)
                    {
                        changeAnim = "knight_sf/knight_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("knight_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                        if (xScale < 0) { ReverseScale(); }
                    }
                    else if (playerPos.x < targetPos.x && playerPos.y == targetPos.y)
                    {
                        changeAnim = "knight_sf/knight_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("knight_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                        if (xScale > 0) { ReverseScale(); }

                    }
                    else if (playerPos.x == targetPos.x && playerPos.y > targetPos.y)
                    {
                        changeAnim = "knight_sf/knight_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("knight_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                    }
                    else if (playerPos.x == targetPos.x && playerPos.y < targetPos.y)
                    {
                        changeAnim = "knight_sb/knight_sb_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("knight_sb");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                    }
                    else if (playerPos.x < targetPos.x && playerPos.y < targetPos.y)
                    {
                        changeAnim = "knight_sf/knight_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("knight_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                        if (xScale > 0) { ReverseScale(); }
                    }
                    else if (playerPos.x > targetPos.x && playerPos.y < targetPos.y)
                    {
                        changeAnim = "knight_sf/knight_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("knight_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                        if (xScale < 0) { ReverseScale(); }
                    }
                    else if (playerPos.x > targetPos.x && playerPos.y > targetPos.y)
                    {
                        changeAnim = "knight_sf/knight_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("knight_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                        if (xScale < 0) { ReverseScale(); }
                    }
                    else if (playerPos.x < targetPos.x && playerPos.y > targetPos.y)
                    {
                        changeAnim = "knight_sf/knight_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("knight_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                        if (xScale > 0) { ReverseScale(); }
                    }
                }
            }
        }
        if (unit.newChar == false && fsm.fight == true && fsm.currentTime >= 0.7f && fsm.currentTime <= 1.5f)
        {
            GameObject target = GetComponent<Tank_fsm>().target;
            Vector3 playerPos = GetComponent<Transform>().position;

            if (unit.die == false && arrived == true && attackMotion == 1 && fsm.fight == true)
            {
                //Invoke("Effect", 0.1f);
                var position = new Vector2Int((int)target.GetComponent<Transform>().position.x, (int)target.GetComponent<Transform>().position.y);
                targetPos = position;

                if (playerPos.x > targetPos.x && playerPos.y == targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_2";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_attack_2", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_sf/knight_sf_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x < targetPos.x && playerPos.y == targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_2";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_attack_2", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_sf/knight_sf_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale > 0) { ReverseScale(); }

                }
                else if (playerPos.x == targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_2";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_attack_2", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_sf/knight_sf_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                }
                else if (playerPos.x == targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "knight_sb/knight_sb_attack_2";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sb");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sb/knight_sb_attack_2", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_sb/knight_sb_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                }
                else if (playerPos.x < targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_2";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_sf/knight_sf_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale > 0) { ReverseScale(); }
                }
                else if (playerPos.x > targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_2";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_sf/knight_sf_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x > targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_2";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_attack_2", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_sf/knight_sf_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x < targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_2";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_attack_2", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_sf/knight_sf_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale > 0) { ReverseScale(); }
                }
            }
            else if (unit.die == false && arrived == true && attackMotion == 0 && fsm.fight == true && target != null)
            {
                var position = new Vector2Int((int)target.GetComponent<Transform>().position.x, (int)target.GetComponent<Transform>().position.y);
                targetPos = position;
                //Invoke("Effect", 0.2f);
                if (playerPos.x > targetPos.x && playerPos.y == targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_1";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_attack_1", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_sf/knight_sf_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x < targetPos.x && playerPos.y == targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_1";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_attack_1", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_sf/knight_sf_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale > 0) { ReverseScale(); }

                }
                else if (playerPos.x == targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_1";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_attack_1", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_sf/knight_sf_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                }
                else if (playerPos.x == targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "knight_sb/knight_sb_attack_1";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sb");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sb/knight_sb_attack_1", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_sb/knight_sb_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                }
                else if (playerPos.x < targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_1";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_sf/knight_sf_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale > 0) { ReverseScale(); }
                }
                else if (playerPos.x > targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_1";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_sf/knight_sf_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x > targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_1";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_sf/knight_sf_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x < targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_1";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_sf/knight_sf_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale > 0) { ReverseScale(); }
                }
            }
        }
        if (unit.newChar == true)
        {
            changeAnim = "knight_f/knight_f_create";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.Skeleton.SetSkin("knight_f");
                skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                skeletonAnimation.AnimationState.SetAnimation(0, "knight_f/knight_f_create", false);
            }
        }
    }

    public bool SameAnimation(string currentAnim, string changeAnim)
    {
        if (currentAnim == changeAnim)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //void Effect()
    //{
    //    if(effecting == false)
    //    {
    //        effecting = true;
    //        if (attackMotion == 1)
    //        {
    //            effect = Instantiate(cut);
    //            effectTime = 1f;
    //            if (gameObject.GetComponent<Transform>().localScale.x < 0)
    //            {
    //                effect.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x + 2f, gameObject.GetComponent<Transform>().position.y + 1.5f, 0);
    //            }
    //            else if (gameObject.GetComponent<Transform>().localScale.x > 0)
    //            {
    //                effect.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x - 2f, gameObject.GetComponent<Transform>().position.y + 1.5f, 0);
    //            }
    //        }
    //        else if (attackMotion == 0)
    //        {
    //            effect = Instantiate(sting);
    //            effectTime = 1f;
    //            if (gameObject.GetComponent<Transform>().localScale.x < 0)
    //            {
    //                effect.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x + 2f, gameObject.GetComponent<Transform>().position.y + 1.5f, 0);
    //            }
    //            else if(gameObject.GetComponent<Transform>().localScale.x > 0)
    //            {
    //                effect.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x - 2f, gameObject.GetComponent<Transform>().position.y + 1.5f, 0);
    //            }
    //        }

    //        if (gameObject.GetComponent<Transform>().localScale.x < 0)
    //        {
    //            float x = effect.transform.localScale.x;
    //            float y = effect.transform.localScale.y;
    //            float z = effect.transform.localScale.z;
    //            effect.transform.localScale = new Vector3(-x, y, z);
    //        }
    //        Invoke("NoEffect", effectTime);
    //    }
    //}

    //void NoEffect()
    //{
    //    effecting = false;
    //}

    public void ReverseScale()
    {
        float y = transform.localScale.y;
        float z = transform.localScale.z;
        transform.localScale = new Vector3(-xScale, y, z);
    }
}