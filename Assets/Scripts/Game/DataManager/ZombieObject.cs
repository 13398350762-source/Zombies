using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieObject : MonoBehaviour
{
    public bool isDead = false;
    private Animator animator;
    private NavMeshAgent agent;
    private ZombieInfo info;
    private int hp;
    private float preTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void Init(ZombieInfo info)
    {
        this.info= info;
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(info.animator);
        agent.angularSpeed = info.roundSpeed;
        agent.speed = agent.acceleration = info.moveSpeed;
        hp = info.hp;
    }

    public void Wound(int damage)
    {
        if (isDead == true)
            return;
        hp-= damage;
        animator.SetTrigger("damage");
        if (hp <= 0)
        {
            isDead = true;
            Dead();
        }
        else
        {
            GameDataManager.Instance.SoundPlay("Music/Wound");
        }
    }
    public void Dead()
    {
        GameDataManager.Instance.SoundPlay("Music/dead");
        animator.SetBool("dead",true);
        agent.isStopped = true;
        UIManager.Instance.GetPanel<GamePanel>().AddMoney(25);
        Invoke("DestroyZombie", 5f);
        GameLevelManager.Instance.DestroyZombie(this);
    }

    private void DestroyZombie()
    {
        Destroy(gameObject);
    }

    public void BornOver()
    {
        agent.SetDestination(MainTowerObject.Instance.transform.position);
        animator.SetBool("run", true);
    }

    void Update()
    {
        if (isDead)
            return;
        animator.SetBool("run",agent.velocity != Vector3.zero);

        if (Vector3.Distance(transform.position, MainTowerObject.Instance.transform.position) < 5 && Time.time - preTime >= info.atkOffset)
        {
            animator.SetTrigger("attack");
            preTime = Time.time;
        }
    }

    public void AtkEvent()
    {
        GameDataManager.Instance.SoundPlay("Music/Eat");
        Collider[] colliders=Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1,LayerMask.GetMask("MainTower"));
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject==MainTowerObject.Instance.gameObject)
                MainTowerObject.Instance.Wound(info.atk);
        }
    }
}
