using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    FirstDelay,
    Delay,
    Wave
};

public class ZombiePoint : MonoBehaviour
{

    public int MaxWave;
    public int nowWaveZombieNum;
    public List<int> ids;
    //第一波间隔
    public float firstdelayTime;
    //怪物创建间隔
    public float offset;
    public int NowWave;
    public int nowId;
    private float preTime;
    public State state;
    public int nowNum;
    private void CreateZombie()
    {
        ZombieInfo info = GameDataManager.Instance.zombieList[ids[nowId]];
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.res),transform.position,Quaternion.identity);
        ZombieObject zo=obj.AddComponent<ZombieObject>();
        zo.Init(info);
        GameLevelManager.Instance.zombies.Add(zo);
        nowNum--;
    }

    public void Init(int num)
    {
        MaxWave = num;
        NowWave = MaxWave;
        state = State.FirstDelay;
    }

    void Update()
    {
        if (NowWave < 0)
        {
            nowNum = 0;
            return;
        }
        switch (state)
        {
            case State.FirstDelay:
                if (Time.time - preTime >= firstdelayTime)
                {
                    preTime = Time.time;
                    nowNum = nowWaveZombieNum;
                    state = State.Wave;
                    nowId = Random.Range(0, ids.Count - 1);
                    NowWave--;
                }
                break;
            case State.Delay:
                    preTime = Time.time;
                break;
            case State.Wave:
                if (Time.time - preTime >= offset)
                {
                    CreateZombie();
                    preTime = Time.time;
                    if(nowNum == 0)
                    state = State.Delay;
                }
                break;
        }
    }

    public bool CheckOver()
    {
        if (NowWave <= 0 && nowNum == 0)
            return true;
        return false;
    }

}
