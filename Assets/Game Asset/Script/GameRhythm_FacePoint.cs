using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameRhythm_FacePoint : MonoBehaviour
{
    public SpriteRenderer circleIndicator;
    public Transform debugIndicator;
    public float duration;
    private Tween scaleTween;
    public Vector3 currentScale;
    public float targetScale;
    public Action<float> onPointPressedE;

    [Button(ButtonSizes.Gigantic)]
    public void InitScale()
    {
        debugIndicator.transform.localScale = Vector3.one * targetScale;
        circleIndicator.transform.localScale = Vector3.one;
        circleIndicator.gameObject.SetActive(false);
    }

    [Button(ButtonSizes.Gigantic)]
    public void StartCountDown(Action<float> callback)
    {
        onPointPressedE = callback;
        InitScale();
        circleIndicator.gameObject.SetActive(true);
        circleIndicator.DOFade(1f, 0.1f);
        gameObject.SetActive(true);
        scaleTween = circleIndicator.transform.DOScale(Vector3.zero, duration).SetEase(Ease.Linear);
    }

    public void KillAnim()
    {
        if (scaleTween != null && scaleTween.IsActive()) scaleTween.Kill();
        circleIndicator.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

      void OnMouseDown()
    {
        currentScale = circleIndicator.transform.localScale;
        if (scaleTween != null && scaleTween.IsActive())
        {
            scaleTween.Kill();
        }

        Debug.Log($"Hit {name}", gameObject);
        onPointPressedE?.Invoke(currentScale.magnitude >= targetScale ? 1 : 0);
        circleIndicator.gameObject.SetActive(false);
        onPointPressedE = null;
        gameObject.SetActive(false);
    }
}