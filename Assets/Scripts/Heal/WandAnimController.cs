using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class WandAnimController : MonoBehaviour
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
        parentAnim = GetComponentInParent<HealAnimController>().currentAnim;
        if(parentAnim == "healer_sf/healer_sf_run" || parentAnim == "healer_sb/healer_sb_run" 
            || parentAnim == "healer_f/healer_f_run" || parentAnim == "healer_b/healer_b_run")
        {
            changeAnim = "Healer_weapon_run";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
            }
            if ((parentXScale > 0 && xScale < 0) || (parentXScale < 0 && xScale > 0)) { ReverseScale(); }
        }
        else if (parentAnim == "healer_sf/healer_sf_idle" || parentAnim == "healer_sb/healer_sb_idle"
            || parentAnim == "healer_f/healer_f_idle" || parentAnim == "healer_b/healer_b_idle")
        {
            changeAnim = "Healer_weapon_idle";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
            }
            if ((parentXScale > 0 && xScale < 0) || (parentXScale < 0 && xScale > 0)) { ReverseScale(); }
        }
        else if (parentAnim == "healer_sf/healer_sf_attack" || parentAnim == "healer_sb/healer_sb_attack"
            || parentAnim == "healer_f/healer_f_attack" || parentAnim == "healer_b/healer_b_attack")
        {
            changeAnim = "Healer_weapon_atk";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                skeletonAnimation.AnimationState.AddAnimation(0, "Healer_weapon_idle", true, 0);
            }
            if ((parentXScale > 0 && xScale < 0) || (parentXScale < 0 && xScale > 0)) { ReverseScale(); }
        }
        else if (parentAnim == "healer_sf/healer_sf_die")
        {
            changeAnim = "Healer_weapon_die";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
            }
        }
        else if (parentAnim == "healer_f / healer_f_create")
        {
            changeAnim = "Healer_weapon_create";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
            }
        }
        else if (parentAnim == "healer_sf / healer_sf_skill")
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