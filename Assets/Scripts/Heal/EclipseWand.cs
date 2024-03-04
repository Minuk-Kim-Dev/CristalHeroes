using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class EclipseWand : MonoBehaviour
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
        parentAnim = GetComponentInParent<EclipseAnimController>().currentAnim;
        if (parentAnim == "eclipse_sf/eclipse_sf_run" || parentAnim == "eclipse_sb/eclipse_sb_run"
            || parentAnim == "eclipse_f/eclipse_f_run" || parentAnim == "eclipse_b/eclipse_b_run")
        {
            changeAnim = "eclipse_weapon_run";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
            }
            if ((parentXScale > 0 && xScale < 0) || (parentXScale < 0 && xScale > 0)) { ReverseScale(); }
        }
        else if (parentAnim == "eclipse_sf/eclipse_sf_idle" || parentAnim == "eclipse_sb/eclipse_sb_idle"
            || parentAnim == "eclipse_f/eclipse_f_idle" || parentAnim == "eclipse_b/eclipse_b_idle")
        {
            changeAnim = "eclipse_weapon_idle";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, true);
            }
            if ((parentXScale > 0 && xScale < 0) || (parentXScale < 0 && xScale > 0)) { ReverseScale(); }
        }
        else if (parentAnim == "eclipse_sf/eclipse_sf_attack" || parentAnim == "eclipse_sb/eclipse_sb_attack"
            || parentAnim == "eclipse_f/eclipse_f_attack" || parentAnim == "eclipse_b/eclipse_b_attack")
        {
            changeAnim = "eclipse_weapon_atk";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
                skeletonAnimation.AnimationState.AddAnimation(0, "eclipse_weapon_idle", true, 0);
            }
            if ((parentXScale > 0 && xScale < 0) || (parentXScale < 0 && xScale > 0)) { ReverseScale(); }
        }
        else if (parentAnim == "eclipse_sf/eclipse_sf_die")
        {
            changeAnim = "eclipse_weapon_die";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
            }
        }
        else if (GetComponentInParent<Unit>().newChar == true)
        {
            changeAnim = "eclipse_weapon_create";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, changeAnim, false);
            }
        }
        else if (parentAnim == "eclipse_sf / eclipse_sf_skill")
        {
            changeAnim = "eclipse_weapon_skill";
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