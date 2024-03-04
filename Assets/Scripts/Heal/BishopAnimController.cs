using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class BishopAnimController : MonoBehaviour
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
            fsm.fight = false;
            if (startPos.x > targetPos.x && startPos.y == targetPos.y)
            {
                changeAnim = "bishop_sf/bishop_sf_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x < targetPos.x && startPos.y == targetPos.y)
            {
                changeAnim = "bishop_sf/bishop_sf_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
                if (xScale > 0) { ReverseScale(); }
            }
            else if (startPos.x == targetPos.x && startPos.y > targetPos.y)
            {
                changeAnim = "bishop_f/bishop_f_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("bishop_f");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x == targetPos.x && startPos.y < targetPos.y)
            {
                changeAnim = "bishop_b/bishop_b_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("bishop_b");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x < targetPos.x && startPos.y < targetPos.y)
            {
                changeAnim = "bishop_sb/bishop_sb_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("bishop_sb");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
                if (xScale > 0) { ReverseScale(); }
            }
            else if (startPos.x > targetPos.x && startPos.y < targetPos.y)
            {
                changeAnim = "bishop_sb/bishop_sb_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("bishop_sb");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x > targetPos.x && startPos.y > targetPos.y)
            {
                changeAnim = "bishop_sf/bishop_sf_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x < targetPos.x && startPos.y > targetPos.y)
            {
                changeAnim = "bishop_sf/bishop_sf_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
                if (xScale > 0) { ReverseScale(); }
            }
        }

        if (unit.newChar == false && arrived == true && fsm.fight == false && GetComponent<Unit>().usingSkill == false)
        {
            if (currentSkin == "bishop_f")
            {
                changeAnim = "bishop_f/bishop_f_idle";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
            }
            else if (currentSkin == "bishop_sf")
            {
                changeAnim = "bishop_sf/bishop_sf_idle";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
            }
            else if (currentSkin == "bishop_b")
            {
                changeAnim = "bishop_b/bishop_b_idle";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
            }
            else if (currentSkin == "bishop_sb")
            {
                changeAnim = "bishop_sb/bishop_sb_idle";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                }
            }
        }

        if (unit.newChar == false && unit.die == true)
        {
            changeAnim = "bishop_sf/bishop_sf_die";
            gameObject.GetComponent<Heal_Unitmovement>().enabled = false;
            gameObject.GetComponent<Heal_fsm>().enabled = false;
            gameObject.GetComponent<Heal_PlayerTarget>().enabled = false;
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.Skeleton.SetSkin("bishop_sf");
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
            if ((fsm.currentTime < 0.7f || fsm.currentTime > 1.5f) && fsm.currentTime < fsm.DelayTime)
            {
                GameObject target = GetComponent<Heal_fsm>().target;
                Vector3 playerPos = GetComponent<Transform>().position;
                if (unit.die == false && arrived == true && fsm.fight == true && target != null)
                {
                    var position = new Vector2Int((int)target.GetComponent<Transform>().position.x, (int)target.GetComponent<Transform>().position.y);
                    targetPos = position;
                    if (playerPos.x > targetPos.x && playerPos.y == targetPos.y)
                    {
                        changeAnim = "bishop_sf/bishop_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                        if (xScale < 0) { ReverseScale(); }
                    }
                    else if (playerPos.x < targetPos.x && playerPos.y == targetPos.y)
                    {
                        changeAnim = "bishop_sf/bishop_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                        if (xScale > 0) { ReverseScale(); }

                    }
                    else if (playerPos.x == targetPos.x && playerPos.y > targetPos.y)
                    {
                        changeAnim = "bishop_sf/bishop_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                    }
                    else if (playerPos.x == targetPos.x && playerPos.y < targetPos.y)
                    {
                        changeAnim = "bishop_sb/bishop_sb_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("bishop_sb");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                    }
                    else if (playerPos.x < targetPos.x && playerPos.y < targetPos.y)
                    {
                        changeAnim = "bishop_sf/bishop_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                        if (xScale > 0) { ReverseScale(); }
                    }
                    else if (playerPos.x > targetPos.x && playerPos.y < targetPos.y)
                    {
                        changeAnim = "bishop_sf/bishop_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                        if (xScale < 0) { ReverseScale(); }
                    }
                    else if (playerPos.x > targetPos.x && playerPos.y > targetPos.y)
                    {
                        changeAnim = "bishop_sf/bishop_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                        if (xScale < 0) { ReverseScale(); }
                    }
                    else if (playerPos.x < targetPos.x && playerPos.y > targetPos.y)
                    {
                        changeAnim = "bishop_sf/bishop_sf_idle";
                        if (SameAnimation(currentAnim, changeAnim))
                        {
                            skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                            skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
                        }
                        if (xScale > 0) { ReverseScale(); }
                    }
                }
            }
        }
        if (unit.newChar == false && fsm.fight == true && fsm.currentTime >= 0.7f && fsm.currentTime <= 1.5f && GetComponent<Unit>().usingSkill == false)
        {
            GameObject target = GetComponent<Heal_fsm>().target;
            Vector3 playerPos = GetComponent<Transform>().position;

            if (unit.die == false && arrived == true && fsm.fight == true && target != null && GetComponent<Unit>().usingSkill == false)
            {
                var position = new Vector2Int((int)target.GetComponent<Transform>().position.x, (int)target.GetComponent<Transform>().position.y);
                targetPos = position;
                if (playerPos.x > targetPos.x && playerPos.y == targetPos.y)
                {
                    changeAnim = "bishop_sf/bishop_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "bishop_sf/bishop_sf_idle", true, 0);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x < targetPos.x && playerPos.y == targetPos.y)
                {
                    changeAnim = "bishop_sf/bishop_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "bishop_sf/bishop_sf_idle", true, 0);
                    }
                    if (xScale > 0) { ReverseScale(); }

                }
                else if (playerPos.x == targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "bishop_sf/bishop_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "bishop_sf/bishop_sf_idle", true, 0);
                    }
                }
                else if (playerPos.x == targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "bishop_sb/bishop_sb_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("bishop_sb");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "bishop_sb/bishop_sb_idle", true, 0);
                    }
                }
                else if (playerPos.x < targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "bishop_sf/bishop_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "bishop_sf/bishop_sf_idle", true, 0);
                    }
                    if (xScale > 0) { ReverseScale(); }
                }
                else if (playerPos.x > targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "bishop_sf/bishop_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "bishop_sf/bishop_sf_idle", true, 0);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x > targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "bishop_sf/bishop_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "bishop_sf/bishop_sf_idle", true, 0);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x < targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "bishop_sf/bishop_sf_attack";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "bishop_sf/bishop_sf_idle", true, 0);
                    }
                    if (xScale > 0) { ReverseScale(); }
                }
            }
        }
        if (unit.newChar == true)
        {
            changeAnim = "bishop_f/bishop_f_create";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.Skeleton.SetSkin("bishop_f");
                skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
            }
        }
        if (GetComponent<Unit>().usingSkill == true && unit.newChar == false && unit.die == false && arrived == true)
        {
            changeAnim = "bishop_sf/bishop_sf_skill";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.Skeleton.SetSkin("bishop_sf");
                skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                skeletonAnimation.AnimationState.AddAnimation(0, "bishop_sf/bishop_sf_idle", true, 0);
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