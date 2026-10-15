using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public Text txtContent;
    public Button btnSure;
    protected override void Init()
    {
        btnSure.onClick.AddListener(() =>{
            UIManager.Instance.HidePanel<TipPanel>();
        });
    }

    public void ChangeContent(string content)
    {
        txtContent.text = content;
    }

}
