using System;
using System.Threading.Tasks;
using DG.Tweening;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Tween = DG.Tweening.Tween;

[Category("D Custom/UI/Image Color")]
[Keywords("UI", "Action")]
[Serializable]
public class Action_UI_Image : Instruction
{
    public Image targetImage;
    public Color targetColor;
    public float duration;
    public bool waitComplete;
    public Ease mEasing = Ease.Linear;
    private Tween mTween;
    private bool isWaiting;

    // public override string Title => $"{character.name} Charge To : {targetCharge.name}";

    protected override async Task Run(Args args)
    {
        isWaiting = waitComplete;
        mTween = targetImage.DOColor(targetColor, duration).SetEase(mEasing).SetUpdate(true);
        while (isWaiting) await Task.Yield();
    }
}