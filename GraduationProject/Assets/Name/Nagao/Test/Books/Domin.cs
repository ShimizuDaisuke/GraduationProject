using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Domin : MonoBehaviour
{
    //イベント管理クラス
    [SerializeField]
    private EventDirector m_event = default;

    //イベント中のカメラの移動
    [SerializeField]
    private EventCameraDomino m_eventDomino = default;


    //当たったフラグ
    private bool m_hitFlag;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if ((collider.gameObject.tag == "Player") && (m_hitFlag == false))
        {
            m_hitFlag = true;
            //ドミノ倒しのイベントに設定
            m_event.IsEventKIND = EventDirector.EventKIND.RULE_DOMINO;

            //移動開始
            m_eventDomino.MoveFlag = true;
        }
    }


    public bool HitFlag { get { return m_hitFlag; } set { m_hitFlag = value; } }
}
