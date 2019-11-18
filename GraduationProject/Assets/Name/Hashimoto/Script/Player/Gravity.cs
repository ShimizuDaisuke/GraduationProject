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
// 警告を無効にする
#pragma warning disable CS0109

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
    private float GravityObj = 100.0f;

    // 重力の最大速度
    [SerializeField]
    private float MaxGravity = 3.0f;

    // プレイヤー
    private GameObject PlayerObj;

    // rigidbody 
    private new Rigidbody rigidbody;

    // スクリプト：イベント監督用のスクリプト
    private EventDirector Script_EventDirector;

    void Start()
    {
        // プレイヤーを探す
        PlayerObj = GameObject.FindGameObjectWithTag("Player");

        // プレイヤーのオブジェクトのrigidbodyを取得する
        rigidbody = PlayerObj.GetComponent<Rigidbody>();

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
            Vector3 gravity_vec3 = new Vector3(0.0f, -GravityObj, 0.0f);

            // 手動で常に重力が働くようにする
            rigidbody.AddForce(gravity_vec3, ForceMode.Acceleration);

            Debug.Log(rigidbody.velocity.y);

            // 重力の速度が範囲内より越えた場合
            if(rigidbody.velocity.y < -MaxGravity)
            {
                // 重力の速度を制限する
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, -MaxGravity, rigidbody.velocity.z);
            }


        }
        else
        {
            // rigidbodyによる重力を有効にして、自動に重力が働くようにする
            rigidbody.useGravity = true;

        }


    } 
}
