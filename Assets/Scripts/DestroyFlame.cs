using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFlame : MonoBehaviour
{
    Transform Enemy;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        transform.position = Enemy.transform.position + new Vector3(0, 0.95f, -0.1f);
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
}
