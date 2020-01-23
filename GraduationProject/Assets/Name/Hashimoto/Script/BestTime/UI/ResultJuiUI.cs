//=======================================================================================
//! @file   ResultJuiUI.cs
//!
//! @brief  リザルトシーンに写る順位のUI  
//!
//! @author 橋本奉武
//!
//! @date   1月22日
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultJuiUI : ResultUI
{
    // テキスト：順位
    [SerializeField]
    private Text Text_Juni = default;
    // テキスト：順位総合
    [SerializeField]
    private Text Text_JuniTotal = default;

    // テキスト：順位(全体)
    [SerializeField]
    private GameObject All_Jyui = default;

    /// <summary>
    ///　独自による開始処理
    public override void OriginalStart()
    {
        // 継承先のクラスにある変数をリンクさせる
        AllTextObj = All_Jyui;
    }

    /// <summary>
    /// 順位を書く
    /// </summary>
    /// <param name="yourjuni">現在の順位</param>
    /// <param name="junitotal">順位総合</param>
    public void Write(int yourjyuni,int jyunitotal)
    {
        // その順位を反映させる
        Text_Juni.text = yourjyuni.ToString() + " 位";
        // その順位総合を反映させる
        Text_JuniTotal.text = "/ " + jyunitotal.ToString() + " 位中";
    }
}
