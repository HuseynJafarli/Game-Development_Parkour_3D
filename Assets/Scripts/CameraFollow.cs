using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 4, -6);
    public float lookOffset = 2.0f; 

    void LateUpdate()
    {
        transform.position = player.position + offset;

        Vector3 lookTarget = player.position + Vector3.up * lookOffset;
        transform.LookAt(lookTarget);
    }
}