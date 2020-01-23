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

// 型名省略
using TimeKind = ResultTimeUI.TimeKind;
public class ResultTimeAndJuni : MonoBehaviour
{
    // ゲームオーバーで、ずらしたいベストタイムのUIの位置
    private const float BESTTIMEUIPOS_MOVE = -100.0f;

    // ゲームオーバーの時のベストタイムのUIの大きさ
    private Vector2 BESTTIMESIZE = Vector2.one;

    // プレイヤーがステージクリアしたか確認できるオブジェクト
    [SerializeField]
    private GameObject ClearObject = default;
    // スクリプト：プレイヤーがステージクリアしたか
    private ClearManagement Script_ClearManagement;

    // ステージで取得したタイムスコアを調べられるオブジェクト
    [SerializeField]
    private GameObject TimeScoreObj = default;
    // スクリプト：ステージで取得したタイムスコア
    private TimeManager Script_ScoreManager;

    // スクリプト： タイムスコアのUI
    private ResultTimeUI[] TimeScore;

    // スクリプト：順位のUI
    private ResultJuiUI Juni;

    // --------------------------------------------------------------

    // スクリプト：テキストにデータを追加したり読み込んだりする  
    private FinalTimeWriteLoad Script_FinalTimeWriteLoad;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {

        // スクリプト：プレイヤーがステージクリアしたか　の取得
        Script_ClearManagement = ClearObject.GetComponent<ClearManagement>();

        // スクリプト：ステージで取得したタイムスコア数 の取得
        Script_ScoreManager = TimeScoreObj.GetComponent<TimeManager>();

        // スクリプト：タイムスコア  の取得
        Script_FinalTimeWriteLoad = GetComponent<FinalTimeWriteLoad>();

        // スクリプト：ステージで取得したタイムスコアのUI の取得
        TimeScore = GetComponents<ResultTimeUI>();

        // スクリプト：順位のUI  の取得
        Juni = GetComponent<ResultJuiUI>();
        // ===================================================================

        // タイムの初期化
        TimeScore[(int)TimeKind.CLEARTIME].OriginalStart();

        //　ベストタイムの初期化
        TimeScore[(int)TimeKind.BESTTIME].OriginalStart();

        // 順位の初期化
        Juni.OriginalStart();

        // ===================================================================

        // ステージクリアした場合
        if (Script_ClearManagement.IsPlayerClear)
        {
            // そのタイムスコアの順位
            int jyui = 0;
            // 順位総合
            int jyuitotal = 0;
            // ベストタイムスコア
            int besttimescore = 0;

            // -------------------------------------------------------------------------------------------------------------------------

            // ステージクリアするまで掛かった時間を小数第2位まで略する
            int itime = TimeScore[(int)TimeKind.CLEARTIME].OmitAndDouble(Script_ScoreManager.ClearTime);

            // 順位決めする
            Script_FinalTimeWriteLoad.DecideJuni(ref jyui, ref jyuitotal, ref besttimescore, itime, Script_ClearManagement.PlayingStageName);

            // ステージクリアするまで掛かった時間を描画する
            TimeScore[(int)TimeKind.CLEARTIME].Write(itime);

            // ベストタイムを描画する
            TimeScore[(int)TimeKind.BESTTIME].Write(itime);

            // 順位を描画する
            Juni.Write(jyui, jyuitotal);
        }
        else
        // ゲームオーバーの場合
        {
            // ステージクリアするまで掛かった時間を非表示させる
            TimeScore[(int)TimeKind.CLEARTIME].NoActive();

            // 順位を非表示させる
            Juni.NoActive();

            // 全体的にベストタイムののテキスト(全体)の位置
            Vector2 besttimeuipos = TimeScore[(int)TimeKind.BESTTIME].GetTextUIPostion();
            // 全体的にベストタイムののテキスト(全体)の位置を変える
            TimeScore[(int)TimeKind.BESTTIME].Move(new Vector2(besttimeuipos.x, besttimeuipos.y + BESTTIMEUIPOS_MOVE));

            // 全体的にベストタイムのテキスト(全体)の大きさを変える
            TimeScore[(int)TimeKind.BESTTIME].Resize(BESTTIMESIZE);
        }
    }

    
}
