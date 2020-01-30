//=======================================================================================
//! @file   SelectTitle.cs
//!
//! @brief  セレクトシーンのタイトル名「ステージ選択」を上下に動かす
//!
//! @author 橋本奉武
//!
//! @date   10月10日
//!
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTitle : MonoBehaviour
{
    // 画像
    [SerializeField]
    private Image image = default;

    // その画像による「RectTransform」
    private RectTransform recttransform;

    // UIがY軸方向に動く速度
    private float vell = 0.05f;

    // UIがY軸方向に動ける範囲
    private float movearea = 0.5f;

    // UIがY軸方向に動く基準点
    private float basepos = 0.0f;
       
   /// <summary>
   /// 開始処理
   /// </summary>
    void Start()
    {
        // 画像がない場合
        if(image == null)
        {
            // このオブジェクトを画像にする
            image = gameObject.GetComponent<Image>();

            // このオブジェクトに画像がない場合、処理を飛ばす
            if (image == null) return;

        }

        // その画像による「RectTransform」の取得
        recttransform = image.GetComponent<RectTransform>();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 画像がない場合、処理を飛ばす
        if (image == null) return;

        // 基準点を動かす
        basepos += vell;

        // 基準点が範囲より高い場合
        if(basepos > movearea)
        {
            // 速度を反転する
            vell *= -1;

            // 範囲を制限する
            basepos = movearea;
        }
        else
        // 基準点が範囲より低い場合
        if (basepos < -movearea)
        {
            // 速度を反転する
            vell *= -1;

            // 範囲を制限する
            basepos = -movearea;
        }


        // 現在の画像の位置
        Vector2 imagpos = recttransform.localPosition;

        // 実際に画像を動かす
        recttransform.localPosition = new Vector2(imagpos.x, imagpos.y + basepos);


    } 
}
