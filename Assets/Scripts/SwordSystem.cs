using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSystem : MonoBehaviour
{
    [SerializeField] private Material dissolveMat;
    private float dissolveAmount = 0;
    public bool isDissolving;
    private float dissolveSpeed = 3f;

    GameObject player;
    public float followSpeed = 3f;
    float xPosOffset = 0.7f;
    Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.localScale.x == 1)
        {
            //xPosOffset = Mathf.Abs(xPosOffset);
            newPos = new Vector3(player.transform.position.x + xPosOffset * -1, player.transform.position.y + 0.3f, -0.2f);
        }
        else
        {
            newPos = new Vector3(player.transform.position.x + xPosOffset, player.transform.position.y + 0.3f, -0.2f);
        }
        
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);

        if (isDissolving)
        {
            //dissolveAmount = Mathf.Clamp01(dissolveAmount + dissolveSpeed * Time.deltaTime);
            dissolveAmount = Mathf.Clamp(dissolveAmount + dissolveSpeed * Time.deltaTime, 0, 1.1f);
            dissolveMat.SetFloat("_DissolveAmount", dissolveAmount);
        }
        else
        {
            //dissolveAmount = Mathf.Clamp01(dissolveAmount - dissolveSpeed * Time.deltaTime);
            dissolveAmount = Mathf.Clamp(dissolveAmount - dissolveSpeed * Time.deltaTime, 0, 1.1f);
            dissolveMat.SetFloat("_DissolveAmount", dissolveAmount);
        }
    }

    public void Dissolving()
    {
        isDissolving = true;
    }

    public void Regaining()
    {
        isDissolving = false;
    }
}
