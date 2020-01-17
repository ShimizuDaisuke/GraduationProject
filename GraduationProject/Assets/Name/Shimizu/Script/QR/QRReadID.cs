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
        INCREASE_TIME = 1 << 0,    // 残りの時間が増える            0000 0001  1
        DEF_UP        = 1 << 1,    // カバーを付ける 　             0000 0010  2
        REMAINING_UP  = 1 << 2,    // 残機を増やす                  0000 0100  4
        PLAYER_ERASER = 1 << 3,    // プレイヤーの変更(消しゴム)    0000 1000  8
        PLAYER_IRON   = 1 << 4,    // プレイヤーの変更(鉄)       　 0001 0000  16
        SEESAW_CHANG_RULER = 1 << 5,    //シーソーを定規に変更
        CRAYON_NO_BRAEK = 1 << 6,  // クレヨンが壊れなくなる
        HURIKO_STOP = 1 << 7,      // 振り子停止
        PLAYER_SPEED_UP = 1 << 8,  // プレイヤーの速度UP
    }

    // 読み込んだQRの結果の格納
    private string result = "";

    // 読み込んだQRの結果
    private SampleQRReader qrResult = default;

    // テキスト変更の関数を呼ぶ変数
    QRText qRText = default;

    // 変換先
    int num = -1;

    [SerializeField]
    private PlayerType m_playerType = default;

    //シーソーを定規に変更
    [SerializeField]
    private GameObject m_seesawChangeRuler = null;

    private QRChangeObjct m_qrChangeRuler = default;

    //シーソーを定規に変更
    [SerializeField]
    private GameObject m_crayonsChangeNoBrake = null;

    private QRChangeObjct m_qrChangecrayons = default;

    //振り子停止
    [SerializeField]
    private HurikoStopFlag m_hurikoStopFlag = default;

    //プレイヤーのController
    [SerializeField]
    private PlayerController m_playerCon = default;

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

        if (m_seesawChangeRuler != null)
        {
            m_qrChangeRuler = m_seesawChangeRuler.GetComponent<QRChangeObjct>();
        }

        if (m_crayonsChangeNoBrake != null)
        {
            m_qrChangecrayons = m_crayonsChangeNoBrake.GetComponent<QRChangeObjct>();
        }
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

        // 文字列を数値に変換
        int.TryParse(result, out num);

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
            // プレイヤーの変更 (消しゴム)
            case (int)ReadResult.PLAYER_ERASER:
                m_playerType.IsPlayerType = PlayerType.Type.ERASER;
                qRText.PlayerEraser();
                break;
            // プレイヤーの変更 (鉄)
            case (int)ReadResult.PLAYER_IRON:
                m_playerType.IsPlayerType = PlayerType.Type.IRON;
                qRText.PlayerIron();
                break;
            // シーソーを定規に変更
            case (int)ReadResult.SEESAW_CHANG_RULER:
                m_qrChangeRuler.ChangeRuler();
                break;
            // クレヨンが壊れなくなる
            case (int)ReadResult.CRAYON_NO_BRAEK:
                m_qrChangecrayons.ChangeRuler();
                break;
            // 振り子停止
            case (int)ReadResult.HURIKO_STOP:
                if (m_hurikoStopFlag != null)
                {
                    m_hurikoStopFlag.StopFlag = true;
                }
                break;
                
                // プレイヤーの速度UP
            case (int)ReadResult.PLAYER_SPEED_UP:
                m_playerCon.ChangeVel();
                break;
            // この中の物に属さなかった場合正規のQRじゃない
            default:
                num = 0;
                qRText.NOQR();
                break;
        }
    }

    //=======================================================================================
    //! @brief numの取得関数
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    public int Num { get { return num; } private set { num = value; } }
}