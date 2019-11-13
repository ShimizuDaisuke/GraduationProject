//=======================================================================================
//! @file   Gravity.cs
//!
//! @brief  オブジェクトに重力をつける
//!
//! @author 橋本奉武
//!
//! @date   10月10日
//!
//! @note   重力の値を変えられる
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 型名を省略
using EventKind = EventDirector.EventKIND;

public class Gravity : MonoBehaviour
{
    // イベントの監督
    [SerializeField]
    private GameObject EventDirectorObj = default;
    
    // 重力
    [SerializeField]
    private float gravity = 100.0f;

    // rigidbody 
    private new Rigidbody rigidbody;

    // スクリプト：イベント監督用のスクリプト
    private EventDirector Script_EventDirector;

    void Start()
    {
        // このオブジェクトのrigidbodyを取得する
        rigidbody = GetComponent<Rigidbody>();

        // スクリプト：イベント監督用のスクリプト 取得
        Script_EventDirector = EventDirectorObj.GetComponent<EventDirector>();

        // rigidbodyによる重力を無効にする
        rigidbody.useGravity = false;

    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void FixedUpdate()
    {
        // イベントが発生されていない場合
        if(Script_EventDirector.IsEventKIND == EventKind.NONE)
        {
            // rigidbodyによる重力を無効にする
            rigidbody.useGravity = false;

            // 重力
            Vector3 gravity_vec3 = new Vector3(0.0f, -gravity, 0.0f);

            // 手動で常に重力が働くようにする
            rigidbody.AddForce(gravity_vec3, ForceMode.Acceleration);

        }
        else
        {
            // rigidbodyによる重力を有効にして、自動に重力が働くようにする
            rigidbody.useGravity = true;

        }


    } 
}
