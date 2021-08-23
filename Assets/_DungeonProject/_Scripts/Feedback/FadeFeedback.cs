using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FadeFeedback : Feedback
{
    public UnityEvent OnFadeCompleted;

    [SerializeField] private Shader fadeShader;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float fadeDuration = .5f;

    private Shader originalShader;

    public override void CreateFeedback()
    {
        originalShader = spriteRenderer.material.shader;
        spriteRenderer.material.shader = fadeShader;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(
            spriteRenderer.material.DOFloat(1, "_FadeOpacity", fadeDuration)).
            AppendCallback(() => OnFadeCompleted?.Invoke());
    }

    public override void ResetFeedback()
    {
        if (spriteRenderer.material.HasProperty("_FadeOpacity"))
            spriteRenderer.material.SetFloat("_FadeOpacity", 0);

        if (originalShader)
            spriteRenderer.material.shader = originalShader;
        originalShader = null;

        spriteRenderer.material.DOComplete();
    }
}