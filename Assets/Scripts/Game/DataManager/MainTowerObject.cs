using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerObject : MonoBehaviour
{
    int hp;
    int MaxHp;
    bool isDead;
    static private MainTowerObject instance;
    static public MainTowerObject Instance => instance;
    private void Awake()
    {
        instance = this;
    }
    public void UpdateHp(int hp, int MaxHP)
    {
        this.hp= hp;
        this.MaxHp= MaxHP;
        UIManager.Instance.GetPanel<GamePanel>().UpdateTowerHp(hp, MaxHP);
    }
    public void Wound(int damage)
    {
        if (isDead)
            return;
        hp-=damage;
        UpdateHp(hp, MaxHp);
        if (hp <= 0)
        {
            isDead = true;
            int money = (int)(int.Parse(UIManager.Instance.GetPanel<GamePanel>().txtMomey.text)*0.5f);
            UIManager.Instance.ShowPanel<GameOverPanel>().GameDefeat(money);
            GameDataManager.Instance.playerData.Money += money;
            GameDataManager.Instance.SavePlayerData();
        }
    }
    private void OnDestroy()
    {
        instance = null;
    }

}
