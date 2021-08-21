using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyDeathFeedback : Feedback
{
    [SerializeField] Shader dissolveShader;
    [SerializeField] SpriteRenderer dissolveTarget;
    [SerializeField] float dissolveDuration = .5f;

    Shader originalShader;

    public override void CreateFeedback()
    {
        originalShader = dissolveTarget.material.shader;
        dissolveTarget.material.shader = dissolveShader;

        Sequence sequence = DOTween.Sequence();
        sequence.
            Append(dissolveTarget.material.DOFloat(0, "_Step", dissolveDuration)).
            AppendCallback(ResetFeedback);            
    }

    public override void ResetFeedback()
    {
        dissolveTarget.material.DOComplete();

        if (dissolveTarget.material.HasProperty("_Step"))
            dissolveTarget.material.SetFloat("_Step", 1);

        if (originalShader != null)
        {
            dissolveTarget.material.shader = originalShader;
            originalShader = null;
        }            
    }
}