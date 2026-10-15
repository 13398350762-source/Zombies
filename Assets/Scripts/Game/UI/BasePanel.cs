using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasgroup;

    public UnityAction hidecallback;

    public float speed=10;

    private bool isShow = true;

    private void Awake()
    {
        canvasgroup = GetComponent<CanvasGroup>();
        if(canvasgroup==null)
            canvasgroup=this.AddComponent<CanvasGroup>();
        Init();
    }

    protected void Update()
    {
        if (isShow)
        {
            canvasgroup.alpha += speed * Time.unscaledDeltaTime;
            if(canvasgroup.alpha>=1)
                canvasgroup.alpha = 1;
        }
        else
        {
            canvasgroup.alpha -= speed * Time.unscaledDeltaTime;
            if (canvasgroup.alpha <= 0)
            {
                canvasgroup.alpha = 0;
                hidecallback?.Invoke();
            }
        }
    }

    virtual public void ShowMe()
    {
        canvasgroup.alpha = 0;
        isShow = true;
    }

    virtual public void HideMe(UnityAction callback)
    {
        canvasgroup.alpha = 1;
        isShow = false;
        hidecallback = callback;
    }

    protected abstract void Init();

}
