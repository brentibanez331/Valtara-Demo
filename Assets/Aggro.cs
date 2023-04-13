using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggro : MonoBehaviour
{

    public SlimeBehaviour sb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sb.startAction = true;
            Destroy(this.gameObject);
        }    
    }
}
