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
    // ゲームオーバーで、ずらしたい時間UIの位置
    const float TIMEUIPOS_MOVE = -100.0f;

    // プレイヤーがステージクリアしたか確認できるオブジェクト
    [SerializeField]
    private GameObject ClearObject = default;
    // スクリプト：プレイヤーがステージクリアしたか
    private ClearManagement Script_ClearManagement;

    // ステージで取得したタイムスコアを調べられるオブジェクト
    [SerializeField]
    private GameObject TimeScoreObj = default;
    // スクリプト：ステージで取得したタイムスコア数
    private TimeManager Script_ScoreManager;

    // テキスト：タイムスコア
    [SerializeField] 
    private Text Text_TimeScore;
    // テキスト：順位
    [SerializeField]
    private Text Text_Juni;
    // テキスト：順位総合
    [SerializeField]
    private Text Text_JuniTotal;

    // テキスト：時間(全体)
    [SerializeField]
    private GameObject All_Time;
    // テキスト：順位(全体)
    [SerializeField]
    private GameObject All_Jyui;

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

        // スクリプト：テキストにデータを追加したり読み込んだりする  の取得
        Script_FinalTimeWriteLoad = GetComponent<FinalTimeWriteLoad>();

    // ===================================================================

        // ゲームを始めてからかかった時間
        int time = Script_ScoreManager.ClearTime;
        
        // その時間を実際に反映させる
        Text_TimeScore.text = time.ToString()+" 秒";

        // ステージクリアした場合
        if (Script_ClearManagement.IsPlayerClear)
        {
            // そのタイムスコアの順位
            int jyui = 0;
            // 順位総合
            int jyuitotal = 0;
            // ベストタイムスコア
            int besttimescore = 0;

            // 順位決めする
            Script_FinalTimeWriteLoad.DecideJuni(ref jyui,ref jyuitotal,ref besttimescore, time, Script_ClearManagement.PlayingStageName);

            // その順位を反映させる
            Text_Juni.text = jyui.ToString() + " 位";
            // その順位総合を反映させる
            Text_JuniTotal.text = "/ " + jyuitotal.ToString() +" 位中";

        }
        else
        // ゲームオーバーの場合
        {
            // ゲームオーバーになったら、全体的に順位を非表示する
            All_Jyui.SetActive(false);

            // 全体的に時間のテキスト(全体)の位置をずらす
            All_Time.GetComponent<RectTransform>().transform.localPosition += new Vector3(0.0f, TIMEUIPOS_MOVE, 0.0f);

        }
    }
}
