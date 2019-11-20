//======================================================================================= 
//! @file       BookMove.cs 
//! @brief      何もない時はrigidbodyをフリーズさせる
//! @author     長尾昌輝 
//! @date       2019/10/31
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BookMove : MonoBehaviour
{
    //イベント管理クラス
    [SerializeField]
    private EventDirector m_event = default;

    //当たった判定
    private bool m_hitFlag = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // rigidbodyを取得
        Rigidbody rigidbody = GetComponent<Rigidbody>();


        //イベントがドミノ倒しの時
        if (m_event.IsEventKIND == EventDirector.EventKIND.RULE_DOMINO)
        {
            //rigidbodyのフリーズを解除する
            rigidbody.constraints = RigidbodyConstraints.None;
        }
        else
        {
           // 回転、位置ともに固定
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;

            m_hitFlag = false;
        }
    }

    //他のオブジェクトに当たったら
    void OnCollisionEnter(Collision collision)
    {
        //イベントがドミノ倒しの時
        if ((m_event.IsEventKIND == EventDirector.EventKIND.RULE_DOMINO)&&(m_hitFlag == false))
        {
            //SEの再生
            SoundManager.PlaySE(SoundManager.Sound.SE_DominoHit);

            m_hitFlag = true;
        }
    }

}
