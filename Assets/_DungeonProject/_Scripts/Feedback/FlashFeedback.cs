using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(FeedbackPlayer))]
public class FlashFeedback : Feedback
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
        if(spriteRenderer.material.HasProperty("_TurnFlash"))
            spriteRenderer.material.SetFloat("_TurnFlash", 0);

        if(originalShader)
            spriteRenderer.material.shader = originalShader;
        originalShader = null;

        StopAllCoroutines();
    }

    public IEnumerator FlashRoutine()
    {
        Assert.IsTrue(spriteRenderer.material.HasProperty("_TurnFlash"));
        spriteRenderer.material.SetFloat("_TurnFlash", 1);

        yield return new WaitForSeconds(flashDuration);

        ResetFeedback();
    }
}