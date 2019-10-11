using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dominokilling : MonoBehaviour
{
    //イベント管理クラス
    [SerializeField]
    private EventDirector m_event = default;

    //当たったフラグ
    private bool m_hitFlag;

    private Rigidbody m_rigidbody = null;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

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
        if (collider.gameObject.tag == "Player")
        {
            //プレイヤーを投げる

            m_hitFlag = true;
            //投げられたイベントにする
            m_event.IsEventKIND = EventDirector.EventKIND.Thow;
        }
    }

    [SerializeField]
    private float m_torqueForce = 0.0f;

    private void ControlObject()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
           // SetDominoParameter(this);
            Topple(transform.forward);
        }
    }

    private void Topple(Vector3 i_forward)
    {
        // 進行方向に対してX軸で回転させたいのでrightベクトルを使用して回転ベクトルを作る。
        Vector3 rightVec = transform.right;
        Vector3 torque = rightVec * m_torqueForce;

        m_rigidbody.AddTorque(torque, ForceMode.Force);
    }
}
