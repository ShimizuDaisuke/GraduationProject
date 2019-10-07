//===============================================
//! @file       score
//! @brief      スコアUI
//! @author     服部晃大
//! @date       10/7
//! @note       まずはスコアから
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//テキストを使うため宣言
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    //スコアを表示する
    public Text scoreText;

    //スコアの変数
    private int eraserScore;

    //スコアの桁数
    string remainingScore = "D6";

    // Start is called before the first frame update
    void Start()
    {
        //スコアの初期化
        eraserScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //文字列にしてからテキストに表示
        scoreText.text = eraserScore.ToString(remainingScore);
    }

    /// <summary>
    /// 取得・設定関数
    /// </summary>
    // スコア
    public int Int_EraserScore { get { return eraserScore; } private set { eraserScore = value; } }
}
