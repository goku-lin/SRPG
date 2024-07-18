using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 简单的tip界面
/// </summary>
public class TipView : BaseView
{
    public override void Open(params object[] args)
    {
        base.Open(args);
        Find<Text>("content/txt").text = args[0].ToString();
        Sequence seq = DOTween.Sequence();
        seq.Append(Find("content").transform.DOScaleY(1, 0.15f)).SetEase(Ease.OutBack);
        seq.AppendInterval(0.75f);
        seq.Append(Find("content").transform.DOScaleY(0, 0.15f)).SetEase(Ease.Linear);
        seq.AppendCallback(() =>
        {
            GameApp.ViewManager.Close(ViewId);
        });
    }
}
