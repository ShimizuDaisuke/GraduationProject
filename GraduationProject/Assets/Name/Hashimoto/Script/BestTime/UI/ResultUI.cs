//=======================================================================================
//! @file   ResultJuiUI.cs
//!
//! @brief  リザルトシーンに写るのUIテキストによる元
//!
//! @author 橋本奉武
//!
//! @date   1月22日
//!
//! @note  「abstract」:純粋仮想関数を使いたいときに使う
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ResultUI : MonoBehaviour
{
    /// <summary>
    /// テキストに文字を書く
    /// </summary>
    /// <param name="moji">文字</param>
    /// <param name="textobj">テキスト</param>
    public virtual void Write(string moji,Text textobj)
    {
        // テキストに文字を書く
        textobj.text = moji;
    }

    // UIを非表示させる
    public abstract void NoActive();
}
