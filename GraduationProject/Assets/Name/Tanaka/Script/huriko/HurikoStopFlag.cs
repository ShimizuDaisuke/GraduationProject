//=======================================================================================
//! @file   HurikoStopFlag.cs
//! @brief  振り子停止フラグの処理
//! @author 田中歩夢
//! @date   01月15日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//振り子停止フラグ
public class HurikoStopFlag : MonoBehaviour
{
    //停止フラグ
    private bool m_stopFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter(Collision collider)
    {
        if (collider.transform.tag == "Player")
        {
            m_stopFlag = true;
        }
    }

    //フラグの取得・設定
    public bool StopFlag { get { return m_stopFlag; } set { m_stopFlag = value; } }
}
