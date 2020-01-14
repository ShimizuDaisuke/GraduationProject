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
    private EventDirector m_event = default;

    //プレイヤーのゲームオブジェクト 
    private GameObject m_player = null;

    //シーソー板
    [SerializeField]
    private GameObject m_ruler = null;

    //速度X
    [SerializeField]
    private float m_speedX;

    //速度Z
    [SerializeField]
    private float m_speedZ;

    //直進フラグ
    private bool m_straightFlag = false;

    //直進するまでの時間
    private float m_time = 0.0f;

    //直進するまでの最大時間
    private const float MAX_TIME = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーを探す
        m_player = GameObject.FindGameObjectWithTag("Player");

        m_time = 0;
        m_straightFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (m_event.IsEventKIND == EventDirector.EventKIND.NONE)
            {
                
                //直進移動のイベントに設定
                m_event.IsEventKIND = EventDirector.EventKIND.RULE_MOVE_STRAIGHT;
            }
        }
    }


    //Z軸の移動
    public void MoveZ()
    {

        if (m_player.transform.position.z != m_ruler.transform.position.z)
        {
            Debug.Log(m_ruler.transform.position.z);
            //Z軸の移動
            Vector2 m_movePos = Vector2.MoveTowards(new Vector2(m_player.transform.position.x, m_player.transform.position.z), new Vector2(m_ruler.transform.position.x, m_ruler.transform.position.z), m_speedZ);
            m_player.transform.position = new Vector3(m_player.transform.position.x, m_player.transform.position.y, m_movePos.y);
            m_time += Time.deltaTime;
        }

        if (m_time > MAX_TIME)
        {
            //直進していいよ
            m_straightFlag = true;

        }
          
        
    }


    //直進移動
    public void MoveStraight()
    {
       
            
                
         m_player.transform.position = new Vector3(m_player.transform.position.x + m_speedX, m_player.transform.position.y, m_player.transform.position.z);

                
            
        
    }
}
