using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float FreeRange = 1;
    private GameObject player;
    void Start()
    {
        player = GameManager.Instance.Player.gameObject;
    }

    void Update()
    {
        if(player != null)
        {
            Vector2 difference = (Vector2)transform.position - (Vector2)player.transform.position;
            if (difference.magnitude > FreeRange)
            {
                ShiftXY(player.transform.position, difference.normalized * FreeRange);
            }
        }
    }

    void ShiftXY(Vector3 position, Vector2 transformation)
    {
        transform.position = new Vector3(position.x + transformation.x, position.y + transformation.y, transform.position.z);
    }
}
