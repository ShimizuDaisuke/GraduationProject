//=======================================================================================
//! @file   QRSpotObject
//! @brief  QRSpotに触れた時の処理
//! @author 志水大輔
//! @date   10/9
//! @note   書き換える可能性あり
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRSpotObject : MonoBehaviour
{
    //イベント管理クラス
    [SerializeField]
    private EventDirector _event = default;

    [SerializeField]
    private GameObject cameraDirector = default;

    // QRDirectorの変数
    GameObject qRDirector;

    // QRSpotの変数
    SampleQRReader spot;

    // プレイヤーのRigidbody
    Rigidbody rgb;

    //使用フラグ
    public bool m_useFlag = false;

    //このスクリプトがついてるオブジェクト
    [SerializeField]
    private GameObject m_thisObj = null;

    //=======================================================================================
    //! @brief 開始処理
    //! @param[in] なし
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    void Start()
    {
        // QRDirectorの参照
        qRDirector = GameObject.FindWithTag("QRDirector");
        // SampleQRReaderにアクセス
        spot = qRDirector.gameObject.GetComponent<SampleQRReader>();
        // プレイヤーのRigidbody
        rgb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    //=======================================================================================
    //! @brief 当たった時の処理
    //! @param[in] 当たり判定
    //! @param[out] なし
    //! @return なし
    //=======================================================================================
    void OnTriggerEnter(Collider col)
    {
        if(!m_thisObj.GetComponent<QRSpotObject>().UseFlag)
        {
            if (cameraDirector.GetComponent<CameraDirector>().IsMove2D3DCameraPos != true)
            {
                // Playerに当たったら
                if (col.gameObject.tag == "Player")
                {
                    qRDirector.GetComponent<QRReadID>().QRSpotObj = this.gameObject;

                    // プレイヤーのRigidBodyをフリーズさせる
                    rgb.constraints = RigidbodyConstraints.FreezeAll;

                    // カメラをオンにするフラグをtrueにする
                    spot.QRSpot = true;

                    // イベントを設定する
                    _event.IsEventKIND = EventDirector.EventKIND.RULE_QR;
                }
            }
        }
    }

    public bool UseFlag { get { return m_useFlag; } set { m_useFlag = value; } }
}
