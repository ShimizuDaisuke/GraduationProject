//=======================================================================================
//! @file   SeesawController.cs
//! @brief  シーソーの処理
//! @author 田中歩夢
//! @date   10月10日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//シーソーのクラス
public class SeesawController : MonoBehaviour
{
    //ThrowingObjectクラス
    [SerializeField]
    private ThrowingObject m_throwingObj = default;

    //イベント管理クラス
    [SerializeField]
    private EventDirector m_event = default;

    //板を元に戻す力
    [SerializeField]
    private float m_retunBoardPower = 1.0f;

    //板の最初のZの角度
    private float m_startRotZ;

    //投げたフラグ
    private bool m_throwFlag;

    // Start is called before the first frame update
    void Start()
    {
        //最初のZの角度を保存
        m_startRotZ = transform.rotation.z;

        m_throwFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Zの角度を最初に戻す
        RotZ();
    }

    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            //プレイヤーを投げる
            m_throwingObj.ThrowingObj();
            m_throwFlag = true;
            //投げられたイベントにする
            m_event.IsEventKIND = EventDirector.EventKIND.RULE_THOW;
        }
    }

    //Zの角度を最初に戻す
    private void RotZ()
    {
        //投げた後かどうか
        if (m_throwFlag)
        {

            //最初の角度より小さいとき徐々に戻していく
            if (transform.rotation.z <= m_startRotZ)
            {
                transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + m_retunBoardPower));
            }
            else
            {

                m_throwFlag = false;
            }       
        }
        else
        {
            //最初の角度に固定
            transform.Rotate(new Vector3(0, 0, 0));
        }

    }

    //投げたかどうかのフラグを設定・取得
    public bool ThrowFlag { get { return m_throwFlag; } set { m_throwFlag = value; } }

}
