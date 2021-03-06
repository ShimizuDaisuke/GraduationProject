﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoEnd : MonoBehaviour
{
    // プレイヤーがカメラに入ってはいけない領域
    [SerializeField]
    private GameObject Camera2DNoArea = default;

    //イベント管理クラス
    [SerializeField]
    private EventDirector m_event = default;

    //イベント中のカメラの移動
    [SerializeField]
    private EventCameraDomino m_eventDomino = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void OnTriggerEnter(Collider collider)
    {
        if ((collider.gameObject.tag == "Ball"))
        {
            //ドミノ倒しのイベントに設定
            m_event.IsEventKIND = EventDirector.EventKIND.NONE;

            //移動終了
            m_eventDomino.MoveFlag = false;

            //弾の削除
            Destroy(collider.gameObject);

            // プレイヤーが2Dカメラに入ってはいけない領域を消す
            if(Camera2DNoArea != null)
            {
                Destroy(Camera2DNoArea);
            }

            Destroy(this.gameObject);
        }
    }
}
