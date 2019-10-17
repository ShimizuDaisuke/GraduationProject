//======================================================================================= 
//! @file       CrayonBreak.cs
//! @brief      playerが離れたらクレヨンを破壊！
//! @author     長尾昌輝
//! @date       2019/10/16
//======================================================================================= 
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrayonBreak : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionExit(Collision col)
    {
        //プレイヤーが離れたら
        if(col.gameObject.tag == "Player")
        {
            destroyObject();
            Debug.Log("ウンチ！");
        }
    }

    public void destroyObject()
    {
        //バラバラになる処理
        var random = new System.Random();
        var min = -3;
        var max = 3;
        gameObject.GetComponentsInChildren<Rigidbody>().ToList().ForEach
            (r =>
        {
            r.isKinematic = false;
            r.transform.SetParent(null);
            r.gameObject.AddComponent<AutoDestroy>().time = 2f;
            var vect = new Vector3(random.Next(min, max), random.Next(0, max), random.Next(min, max));
            r.AddForce(vect, ForceMode.Impulse);
            r.AddTorque(vect, ForceMode.Impulse);
        });

        //削除
        Destroy(this.gameObject);
    }
}