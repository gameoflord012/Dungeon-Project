//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FindTargetAction : GoapActionBase
//{    
//    [field: SerializeField] public override GameObject Target { get; set; }
//    [field: SerializeField] public override float Cost { get; set; }

//    public override IEnumerable<KeyValuePair<string, object>> GetEffects()
//    {
//        yield return new KeyValuePair<string, object>("HasTarget", true);
//    }

//    public override IEnumerable<KeyValuePair<string, object>> GetPreconditions()
//    {
//        yield break;
//    }

//    public override IEnumerator<PerformState> perform(GameObject agent)
//    {
//        foreach (GameObject chaseTarget in FindChaseTargets())
//        {
//            data.Target = chaseTarget;
//            yield break;
//        }
//        yield return PerformState.falied;
//    }

    
//}