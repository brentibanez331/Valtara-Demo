using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            transform.position = transform.position;
        }
        else
        {
            Vector3 newPos = new Vector3(player.position.x, player.position.y + 2f, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }
        
    }
}
