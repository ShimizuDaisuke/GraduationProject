//=======================================================================================
//! @file   GoolPosMovePlayer.cs
//! @brief  当たったらゴール座標に移動の処理
//! @author 田中歩夢
//! @date   10月11日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//当たったらゴール座標に移動のクラス
public class GoolPosMovePlayer : MonoBehaviour
{
    //イベントクラス
    [SerializeField]
    private EventDirector m_event;

    //ゴール座標のオブジェクト
    [SerializeField]
    private GameObject m_goolPosObj;

    //プレイヤーのゲームオブジェクト
    private GameObject m_player = null;

    //ゴールに入ったか
    private bool m_goolInFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーを探す
        m_player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        //ターゲット座標に移動
        MoveTarget();
    }

    //衝突判定
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //ゴールに入った
            m_goolInFlag = true;
            Debug.Log(m_goolInFlag);
        }
    }


    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (m_event.IsEventKIND == EventDirector.EventKIND.NONE)
            {
                //直進移動のイベントに設定
                m_event.IsEventKIND = EventDirector.EventKIND.PENCILCASE_MOVE_GOOL;
            }
        }
    }


    //ターゲット座標に移動
    private void MoveTarget()
    {
        if(m_event.IsEventKIND == EventDirector.EventKIND.PENCILCASE_MOVE_GOOL)
        {
            if(!m_goolInFlag)
            {
                //距離を求める
                Vector3 targetPos = m_goolPosObj.transform.position - m_player.transform.position;
                //方向を求める
                targetPos.Normalize();

                //移動する
                m_player.GetComponent<PlayerController>().MoveByDirection(new Vector3(targetPos.x, targetPos.y, targetPos.z));
            }
        }
    }

    //ゴールに入ったか取得・設定
    public bool GoolINFlag { get { return m_goolInFlag; } set { m_goolInFlag = value; } }


}
