//=======================================================================================
//! @file   QRReadID
//! @brief  ID別処理するクラス
//! @author 志水大輔
//! @date   10/4
//! @note   ID別使用、IDは二進数
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRReadID : MonoBehaviour
{
    // 読み込んだQRのID
    enum ReadResult
    {
        // 二進数       1   桁
        INCREASE_TIME = 1 << 0,    // 残りの時間が増える  0000 00001  1
        DEF_UP        = 1 << 1,    // カバーを付ける 　   0000 00010  2
        REMAINING_UP  = 1 << 2,    // 残機を増やす        0000 00100  4
    }

    // 読み込んだQRの結果の格納
    private string result;

    // 読み込んだQRの結果
    private SampleQRReader qrResult;

    // テキスト変更の関数を呼ぶ変数
    QRText qRText;

    //=======================================================================================
    //! @brief 開始処理
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    void Start()
    {
        // SampleQRReaderにアクセス
        qrResult = GetComponent<SampleQRReader>();
        // QRTextにアクセス
        qRText = GetComponent<QRText>();
    }

    //=======================================================================================
    //! @brief 更新処理
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    void Update()
    {
        // 読み込んだ結果の値をもらう
        result = qrResult.Result;
        int num = -1;

        // 文字列を数値に変換
        if (int.TryParse(result, out num))
        {
            // ID処理
            switch (num)
            {
                // 時間を増やす
                case (int)ReadResult.INCREASE_TIME:
                qRText.IncreaseTime();
                    break;
                // カバーをつける
                case (int)ReadResult.DEF_UP:
                qRText.DefenseUp();
                    break;
                // 残機アップ
                case (int)ReadResult.REMAINING_UP:
                    qRText.RemainingUp();
                    break;
            }
        }
    }
}