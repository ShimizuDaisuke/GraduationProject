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
    private float m_speedX = 0.01f;

    //速度Z
    [SerializeField]
    private float m_speedZ = 0.01f;

    //直進フラグ
    private bool m_straightFlag;

    //直進するまでの時間
    private float m_time;

    //直進するまでの最大時間
    private const float MAX_TIME = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_time = 0;
        m_straightFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveZ();
        //直進移動
        MoveStraight();
    }

    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (m_event.IsEventKIND == EventDirector.EventKIND.NONE)
            {
                //プレイヤーのオブジェクトを取得
                m_player = collider.gameObject;
                //直進移動のイベントに設定
                m_event.IsEventKIND = EventDirector.EventKIND.RULE_MOVE_STRAIGHT;
            }
        }
    }


    //Z軸の移動
    private void MoveZ()
    {
        if (m_event.IsEventKIND == EventDirector.EventKIND.RULE_MOVE_STRAIGHT)
        {
            if (m_player.transform.position.z != transform.position.z)
            {
                //Z軸の移動
                Vector2 m_movePos = Vector2.MoveTowards(new Vector2(m_player.transform.position.x, m_player.transform.position.z), new Vector2(transform.position.x,transform.position.z), m_speedZ);
                m_player.transform.position = new Vector3(m_player.transform.position.x, m_player.transform.position.y, m_movePos.y);
                m_time += Time.deltaTime;
            }

            if(m_time > MAX_TIME)
            {
                //直進していいよ
                m_straightFlag = true;
                
            }
          
        }
    }


    //直進移動
    private void MoveStraight()
    {
        if (m_event.IsEventKIND == EventDirector.EventKIND.RULE_MOVE_STRAIGHT)
        {
            if(m_straightFlag)
            {
                
                m_player.transform.position = new Vector3(m_player.transform.position.x + m_speedX, m_player.transform.position.y, m_player.transform.position.z);

                
            }
        }
    }
}
