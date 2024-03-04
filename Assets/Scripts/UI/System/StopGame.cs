using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopGame : MonoBehaviour
{
    bool isPause = false;
    public GameObject panel;
    public Canvas overlay;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
            {
                stopScene();
            }
            else
            {
                startScene();
            }
        }
    }
    void stopScene()
    {
        Time.timeScale = 0;
        overlay.enabled = false;
        isPause = true;
        panel.SetActive(true);
        return;
    }

    public void startScene()
    {
        Time.timeScale = 1;
        overlay.enabled = true;
        isPause = false;
        panel.SetActive(false);
        return;
    }
}
