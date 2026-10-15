using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    public Button btnSure;
    public Text txtContent;
    public Text txtResult;
    public Text txtGain;
    protected override void Init()
    {
        btnSure.onClick.AddListener(() =>{
            UIManager.Instance.HidePanel<GameOverPanel>();
            UIManager.Instance.HidePanel<GamePanel>();
            SceneManager.LoadScene("BeginScene");
            Time.timeScale = 1f;
            GameLevelManager.Instance.ClearInfo();
        });
    }

    public void GameSuccess(int money)
    {
        txtContent.text = "获得成功奖励";
        txtResult.text = "胜利";
        txtGain.text= "$"+money.ToString();
    }

    public void GameDefeat(int money)
    {
        txtContent.text = "获得失败奖励";
        txtResult.text = "失败";
        txtGain.text = "$" + money.ToString();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }
}
