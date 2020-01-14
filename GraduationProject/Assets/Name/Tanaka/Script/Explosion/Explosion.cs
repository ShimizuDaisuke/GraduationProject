//=======================================================================================
//! @file   Explosion.cs
//! @brief  爆発（吹っ飛ばし）の処理
//! @author 田中歩夢
//! @date   11月18日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//爆発（吹っ飛ばし）の処理
public class Explosion : MonoBehaviour
{
    public float coefficient;   // 空気抵抗係数
    public float speed;         // 爆風の速さ

    void OnTriggerStay(Collider col)
    {
        if (col.attachedRigidbody == null)
        {
            return;
        }

        // 風速計算
        var velocity = (col.transform.position - transform.position).normalized * speed;

        // 風力与える
        col.attachedRigidbody.AddForce(coefficient * velocity);
    }
}
