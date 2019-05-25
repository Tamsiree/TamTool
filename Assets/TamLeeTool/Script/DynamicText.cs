using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicText : WindowRoot
{
    public Animation tipsAni;
    public Text txtTips;

    private bool isTipsShow = false;
    private Queue<string> tipsQue = new Queue<string>();
    protected override void InitWind()
    {
        base.InitWind();
        SetActive(txtTips, false);
    }

    public void AddTips(string tips)
    {
        lock (tipsQue)
        {
            tipsQue.Enqueue(tips);
        }
    }

    private void Update()
    {
        if (tipsQue.Count > 0 && isTipsShow == false)
        {
            lock (tipsQue)
            {
                string tips = tipsQue.Dequeue();
                isTipsShow = true;
                SetTips(tips);
            }
        }
    }

    public void SetTips(string name)
    {
        SetActive(txtTips);
        SetText(txtTips, name);

        AnimationClip clip = tipsAni.GetClip("TipsShowAni");
        tipsAni.Play();

        StartCoroutine(AnimationPlayDone(clip.length, () =>
        {
            SetActive(txtTips, false);
            isTipsShow = false;
        }));

    }

    private IEnumerator AnimationPlayDone(float second, Action action)
    {
        yield return new WaitForSeconds(second * 0.8f);
        if (action != null)
        {
            action();
        }
    }
}
