using System;
using System.Threading.Tasks;
using DG.Tweening;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using Tween = DG.Tweening.Tween;

[Category("D Custom/UI/Rect Anchor Pos")]
[Keywords("UI", "Action")]
[Serializable]
public class Action_UI_AnchorPos : Instruction
{
    public RectTransform rectTransform;
    public Vector2 targetAnchorPos;
    public float duration;
    public bool waitComplete;
    public Ease mEasing = Ease.Linear;
    private Tween mTween;
    private bool isWaiting;

    // public override string Title => $"{character.name} Charge To : {targetCharge.name}";

    protected override async Task Run(Args args)
    {
        isWaiting = waitComplete;
        mTween = rectTransform.DOAnchorPos(targetAnchorPos, duration).SetEase(mEasing).SetUpdate(true).OnComplete(
            delegate { isWaiting = false; });
        while (isWaiting) await Task.Yield();
    }
}