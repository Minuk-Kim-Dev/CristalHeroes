using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public float time = 0;
    public float alpha = 1;

    private void Update()
    {
        if (time < 1.5f)
        {
            GetComponent<Image>().color = new Color(1, 1, 1, alpha);
            GetComponentInChildren<Text>().color = new Color(1, 1, 1, alpha);
        }
        else
        {
            time = 0;
            alpha = 1;
            this.gameObject.SetActive(false);
        }
        time += Time.deltaTime;
        alpha = 1 - (time / 1.5f);
    }

    public void resetAnim()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
        GetComponentInChildren<Text>().color = new Color(1, 1, 1, 1);
        this.gameObject.SetActive(true);
        time = 0;
        alpha = 1;
    }
}
