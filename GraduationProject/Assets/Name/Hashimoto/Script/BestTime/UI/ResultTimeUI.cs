//=======================================================================================
//! @file   ResultTimeUI.cs
//!
//! @brief  リザルトシーンに写る時間のUI  
//!
//! @author 橋本奉武
//!
//! @date   1月22日
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTimeUI : ResultUI
{
    // 時間の種類
    public enum TimeKind
    {
        ERR = -1,       // 例外
        CLEARTIME,      // ステージクリアするのに掛かった時間
        BESTTIME,       // ベストタイム
        MAX             // この列挙型の最大数
    }

    // ゲームを始めてから掛かった時間を小数第2位切り捨てる量
    private int Decimal2PlaceCuttingOff = 100;

    // テキスト：タイム(大文字で表示させる時間)
    [SerializeField]
    private Text Text_IntegraTimeScore = default;
    // テキスト：タイム(数ミリ秒)
    [SerializeField]
    private Text Text_DecimalTimeScore = default;

    // テキスト：時間(全体)
    [SerializeField]
    private GameObject All_Time = default;

    /// <summary>
    ///　独自による開始処理
    public override void OriginalStart()
    {
        // 継承先のクラスにある変数をリンクさせる
        AllTextObj = All_Time;
    }

    /// <summary>
    /// ステージクリアするまで掛かった時間を、小数第2位まで切り捨て100倍にする
    /// <例> 12.345 → 1234
    /// </summary>
    /// <param name="time">ステージクリアするまで掛かった時間</param>
    /// <returns></returns>
    public int OmitAndDouble(float ftime)
    {
        // ステージクリアするまで掛かった時間を、小数第2位まで切り捨て100倍にする
        int itime = (int)(ftime * Decimal2PlaceCuttingOff);

        return itime;
    }

    /// <summary>
    /// 時間を書く
    /// </summary>
    /// <param name="time">時間</param>
    public void Write(int time)
    {
        // 時間で整数となる部分(分や秒)と小数となる部分(数ミリ秒)に分ける
        int integraltime = time / Decimal2PlaceCuttingOff;
        int decimaltime = time % Decimal2PlaceCuttingOff;

        // その時間を実際に反映させる
        Text_IntegraTimeScore.text = integraltime.ToString("D2");       // 整数となる部分(分や秒)
        Text_DecimalTimeScore.text = "'" + decimaltime.ToString("D2");    // 小数となる部分(数ミリ秒)
    }
}
