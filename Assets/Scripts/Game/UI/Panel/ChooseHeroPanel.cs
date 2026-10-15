using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChooseHeroPanel : BasePanel
{
    public Button btnSure;
    public Button btnReturn;
    public Button btnLeft;
    public Button btnRight;
    public Button btnUnlock;
    public Text txtLockMoney;
    public Text txtMoney;
    public Text txtName;
    private Transform pos;
    private RoleInfo info;
    private int id=1;

    protected override void Init()
    {
        pos = GameObject.Find("heroPos").transform;
        txtMoney.text = GameDataManager.Instance.playerData.Money.ToString();

        btnSure.onClick.AddListener(() =>{
            if (GameDataManager.Instance.playerData.availRole.Contains(id))
            {
                GameDataManager.Instance.nowRole = info;
                UIManager.Instance.HidePanel<ChooseHeroPanel>();
                UIManager.Instance.ShowPanel<ChooseScenePanel>();
                //进入场景选择界面
            }
            else
            {
                UIManager.Instance.ShowPanel<TipPanel>().ChangeContent("该角色未解锁");
                //显示提示面板
            }
        });

        btnReturn.onClick.AddListener(() =>{
            UIManager.Instance.HidePanel<ChooseHeroPanel>();
            Camera.main.GetComponent<CameraAnimator>().TurnRight(()=> { 
                UIManager.Instance.ShowPanel<BeginPanel>(); 
            });
        });

        btnLeft.onClick.AddListener(() =>{
            Destroy(pos.GetChild(0).gameObject);
            id -= 1;
            if (id <= 0)
                id = GameDataManager.Instance.roleList.Count;
            ChangeHero();
        });

        btnRight.onClick.AddListener(() =>{
            Destroy(pos.GetChild(0).gameObject);
            id += 1;
            if (id > GameDataManager.Instance.roleList.Count)
                id = 1;
            ChangeHero();
        });

        btnUnlock.onClick.AddListener(() =>{
            int num = int.Parse(txtMoney.text) - int.Parse(info.lockMoney);
            if (num >= 0)
            {
                txtMoney.text = num.ToString();
                btnUnlock.gameObject.SetActive(false);
                GameDataManager.Instance.playerData.availRole.Add(id);
                GameDataManager.Instance.SavePlayerData();
                UIManager.Instance.ShowPanel<TipPanel>().ChangeContent("解锁成功");
                //提示面板显示购买成功
            }
            else
            {
                UIManager.Instance.ShowPanel<TipPanel>().ChangeContent("金币不足");
                //显示提示面板
            }
        });
        ChangeHero();
    }

    public void ChangeHero()
    {
        info=GameDataManager.Instance.roleList[id-1];
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.res));
        obj.transform.SetParent(pos, false);
        txtName.text = info.tips;
        if (GameDataManager.Instance.playerData.availRole.Contains(id))
        {
            btnUnlock.gameObject.SetActive(false);
        }
        else
        {
            btnUnlock.gameObject.SetActive(true);
            txtLockMoney.text= "$："+GameDataManager.Instance.roleList[id - 1].lockMoney;
        }
    }

    public override void HideMe(UnityAction callback)
    {
        base.HideMe(callback);
        if (pos.GetChild(0)!=null)
            Destroy(pos.GetChild(0).gameObject);
    }
}
