using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    Vector3 offset = new Vector3(0, 3, -6);

    void LateUpdate()
    {
        transform.position = player.position + offset;
        transform.LookAt(player);
    }
}