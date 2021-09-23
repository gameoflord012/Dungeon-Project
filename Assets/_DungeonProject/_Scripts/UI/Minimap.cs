using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] Transform player;

    private void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}