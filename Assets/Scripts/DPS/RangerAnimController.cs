using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class RangerAnimController : MonoBehaviour
{
    SkeletonAnimation skeletonAnimation;
    bool arrived;
    Vector2Int startPos, targetPos;
    float xScale;
    DPS_fsm fsm;
    public string currentAnim;
    string changeAnim;
    Unit unit;
    public string currentSkin;

    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        fsm = GetComponent<DPS_fsm>();
        unit = GetComponent<Unit>();
    }

    void Update()
    {
        xScale = transform.localScale.x;
        arrived = GetComponent<UnitMovement>().arrived;
        startPos = GetComponent<UnitMovement>().startPos;
        targetPos = GetComponent<UnitMovement>().targetPos;
        currentAnim = skeletonAnimation.AnimationName;
        currentSkin = skeletonAnimation.Skeleton.Skin.ToString();

        if (unit.newChar == false && arrived == false && unit.die == false)
        {
            fsm.fight = false;
            if (startPos.x > targetPos.x && startPos.y == targetPos.y)
            {
                changeAnim = "ranger_sf/ranger_sf_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sf/ranger_sf_run", true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x < targetPos.x && startPos.y == targetPos.y)
            {
                changeAnim = "ranger_sf/ranger_sf_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sf/ranger_sf_run", true);
                }
                if (xScale > 0) { ReverseScale(); }
            }
            else if (startPos.x == targetPos.x && startPos.y > targetPos.y)
            {
                changeAnim = "ranger_f/ranger_f_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("ranger_f");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, "ranger_f/ranger_f_run", true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x == targetPos.x && startPos.y < targetPos.y)
            {
                changeAnim = "ranger_b/ranger_b_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("ranger_b");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, "ranger_b/ranger_b_run", true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x < targetPos.x && startPos.y < targetPos.y)
            {
                changeAnim = "ranger_sb/ranger_sb_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("ranger_sb");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sb/ranger_sb_run", true);
                }
                if (xScale > 0) { ReverseScale(); }
            }
            else if (startPos.x > targetPos.x && startPos.y < targetPos.y)
            {
                changeAnim = "ranger_sb/ranger_sb_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("ranger_sb");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sb/ranger_sb_run", true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x > targetPos.x && startPos.y > targetPos.y)
            {
                changeAnim = "ranger_sf/ranger_sf_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sf/ranger_sf_run", true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x < targetPos.x && startPos.y > targetPos.y)
            {
                changeAnim = "ranger_sf/ranger_sf_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sf/ranger_sf_run", true);
                }
                if (xScale > 0) { ReverseScale(); }
            }
        }

        if (unit.newChar == false && arrived == true && fsm.fight == false && unit.die == false)
        {
            if (currentSkin == "ranger_f")
            {
                changeAnim = "ranger_f/ranger_f_idle";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
            }
            else if (currentSkin == "ranger_sf")
            {
                changeAnim = "ranger_sf/ranger_sf_idle";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
            }
            else if (currentSkin == "ranger_b")
            {
                changeAnim = "ranger_b/ranger_b_idle";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
            }
            else if (currentSkin == "ranger_sb")
            {
                changeAnim = "ranger_sb/ranger_sb_idle";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
            }
        }

        if (unit.newChar == false && unit.die == true)
        {
            changeAnim = "ranger_sf/ranger_sf_die";
            gameObject.GetComponent<UnitMovement>().enabled = false;
            gameObject.GetComponent<DPS_fsm>().enabled = false;
            gameObject.GetComponent<PlayerTarget>().enabled = false;
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sf/ranger_sf_die", false);
            }
        }

        if (unit.newChar == false && fsm.fight == true && arrived == false)
        {
            targetPos = GetComponent<UnitMovement>().targetPos;
        }

        if (unit.newChar == false && fsm.fight == true)
        {
            if ((fsm.currentTime < 0.7f || fsm.currentTime > 1.5f) && fsm.currentTime < fsm.DelayTime)
            {
                GameObject target = GetComponent<DPS_fsm>().target;
                Vector3 playerPos = GetComponent<Transform>().position;
                if (unit.die == false && arrived == true && fsm.fight == true && target != null)
                {
                    var position = new Vector2Int((int)target.GetComponent<Transform>().position.x, (int)target.GetComponent<Transform>().position.y);
                    targetPos = position;
                    if (playerPos.x > targetPos.x && playerPos.y == targetPos.y)
                    {
                        changeAnim = "ranger_sf/ranger_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sf/ranger_sf_idle", true);
                        }
                        if (xScale < 0) { ReverseScale(); }
                    }
                    else if (playerPos.x < targetPos.x && playerPos.y == targetPos.y)
                    {
                        changeAnim = "ranger_sf/ranger_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sf/ranger_sf_idle", true);
                        }
                        if (xScale > 0) { ReverseScale(); }

                    }
                    else if (playerPos.x == targetPos.x && playerPos.y > targetPos.y)
                    {
                        changeAnim = "ranger_sf/ranger_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sf/ranger_sf_idle", true);
                        }
                    }
                    else if (playerPos.x == targetPos.x && playerPos.y < targetPos.y)
                    {
                        changeAnim = "ranger_sb/ranger_sb_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("ranger_sb");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sb/ranger_sb_idle", true);
                        }
                    }
                    else if (playerPos.x < targetPos.x && playerPos.y < targetPos.y)
                    {
                        changeAnim = "ranger_sf/ranger_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sf/ranger_sf_idle", true);
                        }
                        if (xScale > 0) { ReverseScale(); }
                    }
                    else if (playerPos.x > targetPos.x && playerPos.y < targetPos.y)
                    {
                        changeAnim = "ranger_sf/ranger_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sf/ranger_sf_idle", true);
                        }
                        if (xScale < 0) { ReverseScale(); }
                    }
                    else if (playerPos.x > targetPos.x && playerPos.y > targetPos.y)
                    {
                        changeAnim = "ranger_sf/ranger_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sf/ranger_sf_idle", true);
                        }
                        if (xScale < 0) { ReverseScale(); }
                    }
                    else if (playerPos.x < targetPos.x && playerPos.y > targetPos.y)
                    {
                        changeAnim = "ranger_sf/ranger_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sf/ranger_sf_idle", true);
                        }
                        if (xScale > 0) { ReverseScale(); }
                    }
                }
            }
        }
        if (unit.newChar == false && fsm.fight == true && fsm.currentTime >= 0.7f && fsm.currentTime <= 1.5f)
        {
            GameObject target = GetComponent<DPS_fsm>().target;
            Vector3 playerPos = GetComponent<Transform>().position;

            if (unit.die == false && arrived == true && fsm.fight == true && target != null)
            {
                var position = new Vector2Int((int)target.GetComponent<Transform>().position.x, (int)target.GetComponent<Transform>().position.y);
                targetPos = position;
                if (playerPos.x > targetPos.x && playerPos.y == targetPos.y)
                {
                    changeAnim = "ranger_sf/ranger_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sf/ranger_sf_attack", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "ranger_sf/ranger_sf_idle", true, 0);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x < targetPos.x && playerPos.y == targetPos.y)
                {
                    changeAnim = "ranger_sf/ranger_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sf/ranger_sf_attack", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "ranger_sf/ranger_sf_idle", true, 0);
                    }
                    if (xScale > 0) { ReverseScale(); }

                }
                else if (playerPos.x == targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "ranger_sf/ranger_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sf/ranger_sf_attack", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "ranger_f/ranger_f_idle", true, 0);
                    }
                }
                else if (playerPos.x == targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "ranger_sb/ranger_sb_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("ranger_sb");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sb/ranger_sb_attack", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "ranger_sb/ranger_sb_idle", true, 0);
                    }
                }
                else if (playerPos.x < targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "ranger_sf/ranger_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "ranger_sf/ranger_sf_idle", true, 0);
                    }
                    if (xScale > 0) { ReverseScale(); }
                }
                else if (playerPos.x > targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "ranger_sf/ranger_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "ranger_sf/ranger_sf_idle", true, 0);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x > targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "ranger_sf/ranger_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sf/ranger_sf_attack", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "ranger_sf/ranger_sf_idle", true, 0);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x < targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "ranger_sf/ranger_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("ranger_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "ranger_sf/ranger_sf_attack", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "ranger_sf/ranger_sf_idle", true, 0);
                    }
                    if (xScale > 0) { ReverseScale(); }
                }
            }
        }
        if (unit.newChar == true)
        {
            changeAnim = "ranger_f/ranger_f_create";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.Skeleton.SetSkin("ranger_f");
                skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                skeletonAnimation.AnimationState.SetAnimation(0, "ranger_f/ranger_f_create", false);
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