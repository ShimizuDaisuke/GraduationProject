using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    //イベント管理クラス
    [SerializeField]
    private EventDirector m_event = default;

    //弾の角度
    [SerializeField]
    private Vector3 angle = new Vector3(0, 0, 0);

    //弾の速さ
    [SerializeField]
    private float speed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        var direction = Quaternion.Euler(angle) * Vector3.forward;

        //ドミノ倒しのイベントなら
        if (m_event.IsEventKIND == EventDirector.EventKIND.RULE_DOMINO)
        {
            //弾の移動
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
