using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerObject : MonoBehaviour
{
    private Transform firePos;
    public int money;
    public int atk;
    public float roundSpeed=35f;
    public float maxSpeed=6f;
    public float maxSpeedSpecial = 3f;
    public float accelerate = 20f;
    public float accelerateSpecial = 8f;
    private Vector3 currentDir=Vector3.zero;
    private Vector3 Dir;
    private Vector3 targetDir;
    private Animator animator;
    private CharacterController controller;
    private bool IsDown=false;
    private float shiftSpeed = 5f;
    private float targetLayer0=1f;
    private float targetLayer1=0f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetLayerWeight(2, 1);
    }

    public void Init(int atk, int money)
    {
        this.atk= atk;
        this.money= money;
        firePos = FindChildByName(transform, "firepos");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            IsDown = true;
            targetLayer1 = 1f;
            targetLayer0 = 0f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            IsDown = false;
            targetLayer1 = 0f;
            targetLayer0 = 1f;
        }

        animator.SetLayerWeight(0,Mathf.MoveTowards(animator.GetLayerWeight(0),targetLayer0,Time.deltaTime*shiftSpeed));
        animator.SetLayerWeight(1, Mathf.MoveTowards(animator.GetLayerWeight(1), targetLayer1, Time.deltaTime * shiftSpeed));

        Dir = (Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward).normalized;
        if (IsDown)
        {
            targetDir = Dir * maxSpeedSpecial;
        }
        else
        {
            targetDir = Dir * maxSpeed;
        }
        currentDir = Vector3.MoveTowards(currentDir, targetDir, Time.deltaTime * accelerate);
        controller.Move(currentDir * Time.deltaTime);
        animator.SetFloat("x", Vector3.Dot(currentDir, transform.right) / maxSpeed);
        animator.SetFloat("y", Vector3.Dot(currentDir, transform.forward) / maxSpeed);
        transform.Rotate(Vector3.up, Time.deltaTime * roundSpeed * Input.GetAxis("Mouse X"));

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("k1");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("roll");
        }
    }

    public void UpdateMoney()
    {
        UIManager.Instance.GetPanel<GamePanel>().txtMomey.text = money.ToString();
    }

    public void AddMoney(int Money)
    {
        money += Money;
        UpdateMoney();
    }

    public void Knife()
    {
        GameDataManager.Instance.SoundPlay("Music/Knife");
        Collider[] colliders=Physics.OverlapSphere(transform.position+transform.forward+transform.up,1,LayerMask.GetMask("Zombie"));
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<ZombieObject>().Wound(atk);
        }
    }

    public void Gun()
    {
        GameDataManager.Instance.SoundPlay("Music/Gun");
        RaycastHit[] hits = Physics.RaycastAll(firePos.position, firePos.forward, 1000,  LayerMask.GetMask("Zombie"));
        for (int i = 0; i < hits.Length; i++)
        {
            hits[i].collider.GetComponent<ZombieObject>().Wound(atk);
            GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>(GameDataManager.Instance.nowRole.hitEff));
            obj.transform.position = hits[i].point;
            GameObject.Destroy(obj, 1);
        }
    }


    public static Transform FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;

            Transform result = FindChildByName(child, name);

            if (result != null)
                return result;
        }

        return null;
    }
}
