using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseButton : MonoBehaviour
{
    public GameObject settingUi;
    public GameObject paurseUi;
    public StopGame stopgame;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DiereStart()
    {
        //다시하기
        SceneManager.LoadScene("SampleScene");
    }
    public void reStart()
    {
        //다시하기
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }
    public void back()
    {
        stopgame.startScene();
    }

    public void title()
    {
        //메인화면
        SceneManager.LoadScene("Main");
    }

    public void settingback()
    {
        paurseUi.SetActive(true);
        settingUi.SetActive(false);
    }

    public void setting()
    {
        paurseUi.SetActive(false);
        settingUi.SetActive(true);
    }
    public void gamequit()
    {
        //게임종료
        Application.Quit();
    }
}
