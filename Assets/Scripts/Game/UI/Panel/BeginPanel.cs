using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnStart;
    public Button btnSet;
    public Button btnAbout;
    public Button btnExit;

    Animator animator;

    protected override void Init()
    {
        animator=Camera.main.GetComponent<Animator>();
        btnStart.onClick.AddListener(() => {
            UIManager.Instance.HidePanel<BeginPanel>();
            Camera.main.GetComponent<CameraAnimator>().TurnLeft(() =>{
                UIManager.Instance.ShowPanel<ChooseHeroPanel>();
            });
        });

        btnSet.onClick.AddListener(() => {
            UIManager.Instance.ShowPanel<SettingPanel>();
        });

        //btnAbout.onClick.AddListener(() => {
        //    UIManager.Instance.ShowPanel<AboutPanel>();
        //});

        btnExit.onClick.AddListener(() => {
            Application.Quit();
        });
    }

}
