using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootFollow : MonoBehaviour
{

    public Transform target;
    public float minModifier = 0.1f;
    public float maxModifier = 0.2f;

    Vector3 velocity = Vector3.zero;
    bool isFollowing = false;


    public void StartFollowing()
    {
        isFollowing = true; 
    }

    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, Time.deltaTime * Random.Range(minModifier, maxModifier));
        }
        
    }
}
