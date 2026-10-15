using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseScenePanel : BasePanel
{
    public Button btnLeft;
    public Button btnRight;
    public Button btnStart;
    public Button btnReturn;
    public Image imgScene;
    public Text txtName;
    public Text txtDes;
    private int nowScene=1;
    private SceneInfo nowSceneInfo;
    protected override void Init()
    {
        btnLeft.onClick.AddListener(() =>{
            nowScene--;
            if (nowScene <= 0)
                nowScene = GameDataManager.Instance.sceneList.Count;
            ChangeScene();
        });

        btnRight.onClick.AddListener(() =>{
            nowScene++;
            if (nowScene > GameDataManager.Instance.sceneList.Count)
                nowScene = 1;
            ChangeScene();
        });

        btnStart.onClick.AddListener(() =>{
            GameDataManager.Instance.nowScene = nowSceneInfo;
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            GameDataManager.Instance.nowScene = nowSceneInfo;
            AsyncOperation ao=SceneManager.LoadSceneAsync("GameScene");
            ao.completed += (operation) =>{
                UIManager.Instance.ShowPanel<GamePanel>();
                GameLevelManager.Instance.InitInfo();
            };
        });

        btnReturn.onClick.AddListener(() =>{
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            UIManager.Instance.ShowPanel<ChooseHeroPanel>();
        });
        ChangeScene();
    }

    public void ChangeScene()
    {
        nowSceneInfo=GameDataManager.Instance.sceneList[nowScene-1];
        txtName.text = nowSceneInfo.name;
        txtDes.text = nowSceneInfo.tips;
        imgScene.sprite = Resources.Load<Sprite>(nowSceneInfo.imgRes);
    }
}
