//=======================================================================================
//! @file   ResultJuiUI.cs
//!
//! @brief  リザルトシーンに写るのUIテキストによる元
//!
//! @author 橋本奉武
//!
//! @date   1月22日
//!
//! @note  継承 ->「abstract」 派生->「override」 :純粋仮想関数を使いたいときに使う
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ResultUI : MonoBehaviour
{
    // テキスト(全体)というオブジェクト
    protected GameObject AllTextObj;

    // 独自による開始処理
    public abstract void OriginalStart();

    /// <summary>
    /// テキストに文字を書く
    /// </summary>
    /// <param name="moji">文字</param>
    public virtual void Write(string moji)
    {
        // オブジェクトが存在しない場合、何もしない
        if (AllTextObj == null) return;

        // テキスト
        Text textobj = AllTextObj.GetComponent<Text>();
        // テキストがない場合、何もしない
        if (textobj == null) return;

        // テキストに文字を書く
        textobj.text = moji;
    }

    /// <summary>
    /// UIを動かす
    /// </summary>
    /// <param name="pos">位置</param>
    public void Move(Vector2 pos)
    {
        // オブジェクトが存在しない場合、何もしない
        if (AllTextObj == null) return;

        // RectTransform
        RectTransform recttransform = AllTextObj.GetComponent<RectTransform>();
        // RectTransformがない場合、何もしない
        if (recttransform == null) return;

        // テキストの位置を変える
        recttransform.localPosition = pos;

    }

    /// <summary>
    /// UIのサイズを変える
    /// </summary>
    /// <param name="size">大きさ</param>
    public void Resize(Vector2 size)
    {
        // オブジェクトが存在しない場合、何もしない
        if (AllTextObj == null) return;

        // RectTransform
        RectTransform recttransform = AllTextObj.GetComponent<RectTransform>();
        // RectTransformがない場合、何もしない
        if (recttransform == null) return;

        // テキストのサイズを変える
        recttransform.localScale = size;
    }

    /// <summary>
    /// UIを非表示させる
    /// </summary>
    public void NoActive()
    {
        // オブジェクトが存在しない場合、何もしない
        if (AllTextObj == null) return;

        // 全体的に非表示させる
        AllTextObj.SetActive(false);
    }

    /// <summary>
    /// 手動でテキストUIの位置を取得する
    /// </summary>
    /// <returns>サイズ</returns>
    public Vector2 GetTextUIPostion()
    {
        // オブジェクトが存在しない場合、何もしない
        if (AllTextObj == null) return Vector2.zero;

        // RectTransform
        RectTransform recttransform = AllTextObj.GetComponent<RectTransform>();
        // RectTransformがない場合、何もしない
        if (recttransform == null) return Vector2.zero;

        // 位置を渡す
        return recttransform.localPosition;
    }

    /// <summary>
    /// 手動でテキストUIのサイズを取得する
    /// </summary>
    /// <returns>サイズ</returns>
    public Vector2 GetTextUISize()
    {
        // オブジェクトが存在しない場合、何もしない
        if (AllTextObj == null) return Vector2.zero;

        // RectTransform
        RectTransform recttransform = AllTextObj.GetComponent<RectTransform>();
        // RectTransformがない場合、何もしない
        if (recttransform == null) return Vector2.zero;

        // サイズを渡す
        return recttransform.localScale;
    }
}
