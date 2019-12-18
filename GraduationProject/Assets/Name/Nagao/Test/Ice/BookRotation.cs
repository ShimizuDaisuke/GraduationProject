using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookRotation : MonoBehaviour
{
    private int a = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // rigidbodyを取得
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        //垂直かどうか
        if(a == 0)
        {
            // 回転、位置ともに固定
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            //rigidbodyのフリーズを解除する
            rigidbody.constraints = RigidbodyConstraints.None;
        }

    }

    void OnTriggerExit(Collider collider)
    {
        //糸がなくなったら
        if (collider.gameObject.tag == "Thread")
        {
            //本が倒れる
            a = -10;
            transform.Rotate(new Vector3(0, 0, 1), a);

        }

       
    }
}
