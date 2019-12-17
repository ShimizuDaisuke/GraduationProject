//=======================================================================================
//! @file   BatteryController.cs
//! @brief  バッテリーの処理
//! @author 田中歩夢
//! @date   12月13日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//バッテリーの処理
public class MotorController : MonoBehaviour
{
    //つながったかどうか
    private bool m_batteryConnectFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        //電気が当たったら
        if (collider.gameObject.tag == "Electricity")
        {
            m_batteryConnectFlag = true;

        }
    }

    //向きの取得・設定
    public bool MotorConConnectFlag { get { return m_batteryConnectFlag; } set { m_batteryConnectFlag = value; } }
}
