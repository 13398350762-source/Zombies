using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    private Image ImgTower;
    public Text txtNeedMoney;
    public Text txtKey;

    private void Awake()
    {
        ImgTower= GetComponent<Image>();
    }

    public void Init(TowerInfo info,string str)
    {
        ImgTower.sprite = Resources.Load<Sprite>(info.imgRes);

        txtNeedMoney.text = info.money.ToString();

        txtKey.text = str;
    }
}
