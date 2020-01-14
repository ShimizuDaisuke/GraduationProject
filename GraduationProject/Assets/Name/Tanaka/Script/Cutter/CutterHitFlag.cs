//=======================================================================================
//! @file   CutterHitFlag.cs
//! @brief  カッターが当たった時の処理
//! @author 田中歩夢
//! @date   01月14日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カッターが当たった時の処理
public class CutterHitFlag : MonoBehaviour
{
    private bool m_hitFlag = false;
   

    void OnCollisionEnter(Collision collider)
    {
        if (collider.transform.tag == "Cutter")
        {
            m_hitFlag = true;
        }
    }

    //フラグの取得・設定
    public bool HitFlag { get { return m_hitFlag; } set { m_hitFlag = value; } }

}
