//===============================================
//! @file       HPGaugeController
//! @brief      HPゲージの処理
//! @author     服部晃大
//! @date       10/7
//! @note       sliderを使う方法はダメ
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGaugeController : MonoBehaviour
{
    //イメージをいじくるための変数
    private RectTransform imageRectSize;
    
    // 元の画像サイズ
    [SerializeField] private Vector2 baseSize;

    // 現在の画像サイズ
    private Vector2 nowSize;

    // Start is called before the first frame update
    void Start()
    {
        // 現在のサイズの初期化
        nowSize = baseSize;

        imageRectSize = GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    { 
        // プレイヤーの体力によって、画像サイズを変更する
        imageRectSize.sizeDelta = nowSize;
    }
}
