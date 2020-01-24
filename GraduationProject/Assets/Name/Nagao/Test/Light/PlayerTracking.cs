//======================================================================================= 
//! @file       PlayerTracking.cs
//! @brief      プレイヤーを追尾させる
//! @author     長尾昌輝 
//! @date       2020/01/21
//======================================================================================= 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracking : MonoBehaviour
{
    //追尾するターゲットのオブジェクト
    [SerializeField]
    private GameObject TargetObj = default;

    //追尾する方のオブジェクトの位置
    Vector3 basePos;

    //Y軸を固定させるために
    [SerializeField]
    private Vector3 m_pos  = new Vector3(0.0f,0.0f,0.0f);

    //３Dか２Dの判定フラグ
    [SerializeField]
    private bool m_flag = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ターゲットの位置を代入する
        Vector3 targetPos = TargetObj.transform.position;

        if(m_flag)
        {
            //X軸を同じにする
            basePos.x = targetPos.x;

            //Y軸を一定で固定する
            basePos.y = m_pos.y + targetPos.y;

            //Z軸を同じにする
            basePos.z = targetPos.z;

            //代入
            this.gameObject.transform.position = basePos;
        }
        else
        {
            //X軸を同じにする
            basePos.x = targetPos.x;

            //Y軸を同じにする
            basePos.y = targetPos.y;

            //Z軸を一定で固定する
            basePos.z = m_pos.z + targetPos.z;

            //代入
            this.gameObject.transform.position = basePos;
        }

    }
}
