using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Domin : MonoBehaviour
{
    //イベント管理クラス
    [SerializeField]
    private EventDirector m_event = default;

    // プレイヤー
    [SerializeField]
    private GameObject Player = default;

    //当たったフラグ
    [SerializeField]
    private bool m_hitFlag;

    //プレイヤーを固定する位置
    [SerializeField]
    private Vector3 m_stopPos = new Vector3(0.0f,0.0f,0.0f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((m_hitFlag == true)&& (m_event.IsEventKIND == EventDirector.EventKIND.RULE_DOMINO))
        {
            //プレイヤーとカバーを同じ位置にする
            Player.gameObject.transform.position = m_stopPos;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if ((collider.gameObject.tag == "Player") && (m_hitFlag == false))
        {
            m_hitFlag = true;
            //ドミノ倒しのイベントに設定
            m_event.IsEventKIND = EventDirector.EventKIND.RULE_DOMINO;
        }
    }


    public bool HitFlag { get { return m_hitFlag; } set { m_hitFlag = value; } }
}
