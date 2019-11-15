//=======================================================================================
//! @file   Gravity.cs
//!
//! @brief  オブジェクトに重力をつける
//!
//! @author 橋本奉武
//!
//! @date   10月10日
//!
//! @note   重力の値を変えられる、TargetObj→そのオブジェクトが非表示されても重力が働く
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

    // 重力を変える対象となるオブジェクト
    [SerializeField]
    private GameObject TargetObj = default;

    // 重力[目安:50]
    [SerializeField]
    private float GravityObj = 50.0f;

    // 重力の最大速度[目安:2]
    [SerializeField]
    private float MaxGravity = 2.0f;

    // rigidbody 
    private new Rigidbody rigidbody;

    // スクリプト：イベント監督用のスクリプト
    private EventDirector Script_EventDirector;

    void Start()
    {
        if (TargetObj != null)
        {
            // 重力を変える対象となるオブジェクトのオブジェクトのrigidbodyを取得する
            rigidbody = TargetObj.GetComponent<Rigidbody>();
        }
        else
        {
            // このオブジェクトのrigidbodyを取得する
            rigidbody = GetComponent<Rigidbody>();
        }

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
