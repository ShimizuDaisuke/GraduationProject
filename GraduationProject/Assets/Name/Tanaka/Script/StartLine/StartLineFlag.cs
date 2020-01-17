//=======================================================================================
//! @file   StartLineFlag.cs
//! @brief  スタートラインを超えたフラグの処理
//! @author 田中歩夢
//! @date   01月17日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//スタートラインを超えたフラグ
public class StartLineFlag : MonoBehaviour
{
    //スタートフラグ
    private bool m_startFlag = false;

    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
       
        if (collider.gameObject.tag == "Player")
        {
            m_startFlag = true;
        }

    }

    // スタートラインを超えたフラグ
    public bool StartFlag { get { return m_startFlag; } set { m_startFlag = value; } }

}
