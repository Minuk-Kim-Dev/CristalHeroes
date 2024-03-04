using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class CrusaderWandAnimController : MonoBehaviour
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
        parentAnim = GetComponentInParent<CrusaderAnimController>().currentAnim;
        if (parentAnim == "crusader_sf/crusader_sf_run" || parentAnim == "crusader_sb/crusader_sb_run"
            || parentAnim == "crusader_f/crusader_f_run" || parentAnim == "crusader_b/crusader_b_run")
        {
            changeAnim = "Healer_weapon_run";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
            }
            if ((parentXScale > 0 && xScale < 0) || (parentXScale < 0 && xScale > 0)) { ReverseScale(); }
        }
        else if (parentAnim == "crusader_sf/crusader_sf_idle" || parentAnim == "crusader_sb/crusader_sb_idle"
            || parentAnim == "crusader_f/crusader_f_idle" || parentAnim == "crusader_b/crusader_b_idle")
        {
            changeAnim = "Healer_weapon_idle";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
            }
            if ((parentXScale > 0 && xScale < 0) || (parentXScale < 0 && xScale > 0)) { ReverseScale(); }
        }
        else if (parentAnim == "crusader_sf/crusader_sf_attack" || parentAnim == "crusader_sb/crusader_sb_attack"
            || parentAnim == "crusader_f/crusader_f_attack" || parentAnim == "crusader_b/crusader_b_attack")
        {
            changeAnim = "Healer_weapon_atk";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                skeletonAnimation.AnimationState.AddAnimation(0, "Healer_weapon_idle", true, 0);
            }
            if ((parentXScale > 0 && xScale < 0) || (parentXScale < 0 && xScale > 0)) { ReverseScale(); }
        }
        else if (parentAnim == "crusader_sf/crusader_sf_die")
        {
            changeAnim = "Healer_weapon_die";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
            }
        }
        else if (parentAnim == "crusader_f / crusader_f_create")
        {
            changeAnim = "Healer_weapon_create";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
            }
        }
        else if (parentAnim == "crusader_sf / crusader_sf_skill")
        {
            changeAnim = "Healer_weapon_skill";
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