//======================================================================================= 
//! @file       CutterKnife.cs
//! @brief      カッターナイフのイベント
//! @author     長尾昌輝
//! @date       2019/10/25
//======================================================================================= 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterKnife : MonoBehaviour
{
    //イベント管理クラス
    [SerializeField]
    private EventDirector m_event = default;

    // プレイヤー
    [SerializeField]
    private GameObject Player = default;

    //カッターナイフのカメラの処理
    [SerializeField]
    private EventCameraCutterKnife Script_EventCameraCutterKnife = default;

    // カメラ
    [SerializeField]
    private GameObject CameraDirector = default;

    //当たった判定
    [SerializeField]
    private bool m_hitFlag;

    //収納する速さ
    [SerializeField]
    private float m_speed = 0.0f;

    //現在の時間
    private float time;

    //何秒間イベントを行うか
    [SerializeField]
    private float m_eventTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Script_EventCameraCutterKnife = CameraDirector.GetComponent<EventCameraCutterKnife>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((m_event.IsEventKIND == EventDirector.EventKIND.RULE_CUTTERKNIFE) && (m_hitFlag == true)&&(Script_EventCameraCutterKnife.MoveFlag == true))
        {
            var direction =  Vector3.forward;

            //カウントを足す
            time += Time.deltaTime;

            //モデルの移動
            this.transform.position += direction * m_speed * Time.deltaTime;

            Player.transform.position += direction * m_speed * Time.deltaTime;

            //イベントを行う時間を過ぎたら
            if (m_eventTime <= time)
            {
                //イベント終了
                m_event.IsEventKIND = EventDirector.EventKIND.NONE;

                //当たり判定の終了
                m_hitFlag = false;

                Script_EventCameraCutterKnife.MoveFlag = false;
                //time = 0;
            }
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        //プレイヤーと当たったら
        if ((collider.gameObject.tag == "Player") && (m_hitFlag == false) && (m_eventTime > time))
        {
            m_hitFlag = true;
            //ドミノ倒しのイベントに設定
            m_event.IsEventKIND = EventDirector.EventKIND.RULE_CUTTERKNIFE;
        }
    }


  

}
