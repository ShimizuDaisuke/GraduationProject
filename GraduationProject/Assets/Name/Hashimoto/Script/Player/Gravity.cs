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

public class Gravity : MonoBehaviour
{
    // 重力
    [SerializeField]
    private float gravity = 9.8f;

    // rigidbody 
    private Rigidbody rigidbody;

    void Start()
    {
        // このオブジェクトのrigidbodyを取得する
        rigidbody = GetComponent<Rigidbody>();

        // rigidbodyにある重力を無効にする
        rigidbody.useGravity = false;

    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void FixedUpdate()
    {
        // 重力
        Vector3 gravity_vec3 = new Vector3(0.0f, -gravity, 0.0f);

        // 常に重力が働く
        rigidbody.AddForce(gravity_vec3, ForceMode.Acceleration);
    } 
}
