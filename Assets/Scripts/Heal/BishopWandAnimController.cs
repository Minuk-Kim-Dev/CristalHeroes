using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class BishopWandAnimController : MonoBehaviour
{
    SkeletonAnimation skeletonAnimation;
    float parentXScale;
    float xScale;
    string parentAnim;
    public string currentAnim;
    string changeAnim;

    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    private void Update()
    {
        parentXScale = GetComponentInParent<Transform>().localScale.x;
        xScale = GetComponent<Transform>().localScale.x;
        currentAnim = skeletonAnimation.AnimationName;
        parentAnim = GetComponentInParent<BishopAnimController>().currentAnim;
        if (parentAnim == "bishop_sf/bishop_sf_run" || parentAnim == "bishop_sb/bishop_sb_run"
            || parentAnim == "bishop_f/bishop_f_run" || parentAnim == "bishop_b/bishop_b_run")
        {
            changeAnim = "Bishop_weapon_run";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
            }
            if ((parentXScale > 0 && xScale < 0) || (parentXScale < 0 && xScale > 0)) { ReverseScale(); }
        }
        else if (parentAnim == "bishop_sf/bishop_sf_idle" || parentAnim == "bishop_sb/bishop_sb_idle"
            || parentAnim == "bishop_f/bishop_f_idle" || parentAnim == "bishop_b/bishop_b_idle")
        {
            changeAnim = "Bishop_weapon_idle";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
            }
            if ((parentXScale > 0 && xScale < 0) || (parentXScale < 0 && xScale > 0)) { ReverseScale(); }
        }
        else if (parentAnim == "bishop_sf/bishop_sf_attack" || parentAnim == "bishop_sb/bishop_sb_attack"
            || parentAnim == "bishop_f/bishop_f_attack" || parentAnim == "bishop_b/bishop_b_attack")
        {
            changeAnim = "Bishop_weapon_atk";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                skeletonAnimation.AnimationState.AddAnimation(0, "Bishop_weapon_idle", true, 0);
            }
            if ((parentXScale > 0 && xScale < 0) || (parentXScale < 0 && xScale > 0)) { ReverseScale(); }
        }
        else if (parentAnim == "bishop_sf/bishop_sf_die")
        {
            changeAnim = "Bishop_weapon_die";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
            }
        }
        else if (parentAnim == "bishop_f / bishop_f_create")
        {
            changeAnim = "Bishop_weapon_idle";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
            }
        }
        else if (parentAnim == "bishop_sf / bishop_sf_skill")
        {
            changeAnim = "Bishop_weapon_skill";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
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