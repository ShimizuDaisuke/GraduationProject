using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoEnd : MonoBehaviour
{

    //イベント管理クラス
    [SerializeField]
    private EventDirector m_event = default;

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
        }
    }
}
