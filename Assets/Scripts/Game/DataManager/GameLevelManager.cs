using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager
{
    static private GameLevelManager instance=new GameLevelManager();
    static public GameLevelManager Instance => instance;
    private Transform heroPos;
    public PlayerObject player;
    public List<ZombiePoint> pointList=new List<ZombiePoint>();
    private int maxWave;
    private int nowWave;
    public List<ZombieObject> zombies=new List<ZombieObject>(); 
    private int num=5;
    private GameLevelManager() 
    {
        
    }

    public void InitInfo()
    {
        heroPos = GameObject.Find("HeroPos").transform;
        GameObject hero = GameObject.Instantiate(Resources.Load<GameObject>(GameDataManager.Instance.nowRole.res));
        hero.transform.position = heroPos.position;
        hero.transform.rotation=Quaternion.identity;
        player = hero.AddComponent<PlayerObject>();
        player.Init(int.Parse(GameDataManager.Instance.nowRole.atk),GameDataManager.Instance.nowScene.money);
        player.transform.GetComponent<CharacterController>().enabled = true;
        Camera.main.gameObject.GetComponent<CameraMove>().target =hero.transform;
        MainTowerObject.Instance.UpdateHp(GameDataManager.Instance.nowScene.towerHp, GameDataManager.Instance.nowScene.towerHp);
        GameObject[] points = GameObject.FindGameObjectsWithTag("ZombiePoint");
        foreach (GameObject obj in points)
            pointList.Add(obj.GetComponent<ZombiePoint>());
        UpdateWave(num);
    }

    public void UpdateWave(int maxWave)
    {
        this.maxWave = maxWave;
        nowWave = maxWave;
        UIManager.Instance.GetPanel<GamePanel>().UpdateRamain(maxWave,maxWave);
        for (int i = 0; i < pointList.Count; i++)
        {
            pointList[i].Init(maxWave);
        }
    }

    public bool Delay()
    {
        if (nowWave <= 0)
            return true;
        for (int i = 0; i < pointList.Count; i++)
        {
            if (pointList[i].state != State.Delay)
                return true;
        }
        nowWave--;
        UIManager.Instance.GetPanel<GamePanel>().UpdateRamain(nowWave, maxWave);
        return false;
    }

    public bool CheckOver()
    {
        for (int i = 0; i < pointList.Count; i++)
        {
            if (!pointList[i].CheckOver())
                return false;
        }
        if (zombies.Count != 0)
            return false;
        GameDataManager.Instance.playerData.Money += player.money;
        GameDataManager.Instance.SavePlayerData();
        return true;
    }

    public void ClearInfo()
    {
        pointList.Clear();
        maxWave = 0;
        nowWave = 0;
        zombies.Clear();
    }

    public void DestroyZombie(ZombieObject obj)
    {
        zombies.Remove(obj);
    }
}
