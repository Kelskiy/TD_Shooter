using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Variables
    private Transform player;
    public float smooth = 0.3f;

    public float height;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        player = GameManager.Instance.player;
    }

    // Methods
    void Update()
    {
        if (player == null)
            return;

        Vector3 pos = new Vector3();
        pos.x = player.position.x;
        pos.z = player.position.z - 5f;
        pos.y = player.position.y + height;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);


    }
}
