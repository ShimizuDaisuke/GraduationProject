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

    //速度X
    [SerializeField]
    private float m_speedX = 0.1f;

    //速度Z
    [SerializeField]
    private float m_speedZ = 0.01f;

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
            
            //Z軸を調整する
            if(m_player.transform.position.z > transform.position.z)
            {
                m_player.transform.position = new Vector3(m_player.transform.position.x + m_speedX, m_player.transform.position.y, m_player.transform.position.z - m_speedZ);

            }
            else if (m_player.transform.position.z < transform.position.z)
            {
                
                m_player.transform.position = new Vector3(m_player.transform.position.x + m_speedX, m_player.transform.position.y, m_player.transform.position.z + m_speedZ);

            }
            else
            {
                m_player.transform.position = new Vector3(m_player.transform.position.x + m_speedX, m_player.transform.position.y, m_player.transform.position.z);

            }

        }
    }

}
