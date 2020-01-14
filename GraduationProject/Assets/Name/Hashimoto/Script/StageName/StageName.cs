// -----------------------------------------------------------------------------------------
//! @file       StageName.cs
//!
//! @brief      ステージ名を取得する
//!
//! @author     橋本 奉武
//!
//! @date       2020.1.14
// -----------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageName : MonoBehaviour
{
    // クリアしたか判断できるオブジェクト
    [SerializeField]
    private GameObject ClearObject = default;


    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // ステージ名を取得
        ClearObject.GetComponent<ClearManagement>().PlayingStageName = SceneManager.GetActiveScene().name;
    }

}
