using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerObject : MonoBehaviour
{
    public TowerInfo info;
    public Transform head;
    public Transform firePoint;
    private ZombieObject targetZombie;
    private Vector3 targetPos;
    public float nowTime;
    public float roundSpeed = 20;

    public void InitInfo(TowerInfo info)
    {
        this.info = info;
        nowTime = Time.time;
    }

    void Update()
    {
        if (info.atkType == 1)
        {
            if (targetZombie == null || targetZombie.isDead || Vector3.Distance(targetZombie.transform.position,transform.position)>info.atkRange)
            {
                targetZombie = null;
                for (int i = 0; i < GameLevelManager.Instance.zombies.Count; i++)
                {
                    if (!GameLevelManager.Instance.zombies[i].isDead && Vector3.Distance(GameLevelManager.Instance.zombies[i].transform.position, transform.position) <= info.atkRange)
                        targetZombie = GameLevelManager.Instance.zombies[i];
                }
            }
            if (targetZombie == null)
                return;
            targetPos = targetZombie.transform.position;
            targetPos.y = head.position.y;
            head.rotation = Quaternion.Slerp(head.rotation, Quaternion.LookRotation(targetPos - head.position), roundSpeed*Time.deltaTime);
            if (Vector3.Angle(head.forward, targetPos - head.position) < 5 && Time.time - nowTime >= info.offsetTime)
            {
                nowTime= Time.time;
                targetZombie.Wound(info.atk);
                GameObject obj = Instantiate(Resources.Load<GameObject>(info.eff), firePoint.position, firePoint.rotation);
                GameDataManager.Instance.SoundPlay("Music/Tower");
                Destroy(obj,0.2f);
            }
        }
        else
        {
            if (Time.time - nowTime >= info.offsetTime)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>(info.eff), transform.position, transform.rotation);
                GameDataManager.Instance.SoundPlay("Music/Tower");
                Destroy(obj, 0.2f);
                nowTime = Time.time;
                List<ZombieObject> zombieList = FindZombies();
                for (int i = 0; i < zombieList.Count; i++)
                {
                    zombieList[i].Wound(info.atk);
                }
            }
        }
    }

    public List<ZombieObject> FindZombies()
    {
        List<ZombieObject> zombielist=new List<ZombieObject>();
        for (int i = 0; i < GameLevelManager.Instance.zombies.Count; i++)
        {
            if (Vector3.Distance(this.transform.position, GameLevelManager.Instance.zombies[i].transform.position)<=info.atkRange)
            {
                zombielist.Add(GameLevelManager.Instance.zombies[i]);
            }
        }
        return zombielist;
    }
}
