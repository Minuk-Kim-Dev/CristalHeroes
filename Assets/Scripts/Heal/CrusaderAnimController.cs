using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class CrusaderAnimController : MonoBehaviour
{
    SkeletonAnimation skeletonAnimation;
    bool arrived;
    Vector2Int startPos, targetPos;
    float xScale;
    Heal_fsm fsm;
    public string currentAnim;
    string changeAnim;
    Unit unit;
    public string currentSkin;

    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        fsm = GetComponent<Heal_fsm>();
        unit = GetComponent<Unit>();
    }

    void Update()
    {
        xScale = transform.localScale.x;
        arrived = GetComponent<Heal_Unitmovement>().arrived;
        startPos = GetComponent<Heal_Unitmovement>().startPos;
        targetPos = GetComponent<Heal_Unitmovement>().targetPos;
        currentAnim = skeletonAnimation.AnimationName;
        currentSkin = skeletonAnimation.Skeleton.Skin.ToString();

        if (unit.newChar == false && arrived == false && unit.die == false && GetComponent<Unit>().usingSkill == false)
        {
            //fsm.fight = false;
            if (startPos.x > targetPos.x && startPos.y == targetPos.y)
            {
                changeAnim = "crusader_sf/crusader_sf_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x < targetPos.x && startPos.y == targetPos.y)
            {
                changeAnim = "crusader_sf/crusader_sf_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
                if (xScale > 0) { ReverseScale(); }
            }
            else if (startPos.x == targetPos.x && startPos.y > targetPos.y)
            {
                changeAnim = "crusader_f/crusader_f_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("crusader_f");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x == targetPos.x && startPos.y < targetPos.y)
            {
                changeAnim = "crusader_b/crusader_b_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("crusader_b");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x < targetPos.x && startPos.y < targetPos.y)
            {
                changeAnim = "crusader_sb/crusader_sb_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("crusader_sb");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
                if (xScale > 0) { ReverseScale(); }
            }
            else if (startPos.x > targetPos.x && startPos.y < targetPos.y)
            {
                changeAnim = "crusader_sb/crusader_sb_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("crusader_sb");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x > targetPos.x && startPos.y > targetPos.y)
            {
                changeAnim = "crusader_sf/crusader_sf_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x < targetPos.x && startPos.y > targetPos.y)
            {
                changeAnim = "crusader_sf/crusader_sf_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
                if (xScale > 0) { ReverseScale(); }
            }
        }

        if (unit.newChar == false && arrived == true && fsm.fight == false && GetComponent<Unit>().usingSkill == false)
        {
            if (currentSkin == "crusader_f")
            {
                changeAnim = "crusader_f/crusader_f_idle";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
            }
            else if (currentSkin == "crusader_sf")
            {
                changeAnim = "crusader_sf/crusader_sf_idle";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
            }
            else if (currentSkin == "crusader_b")
            {
                changeAnim = "crusader_b/crusader_b_idle";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
            }
            else if (currentSkin == "crusader_sb")
            {
                changeAnim = "crusader_sb/crusader_sb_idle";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
            }
        }

        if (unit.newChar == false && unit.die == true && GetComponent<Unit>().usingSkill == false)
        {
            changeAnim = "crusader_sf/crusader_sf_die";
            gameObject.GetComponent<Heal_Unitmovement>().enabled = false;
            gameObject.GetComponent<Heal_fsm>().enabled = false;
            gameObject.GetComponent<Heal_PlayerTarget>().enabled = false;
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
            }
        }

        if (unit.newChar == false && fsm.fight == true && arrived == false)
        {
            targetPos = GetComponent<Heal_Unitmovement>().targetPos;
        }

        if (unit.newChar == false && fsm.fight == true && GetComponent<Unit>().usingSkill == false)
        {
            if ((fsm.currentTime < 0.7f || fsm.currentTime > 1.85f) && fsm.currentTime < fsm.DelayTime)
            {
                GameObject target = GetComponent<Heal_fsm>().target;
                Vector3 playerPos = GetComponent<Transform>().position;
                if (unit.die == false && arrived == true && fsm.fight == true && target != null)
                {
                    var position = new Vector2Int((int)target.GetComponent<Transform>().position.x, (int)target.GetComponent<Transform>().position.y);
                    targetPos = position;
                    if (playerPos.x > targetPos.x && playerPos.y == targetPos.y)
                    {
                        changeAnim = "crusader_sf/crusader_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                        if (xScale < 0) { ReverseScale(); }
                    }
                    else if (playerPos.x < targetPos.x && playerPos.y == targetPos.y)
                    {
                        changeAnim = "crusader_sf/crusader_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                        if (xScale > 0) { ReverseScale(); }

                    }
                    else if (playerPos.x == targetPos.x && playerPos.y > targetPos.y)
                    {
                        changeAnim = "crusader_sf/crusader_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                    }
                    else if (playerPos.x == targetPos.x && playerPos.y < targetPos.y)
                    {
                        changeAnim = "crusader_sb/crusader_sb_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("crusader_sb");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                    }
                    else if (playerPos.x < targetPos.x && playerPos.y < targetPos.y)
                    {
                        changeAnim = "crusader_sf/crusader_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                        if (xScale > 0) { ReverseScale(); }
                    }
                    else if (playerPos.x > targetPos.x && playerPos.y < targetPos.y)
                    {
                        changeAnim = "crusader_sf/crusader_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                        if (xScale < 0) { ReverseScale(); }
                    }
                    else if (playerPos.x > targetPos.x && playerPos.y > targetPos.y)
                    {
                        changeAnim = "crusader_sf/crusader_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                        if (xScale < 0) { ReverseScale(); }
                    }
                    else if (playerPos.x < targetPos.x && playerPos.y > targetPos.y)
                    {
                        changeAnim = "crusader_sf/crusader_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                        if (xScale > 0) { ReverseScale(); }
                    }
                }
            }
        }
        if (unit.newChar == false && fsm.fight == true && fsm.currentTime >= 0.7f && fsm.currentTime <= 1.85f && GetComponent<Unit>().usingSkill == false)
        {
            GameObject target = GetComponent<Heal_fsm>().target;
            Vector3 playerPos = GetComponent<Transform>().position;

            if (unit.die == false && arrived == true && fsm.fight == true && target != null && GetComponent<Unit>().usingSkill == false)
            {
                var position = new Vector2Int((int)target.GetComponent<Transform>().position.x, (int)target.GetComponent<Transform>().position.y);
                targetPos = position;
                if (playerPos.x > targetPos.x && playerPos.y == targetPos.y)
                {
                    changeAnim = "crusader_sf/crusader_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "crusader_sf/crusader_sf_idle", true, 0);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x < targetPos.x && playerPos.y == targetPos.y)
                {
                    changeAnim = "crusader_sf/crusader_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "crusader_sf/crusader_sf_idle", true, 0);
                    }
                    if (xScale > 0) { ReverseScale(); }

                }
                else if (playerPos.x == targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "crusader_sf/crusader_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "crusader_sf/crusader_sf_idle", true, 0);
                    }
                }
                else if (playerPos.x == targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "crusader_sb/crusader_sb_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("crusader_sb");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "crusader_sb/crusader_sb_idle", true, 0);
                    }
                }
                else if (playerPos.x < targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "crusader_sf/crusader_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "crusader_sf/crusader_sf_idle", true, 0);
                    }
                    if (xScale > 0) { ReverseScale(); }
                }
                else if (playerPos.x > targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "crusader_sf/crusader_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "crusader_sf/crusader_sf_idle", true, 0);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x > targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "crusader_sf/crusader_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "crusader_sf/crusader_sf_idle", true, 0);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x < targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "crusader_sf/crusader_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "crusader_sf/crusader_sf_idle", true, 0);
                    }
                    if (xScale > 0) { ReverseScale(); }
                }
            }
        }
        if (unit.newChar == true)
        {
            changeAnim = "crusader_f/crusader_f_create";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.Skeleton.SetSkin("crusader_f");
                skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
            }
        }
        if (GetComponent<Unit>().usingSkill == true && unit.newChar == false && unit.die == false && arrived == true && fsm.fight == true)
        {
            changeAnim = "crusader_sf/crusader_sf_skill";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.Skeleton.SetSkin("crusader_sf");
                skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                skeletonAnimation.AnimationState.AddAnimation(0, "crusader_sf/crusader_sf_idle", true, 0);
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

    public void ReverseScale()
    {
        float y = transform.localScale.y;
        float z = transform.localScale.z;
        transform.localScale = new Vector3(-xScale, y, z);
    }
}