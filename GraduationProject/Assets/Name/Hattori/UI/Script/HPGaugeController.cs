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

    // 画像の元の位置
    private Vector2 basePos;

    // 画像の今の位置
    private Vector2 nowPos;

    // 元の画像サイズ
    private Vector2 baseSize;

    // 現在の画像サイズ
    private Vector2 nowSize;

    //プレイヤーの変数
    private GameObject player;

    // プレイヤーのスクリプト
    private Player Script_Player;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーを探す
        player = GameObject.FindGameObjectWithTag("Player");

        // プレイヤーのスクリプトを取得
        Script_Player = player.GetComponent<Player>();

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
        // プレイヤーの最大体力
        int MaxHP = Script_Player.MaxHP;

        // プレイヤーの現在の体力
        int NowHP = Script_Player.HP;

        // 現在のプレイヤーのHPの割合
        float rate = (float)NowHP / (float)MaxHP;

        // HPの割合を元に画像サイズを変更
        nowSize.x = rate * baseSize.x;

        // HPが減った画像サイズ
        float remainsSize = baseSize.x - nowSize.x;

        // プレイヤーの体力によって、画像の位置をずらす
        imageRectSize.transform.position = new Vector2(basePos.x - ((float)remainsSize / 2.0f), basePos.y);

        // プレイヤーの体力によって、画像サイズを変更する
        imageRectSize.sizeDelta = nowSize;

        //Script_Player.HP--;
    }
}
