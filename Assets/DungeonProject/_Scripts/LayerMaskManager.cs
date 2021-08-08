using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerMaskManager
{
    private static HashSet<string> attackableLayers = new HashSet<string>() { "Player", "Enemy" };

    public static bool IsAttackable(LayerMask layer)
    {
        return attackableLayers.Contains(LayerMask.LayerToName(layer));
    }

    public static LayerMask GetAttackLayer(LayerMask layer)
    {
        string resultLayerName;

        switch (LayerMask.LayerToName(layer))
        {
            case "Player":
                resultLayerName = "PlayerAttack";
                break;
            case "Enemy":
                resultLayerName = "EnemyAttack";
                break;
            default:
                throw new UnityException("Invalid layer: " + layer);                
        }

        return LayerMask.NameToLayer(resultLayerName);
    }
}
