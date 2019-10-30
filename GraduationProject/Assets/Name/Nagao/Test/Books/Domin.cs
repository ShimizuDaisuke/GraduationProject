using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Domin : MonoBehaviour
{
    //イベント管理クラス
    [SerializeField]
    private EventDirector m_event = default;

    //当たったフラグ
    [SerializeField]
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
        }
    }


    public bool HitFlag { get { return m_hitFlag; } set { m_hitFlag = value; } }
}
