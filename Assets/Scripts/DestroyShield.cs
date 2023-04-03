using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShield : MonoBehaviour
{
    public Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        this.transform.SetParent(player.parent);
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
}
