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
    private Quaternion m_startRot = Quaternion.identity;

    //投げたフラグ
    private bool m_throwFlag = false;

    //フリーズさせておく座標
    [SerializeField]
    private GameObject m_freezPos = null;

    //プレイヤーのゲームオブジェクト
    private GameObject m_player = null;

    //直進移動クラス
    [SerializeField]
    private GameObject m_hitStraightObj = null;

    HitStraight m_hitStraight = default;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーを探す
        m_player = GameObject.FindGameObjectWithTag("Player");

        //最初のZの角度を保存
        m_startRot = transform.rotation;

        m_hitStraight = m_hitStraightObj.GetComponent<HitStraight>();

        m_throwFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        //フリーズ座標を超えていなかったらフリーズ、越えたらフリーズ解除
        if(m_event.IsEventKIND == EventDirector.EventKIND.RULE_MOVE_STRAIGHT)
        {
            if (m_player.transform.position.x < m_freezPos.transform.position.x)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }
            else
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
            }
        }

        if (m_event.IsEventKIND == EventDirector.EventKIND.RULE_MOVE_STRAIGHT)
        {
            //m_hitStraight.MoveZ();
            m_hitStraight.MoveStraight();
        }

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
            if (transform.rotation.x <= m_startRot.x)
            {
                transform.Rotate(new Vector3(transform.rotation.x + m_retunBoardPower, transform.rotation.y, transform.rotation.z));
            }
            else
            {

                m_throwFlag = false;
            }       
        }
        else
        {
            //プレイヤーが直進移動以外の時は
            if (m_event.IsEventKIND != EventDirector.EventKIND.RULE_THOW && m_event.IsEventKIND != EventDirector.EventKIND.RULE_MOVE_STRAIGHT)
            {
                //最初の角度に固定
                transform.rotation = m_startRot;
            }
        }

    }

    //投げたかどうかのフラグを設定・取得
    public bool ThrowFlag { get { return m_throwFlag; } set { m_throwFlag = value; } }

}
