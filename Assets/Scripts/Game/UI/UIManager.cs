using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    static private UIManager instance = new UIManager();

    static public UIManager Instance => instance;

    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    private Transform canvasTrans;

    private UIManager()
    {
        canvasTrans = GameObject.Find("Canvas").transform;
        GameObject.DontDestroyOnLoad(canvasTrans);
    }

    public T ShowPanel<T>() where T : BasePanel
    {
        string name = typeof(T).Name;
        if (panelDic.ContainsKey(name))
            return panelDic[name] as T;

        GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + name));
        T panel = obj.GetComponent<T>();
        obj.transform.SetParent(canvasTrans, false);
        panelDic.Add(name, panel);
        panel.ShowMe();
        return panel;
    }

    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        string name = typeof(T).Name;
        if (panelDic.ContainsKey(name))
        {
            T panel = panelDic[name] as T;
            if (isFade)
            {
                panel.HideMe(() =>
                {
                    GameObject.Destroy(panel.gameObject);
                    panelDic.Remove(name);
                });

            }
            else
            {
                GameObject.Destroy(panel.gameObject);
                panelDic.Remove(name);
            }
        }
    }

    public T GetPanel<T>() where T : BasePanel
    {
        if (panelDic.ContainsKey(typeof(T).Name))
            return panelDic[typeof(T).Name] as T;
        return null;
    }
}