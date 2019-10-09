//===============================================
//! @file       ScoreGaugeController
//! @brief      スコアゲージの処理
//! @author     服部晃大
//! @date       10/8
//! @note       
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreGaugeController : MonoBehaviour
{
    //イメージをいじくるための変数
    private RectTransform imageRectSize;

    // 画像の元の位置
    private Vector2 basePos;

    // 画像の今の位置
    private Vector2 nowPos;

    // 元の画像サイズ
    private Vector2 baseSize;

    // 現在の画像サイズ
    private Vector2 nowSize;

    // Start is called before the first frame update
    void Start()
    {
        // 「RectTransform」というクラスを取得
        imageRectSize = GetComponent<RectTransform>();

        // 画像の位置を取得
        basePos = imageRectSize.position;

        //現在の位置の初期化
        nowPos = basePos;

        // 画像サイズを取得
        baseSize = imageRectSize.sizeDelta;

        // 現在のサイズの初期化
        nowSize = baseSize;
    }

    // Update is called once per frame
    void Update()
    {
        // 切り抜く

 


    }
}
