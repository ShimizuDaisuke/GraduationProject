//===============================================
//! @file       HPGaugeController
//! @brief      カバーのHPゲージの処理
//! @author     長尾昌輝
//! @date       2019/10/10
//! @note       sliderを使う方法はダメ
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGaugeCover : MonoBehaviour
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

    //カバーの変数
    private GameObject CoverOfEraser;

    // カバーのスクリプト
    private CoverOfEraser Script_CoverOfEraser;

    // Start is called before the first frame update
    void Start()
    {
        // カバーを探す
        CoverOfEraser = GameObject.FindGameObjectWithTag("EraserDustCover");

        // カバーのスクリプトを取得
        Script_CoverOfEraser = CoverOfEraser.GetComponent<CoverOfEraser>();

        // 「RectTransform」というクラスを取得
        imageRectSize = GetComponent<RectTransform>();

        // 画像の位置を取得
        basePos = imageRectSize.localPosition;

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
        // カバーの最大体力
        int MaxHP = Script_CoverOfEraser.EraserMaxHP;

        // カバーの現在の体力
        int NowHP = Script_CoverOfEraser.EraserHP;

        // 現在のカバーのHPの割合
        float rate = (float)NowHP / (float)MaxHP;

        // HPの割合を元に画像サイズを変更
        nowSize.x = rate * baseSize.x;

        // カバーの体力によって、画像サイズを変更する
        imageRectSize.sizeDelta = nowSize;

        // カバーの体力によって、画像の位置をずらす
        imageRectSize.localPosition = new Vector2(basePos.x - baseSize.x / 2 + imageRectSize.sizeDelta.x / 2, basePos.y);



    }
}
