//=======================================================================================
//! @file   HitStraight.cs
//! @brief  当たったら直進移動の処理
//! @author 田中歩夢
//! @date   10月11日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//当たったら直進移動のクラス
public class HitStraight : MonoBehaviour
{
    //イベントクラス
    [SerializeField]
    private EventDirector m_event;
    
    //プレイヤーのゲームオブジェクト
    private GameObject m_player = null;

    //速度
    [SerializeField]
    private float m_speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //直進移動
        MoveStraight();
    }

    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            //プレイヤーのオブジェクトを取得
            m_player = collider.gameObject;
            //直進移動のイベントに設定
            m_event.IsEventKIND = EventDirector.EventKIND.RULE_MOVE_STRAIGHT;
        }
    }

    //直進移動
    private void MoveStraight()
    {
        if(m_event.IsEventKIND == EventDirector.EventKIND.RULE_MOVE_STRAIGHT)
        {
            m_player.transform.position = new Vector3(m_player.transform.position.x + m_speed, m_player.transform.position.y, m_player.transform.position.z);
        }
    }

}
