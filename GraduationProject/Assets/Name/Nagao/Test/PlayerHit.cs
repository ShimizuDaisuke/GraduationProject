using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    //プレイヤーの大きさ
    [SerializeField]
    private Vector3 size = new Vector3(1.0f,1.0f,1.0f);

   // Start is called before the first frame update
   void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = size;
    }


    //OnTriggerEnter
    void OnTriggerStay(Collider col)
    {
        
        if (col.gameObject.tag == "sword")
        {
            Debug.Log("うんち！");
            size +=  new Vector3(0.01f, 0.01f, 0.01f);
        }
    }
}
