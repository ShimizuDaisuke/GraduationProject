//=======================================================================================
//! @file   SpringScale.cs
//!
//! @brief  ばねサイズ
//!
//! @author 橋本奉武
//!
//! @date   10月10日
//!
//! @note  <参考> ばね振り子の力学的エネルギー
//!               http://www.wakariyasui.sakura.ne.jp/p/mech/rikiene/banehuriko.html
//!               https://assets.clip-studio.com/ja-jp/detail?id=1302083
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringScale : MonoBehaviour
{
    // ばね定数 k[N/m]
    [SerializeField]
    private float k = 1.0f;

    // 自然長から引っ張った長さ a[m]
    [SerializeField]
    private float a = 10.0f;

    // ばねの自然長 e[m]
    private float e = 0.0f;

    // 現在の自然長から引っ張られた長さ　x[m]
    private float x = 0.0f;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // ばねの自然長を取得
        e = transform.localScale.y;

        // 弾性エネルギー
        float U = (k * a * a) / 2.0f;

        // 実際にサイズを変える
        transform.localScale = new Vector3(transform.localScale.x,
                                                 U,
                                                 transform.localScale.z);

        // 現在の自然長から引っ張られた長さを調べる
        x = transform.localScale.y - e;


    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 弾性エネルギー
        float U = (k * x * x) / 2.0f;

        // 実際にサイズを変える
        transform.localScale = new Vector3(transform.localScale.x,
                                                 U,
                                                 transform.localScale.z);

        // 現在の自然長から引っ張られた長さを調べる
        x = transform.localScale.y - e;
    }
}
