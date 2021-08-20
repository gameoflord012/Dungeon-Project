using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FeedbackPlayer))]
public class EnemyHitFeedback : Feedback
{
    [SerializeField] private Shader flashShader;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float flashDuration = .2f;

    private Shader originalShader;

    public override void CreateFeedback()
    {
        originalShader = spriteRenderer.material.shader;
        spriteRenderer.material.shader = flashShader;
        StartCoroutine(FlashRoutine());
    }

    public override void ResetFeedback()
    {
        spriteRenderer.material.shader = originalShader;
        StopAllCoroutines();
        spriteRenderer.material.SetFloat("_TurnFlash", 0);
    }

    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material.SetFloat("_TurnFlash", 1);
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.material.SetFloat("_TurnFlash", 0);
    }
}