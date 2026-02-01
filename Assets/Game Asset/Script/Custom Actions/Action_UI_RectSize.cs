using System;
using System.Threading.Tasks;
using DG.Tweening;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using Tween = DG.Tweening.Tween;

[Category("D Custom/UI/Rect Size")]
[Keywords("UI", "Action")]
[Serializable]
public class Action_UI_RectSize : Instruction
{
    public RectTransform rectTransform;
    public bool keptWidth;
    public float targetWidth;
    public bool keptHeight;
    public float targetHeight;
    public float duration;
    public bool waitComplete;
    public Ease mEasing = Ease.Linear;
    private Tween mTween;
    private bool isWaiting;

    // public override string Title => $"{character.name} Charge To : {targetCharge.name}";
    private float targetW, targetH;

    protected override async Task Run(Args args)
    {
        isWaiting = waitComplete;
        targetW = targetWidth;
        targetH = targetHeight;
        if (keptWidth) targetW = rectTransform.sizeDelta.x;
        if (keptHeight) targetH = rectTransform.sizeDelta.y;
        Vector2 targetSize = new Vector2(targetWidth, targetHeight);
        mTween = rectTransform.DOSizeDelta(targetSize, duration).SetEase(mEasing).SetUpdate(true)
            .OnComplete(delegate { isWaiting = false; });
        while (isWaiting) await Task.Yield();
    }
}