using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    public TowerInfo nowTower;
    public GameObject towerobj;
    public List<int> chooseIDs;
    public bool isBuild = false;
    private void OnTriggerStay(Collider other)
    {
        UIManager.Instance.GetPanel<GamePanel>().OnSelUpdateTower(this);
    }

    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.GetPanel<GamePanel>().OnSelUpdateTower(null);
    }
    public void CreateTower(TowerInfo info)
    {
        int num = int.Parse(UIManager.Instance.GetPanel<GamePanel>().txtMomey.text);
        if (num < info.money)
            return;
        num -= info.money;
        UIManager.Instance.GetPanel<GamePanel>().txtMomey.text = num.ToString();
        nowTower = info;
        Destroy(towerobj);
        towerobj = Instantiate(Resources.Load<GameObject>(info.res));
        towerobj.transform.position = transform.position;
        TowerObject to = towerobj.GetComponent<TowerObject>();
        to.InitInfo(info);
        isBuild = true;
    }
}
