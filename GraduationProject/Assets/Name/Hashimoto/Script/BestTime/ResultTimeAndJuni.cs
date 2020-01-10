//=======================================================================================
//! @file   FinalTimeWriteLoad.cs
//!
//! @brief  テキストにデータを追加したり読み込んだりする  
//!
//! @author 橋本奉武
//!
//! @date   1月9日
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTimeAndJuni : MonoBehaviour
{
    // プレイヤーがステージクリアしたか確認できるオブジェクト
    [SerializeField]
    private GameObject ClearObject = default;
    // スクリプト：プレイヤーがステージクリアしたか
    private ClearManagement Script_ClearManagement;

    // ステージで取得したタイムスコアを調べられるオブジェクト
    [SerializeField]
    private GameObject TimeScoreObj;
    // スクリプト：ステージで取得したタイムスコア数
    private ScoreManager Script_ScoreManager;

    // テキスト：タイムスコア
    [SerializeField] 
    private Text TimeScore;
    // テキスト：順位
    [SerializeField]
    private Text Juni;
    // テキスト：順位総合
    [SerializeField]
    private Text JuniTotal;

    // ゲームオーバーになったら非表示したいもの
    [SerializeField]
    private GameObject[] NoActiveByGameOver;

    // 

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // スクリプト：プレイヤーがステージクリアしたか　の取得
        Script_ClearManagement = ClearObject.GetComponent<ClearManagement>();

        // スクリプト：ステージで取得したタイムスコア数 の取得
        Script_ScoreManager = TimeScoreObj.GetComponent<ScoreManager>();

        // ===================================================================

        // ステージクリアした場合
        if (Script_ClearManagement.IsPlayerClear)
        {
            Debug.Log("クリア");

            // 

        }
        else
        // ゲームオーバーの場合
        {
            Debug.Log("ゲームオーバー");

            // ゲームオーバーになったら非表示したいものを非表示する
            if(NoActiveByGameOver != null)
            {
                foreach (GameObject obj in NoActiveByGameOver) obj.SetActive(false);
            }

        }
    }
}
