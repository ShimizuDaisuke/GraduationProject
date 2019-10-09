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
        INCREASE_TIME = 1 << 0 , // 残りの時間が増える  0000 00001
        ATK_UP        = 1 << 1,  // 攻撃力の上昇(仮)    0000 00010
        
    }

    // 読み込んだQRの結果の格納
    private string result;

    // 読み込んだQRの結果
    private SampleQRReader qrResult;

    //=======================================================================================
    //! @brief 開始処理
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    void Start()
    {
        qrResult = GetComponent<SampleQRReader>();   
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
        Debug.Log(qrResult.Result);


        int num = -1;

        //// nullチェック
        //if (result != null)
        //{
            // 文字列を数値に変換
            if (int.TryParse(result, out num))
            {
                // ID処理
                switch (num)
                {
                    case (int)ReadResult.INCREASE_TIME:
                        Debug.Log("制限時間を上げました");
                        break;
                    case (int)ReadResult.ATK_UP:
                        Debug.Log("攻撃力を上げました");
                        break;
                }
            }
    }
}
