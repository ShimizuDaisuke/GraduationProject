using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    //イベント管理クラス
    //[SerializeField]
    //private EventDirector m_event = default;

    // ドミノ倒しのスクリプト
    [SerializeField]
    private Domin Script_Domin = default;

    //弾の角度
    [SerializeField]
    private Vector3 angle = new Vector3(0, 0, 0);

    //弾の速さ
    [SerializeField]
    private float speed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //
        //m_event = GameObject.FindGameObjectWithTag("Player");

        // プレイヤーのスクリプトを取得
        //Script_Domin = m_event.GetComponent<Domin>();
    }

    // Update is called once per frame
    void Update()
    { 
        //ドミノ倒しのイベントなら
        if (Script_Domin.HitFlag == true)
        {
            var direction = Quaternion.Euler(angle) * Vector3.forward;

            //弾の移動
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
