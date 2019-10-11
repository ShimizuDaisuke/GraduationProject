//=======================================================================================
//! @file   StatusFunction
//! @brief  QRによる読み込みでステータスなど上昇させる関数が集まるクラス
//! @author 志水大輔
//! @date   10/4
//! @note   関数増やす予定あり
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusFunction : MonoBehaviour
{
    // 増やす時間
    private float addTime = 30.0f;

    //=======================================================================================
    //! @brief 時間の追加
    //! @param[in] time 現在の時間
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    public void IncreaseTime(ref float time)
    {
        // 時間を増やす
        time += addTime;
    }
    
}
