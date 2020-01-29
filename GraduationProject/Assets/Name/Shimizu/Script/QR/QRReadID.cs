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
        PLAYER_ERASER = 1 << 3,    // プレイヤーの変更(消しゴム)    0000 1000  8
        PLAYER_IRON   = 1 << 4,    // プレイヤーの変更(鉄)       　 0001 0000  16
        SEESAW_CHANG_RULER = 1 << 5,    //シーソーを定規に変更      0010 0000  32
        CRAYON_NO_BRAEK = 1 << 6,  // クレヨンが壊れなくなる        0100 0000  64
        HURIKO_STOP = 1 << 7,      // 振り子停止                    1000 0000  128
        PLAYER_SPEED_UP = 1 << 8,  // プレイヤーの速度UP            0001 0000 0000  256
        TIMESTOP_5 = 1 << 9,       //５秒時間停止
        TIMESTOP_10 = 1 << 10,     //１０秒時間停止
        NONE = 1<<11,
    }

    // 読み込んだQRの結果の格納
    private string result = "";

    // 読み込んだQRの結果
    private SampleQRReader qrResult = default;

    // テキスト変更の関数を呼ぶ変数
    QRText qRText = default;

    // イメージ変更の関数を呼ぶ変数
    QRImage qRImage = default;

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

    //QR時間停止
    [SerializeField]
    private QRTimeStop m_timeStop = default;

    //プレイヤーの変更
    [SerializeField]
    private PlayerChange m_playerChange = default;

    //QRスポット
    private GameObject m_hitQRObject = default;

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
        // QRImageにアクセス
        qRImage = GetComponent<QRImage>();

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

        // 何も読み込めなかった場合、今後の処理を飛ばす
        if (result == null) return;

        // 文字列を数値に変換
        int.TryParse(result, out num);

        // ID処理
        switch (num)
        {
            // プレイヤーの変更 (消しゴム)
            case (int)ReadResult.PLAYER_ERASER:
                m_playerType.IsPlayerType = PlayerType.Type.ERASER;
                m_playerChange.ChangeModel();
                qRText.PlayerEraser();
                qRImage.PlayerEraser_Image();
                m_hitQRObject.GetComponent<QRSpotObject>().UseFlag = true;

                //QRリセット
                QRReset();
                break;
            // プレイヤーの変更 (鉄)
            case (int)ReadResult.PLAYER_IRON:
                m_playerType.IsPlayerType = PlayerType.Type.IRON;
                m_playerChange.ChangeModel();
                qRText.PlayerIron();
                qRImage.PlayerIron_Image();
                m_hitQRObject.GetComponent<QRSpotObject>().UseFlag = true;

                //QRリセット
                QRReset();
                break;

            // シーソーを定規に変更
            case (int)ReadResult.SEESAW_CHANG_RULER:
                if (m_seesawChangeRuler != null)
                {
                    m_qrChangeRuler.ChangeRuler();
                    qRText.Seesaw_Chang_Ruler();
                    qRImage.Seesaw_Chang_Ruler_Image();
                    m_hitQRObject.GetComponent<QRSpotObject>().UseFlag = true;
                }
                else
                {
                    qRText.NOQR();
                    qRImage.NullQR_Image();
                    m_hitQRObject.GetComponent<QRSpotObject>().UseFlag = false;

                    //QRリセット
                    QRReset();
                }
                break;

            // クレヨンが壊れなくなる
            case (int)ReadResult.CRAYON_NO_BRAEK:
                if (m_crayonsChangeNoBrake != null)
                {
                    m_qrChangecrayons.ChangeRuler();
                    qRText.Crayon_No_Break();
                    qRImage.Crayon_No_Break_Image();
                    m_hitQRObject.GetComponent<QRSpotObject>().UseFlag = true;
                }
                else
                {
                    qRText.NOQR();
                    qRImage.NullQR_Image();
                    m_hitQRObject.GetComponent<QRSpotObject>().UseFlag = false;

                    //QRリセット
                    QRReset();
                }
                break;

            // 振り子停止
            case (int)ReadResult.HURIKO_STOP:
                if (m_hurikoStopFlag != null)
                {
                    m_hurikoStopFlag.StopFlag = true;
                    m_hitQRObject.GetComponent<QRSpotObject>().UseFlag = true;
                    qRText.Huriko_Stop();
                    qRImage.Huriko_Stop_Image();
                }
                else
                {
                    qRText.NOQR();
                    qRImage.NullQR_Image();

                    //QRリセット
                    QRReset();
                }
                    
                break;
                
            // プレイヤーの速度UP
            case (int)ReadResult.PLAYER_SPEED_UP:
                m_playerCon.ChangeVel();
                qRText.Player_Speed_Up();
                qRImage.Player_Speed_Up_Image();
                m_hitQRObject.GetComponent<QRSpotObject>().UseFlag = true;

                //QRリセット
                QRReset();
                break;

            //５秒時間停止
            case (int)ReadResult.TIMESTOP_5:
                qRImage.Stop_Timer_Image();
                m_timeStop.TimeStop = QRTimeStop.TIMESTOP_COUNT.STOP5;
                m_hitQRObject.GetComponent<QRSpotObject>().UseFlag = true;

                //QRリセット
                QRReset();
                break;

            //１０秒時間停止
            case (int)ReadResult.TIMESTOP_10:
                qRImage.Stop_Timer_Image();
                m_timeStop.TimeStop = QRTimeStop.TIMESTOP_COUNT.STOP10;
                m_hitQRObject.GetComponent<QRSpotObject>().UseFlag = true;

                //QRリセット
                QRReset();
                break;

            // この中の物に属さなかった場合正規のQRじゃない
            default:
                qRText.NOQR();
                qRImage.NullQR_Image();

                //QRリセット
                QRReset();
                break;
        }
    }

    //QRのリセット
    private void QRReset()
    {
        num = (int)ReadResult.NONE;
        result = null;
        qrResult.Result = null;
    }

    //=======================================================================================
    //! @brief numの取得関数
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    public int Num { get { return num; } private set { num = value; } }

    public GameObject QRSpotObj { get { return m_hitQRObject; } set { m_hitQRObject = value; } }
}