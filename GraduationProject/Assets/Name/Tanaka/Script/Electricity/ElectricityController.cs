//=======================================================================================
//! @file   ElectricityController.cs
//! @brief  電気の動き
//! @author 田中歩夢
//! @date   12月13日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//電気の動き
public class ElectricityController : MonoBehaviour
{
    //向き
    private Vector3 m_direction = Vector3.zero;

    //速度
    private float m_speed = 0.1f;

    //回路クラス
    private CircuitController m_circuiCon = default;

    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = new Vector3(transform.position.x + (m_speed * m_direction.x), transform.position.y + (m_speed * m_direction.y), transform.position.z + (m_speed * m_direction.z));

        //つながってなかったら削除
        if (!m_circuiCon.ConnectFlag)
        {
            Destroy(gameObject);
        }

    }


    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        //電気が当たったら
        if (collider.gameObject.tag == "ChangeDirection")
        {
            m_direction = collider.GetComponent<HitChangeDirection>().Direction;

        }

        if(collider.gameObject.tag == "Battery")
        {
            Destroy(gameObject);
        }

    }

    //向きの取得・設定
    public CircuitController IsCircuitController { get { return m_circuiCon; } set { m_circuiCon = value; } }

}
