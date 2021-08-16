using UnityEngine;


public class PatrolTask : MonoBehaviour
{    
    [SerializeField] PathNode currentPathNode;

    
    bool AdvancedPath()
    {
        if (currentPathNode.neighbors.Count == 0) return false;
        currentPathNode = currentPathNode.neighbors[Random.Range(0, currentPathNode.neighbors.Count - 1)];
        return true;
    }
}
