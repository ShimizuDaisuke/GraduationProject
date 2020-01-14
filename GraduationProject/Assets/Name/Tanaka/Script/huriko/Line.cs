//=======================================================================================
//! @file   Line.cs
//! @brief  振り子の線の処理
//! @author 田中歩夢
//! @date   11月18日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//振り子の線の処理
public class Line : MonoBehaviour
{
    [SerializeField]
    private LineRenderer line = null;

    [SerializeField]
    private Transform pivot = null;

    /// <summary>
    /// 毎フレームごとの処理
    /// </summary>
    public void Update()
    {
        // ヒモ部分の始点と終点の座標を更新
        this.line.SetPosition(0, this.transform.position);
        this.line.SetPosition(1, this.pivot.position);
    }
}
