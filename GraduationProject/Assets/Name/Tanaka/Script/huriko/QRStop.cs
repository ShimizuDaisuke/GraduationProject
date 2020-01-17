//=======================================================================================
//! @file   QRStop.cs
//! @brief  振り子停止の処理
//! @author 田中歩夢
//! @date   01月15日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//振り子停止のクラス
public class QRStop : MonoBehaviour
{
    [SerializeField]
    private HurikoStopFlag m_hurikoStopFlag = default;

    //スタート座標  
    private Vector3 m_startPos = default;

    // Start is called before the first frame update
    void Start()
    {
        m_startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_hurikoStopFlag.StopFlag)
        {
            transform.position = m_startPos;
        }
    }
}
