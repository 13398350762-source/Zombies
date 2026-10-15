using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    private float Hpwidth;
    public Image ImgHP;
    public Text txtHp;
    public Text txtRemain;
    public Text txtMomey;
    public Button btnExit;
    public Transform BotTrans;
    public List<TowerButton> btnTowers;
    private bool isDoing=false;
    private TowerPoint NowSelTowerPoint;
    private bool input;

    protected override void Init()
    {
        Hpwidth = ImgHP.GetComponent<RectTransform>().rect.width;

        btnExit.onClick.AddListener(() =>{
            UIManager.Instance.HidePanel<GamePanel>();
            SceneManager.LoadScene("BeginScene");
        });
        BotTrans.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void UpdateTowerHp(int hp,int maxHp)
    {
        txtHp.text = hp + "/"+maxHp;
        ImgHP.rectTransform.sizeDelta = new Vector2((float)hp / (float)maxHp * Hpwidth, ImgHP.rectTransform.sizeDelta.y);
    }

    public void UpdateRamain(int remain,int max)
    {
        txtRemain.text = remain + "/"+max;
    }

    private void Update()
    {
        if (GameLevelManager.Instance.CheckOver())
        {
            int money = int.Parse(txtMomey.text);
            UIManager.Instance.ShowPanel<GameOverPanel>().GameSuccess(money);
            GameDataManager.Instance.playerData.Money += money;
            GameDataManager.Instance.SavePlayerData();
        }

        base.Update();
        if (!isDoing)
        {
            if (!GameLevelManager.Instance.Delay())
            {
                Invoke("ChangeState", 1f);
                isDoing = true;
            }
        }

        if (!input)
            return;
        if (!NowSelTowerPoint.isBuild&&Input.GetKeyDown(KeyCode.Alpha1))
        {
            NowSelTowerPoint.CreateTower(GameDataManager.Instance.towerList[NowSelTowerPoint.chooseIDs[0]-1]);
        }
        else if (!NowSelTowerPoint.isBuild && Input.GetKeyDown(KeyCode.Alpha2))
        {
            NowSelTowerPoint.CreateTower(GameDataManager.Instance.towerList[NowSelTowerPoint.chooseIDs[1]-1]);
        }
        else if (!NowSelTowerPoint.isBuild && Input.GetKeyDown(KeyCode.Alpha3))
        {
            NowSelTowerPoint.CreateTower(GameDataManager.Instance.towerList[NowSelTowerPoint.chooseIDs[2]-1]);
        }
        else if (NowSelTowerPoint.isBuild && Input.GetKeyDown(KeyCode.Q)&&NowSelTowerPoint.nowTower.nextLev!=0)
        {
            NowSelTowerPoint.CreateTower(GameDataManager.Instance.towerList[NowSelTowerPoint.nowTower.nextLev-1]);
        }
    }

    public void ChangeState()
    {
        for (int i = 0; i < GameLevelManager.Instance.pointList.Count; i++)
        {
            GameLevelManager.Instance.pointList[i].state = State.Wave;
            GameLevelManager.Instance.pointList[i].nowNum = GameLevelManager.Instance.pointList[i].nowWaveZombieNum;
            GameLevelManager.Instance.pointList[i].nowId = Random.Range(0, GameLevelManager.Instance.pointList[i].ids.Count - 1);
            GameLevelManager.Instance.pointList[i].NowWave--;
        }
        isDoing = false;
    }

    public void OnSelUpdateTower(TowerPoint point)
    {
        NowSelTowerPoint = point;
        if (point != null)
        {
            input = true;
            BotTrans.gameObject.SetActive(true);
            if (!point.isBuild)
            {
                for (int i = 0; i < NowSelTowerPoint.chooseIDs.Count; i++)
                {
                    btnTowers[i].gameObject.SetActive(true);
                    btnTowers[i].Init(GameDataManager.Instance.towerList[NowSelTowerPoint.chooseIDs[i] - 1], "数字键" + (i + 1));
                }
            }
            else if(point.nowTower.nextLev != 0)
            {
                btnTowers[0].gameObject.SetActive(false);
                btnTowers[1].Init(GameDataManager.Instance.towerList[NowSelTowerPoint.nowTower.nextLev-1], "Q键");
                btnTowers[2].gameObject.SetActive(false);
            }
        }
        else
        {
            input = false;
            BotTrans.gameObject.SetActive(false);
        }
    }

    public void AddMoney(int num)
    {
        int money = int.Parse(txtMomey.text);
        money += num;
        txtMomey.text = money.ToString();
    }
}
