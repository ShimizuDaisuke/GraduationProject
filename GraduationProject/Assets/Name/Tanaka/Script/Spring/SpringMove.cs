//=======================================================================================
//! @file   SpringMove.cs
//! @brief  バネの動き
//! @author 田中歩夢
//! @date   11月29日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//バネの動き
public class SpringMove : MonoBehaviour
{
   
    //レイヤーマスク（default）
    [SerializeField]
    private LayerMask m_layerMaskDefault = default;

    //ジャンプ力
    [SerializeField]
    private float m_jumpPower = 400.0f;

    //レイの飛ばす距離
    [SerializeField]
    private float m_distance = 0.5f;

    //フラグ
    private bool m_flag = false;

    // プレイヤーのRigidbody
    private Rigidbody m_rigidbody = default;

    //ジャンプ
    private float m_jump = 0.0f;

    //１回フラグ
    private bool m_oneFlag = false;

    //レイの作成
    private Ray m_ray = default;

    // Rayが衝突したコライダーの情報
    private RaycastHit m_hit = default;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーのRigidbodyを探す
        m_rigidbody = transform.GetComponent<Rigidbody>();

        
    }

    // Update is called once per frame
    void Update()
    {
        //レイの作成
        m_ray = new Ray(transform.position, Vector3.down);

        //レイと地面？が接触した
        if (Physics.Raycast(m_ray, out m_hit, m_distance, m_layerMaskDefault))
        {
            m_flag = true;
        } 
        else
        {
            m_flag = false;
            m_oneFlag = false;

        }

        //地面についた
        if (m_flag)
        {
            if(!m_oneFlag)
            {
                Debug.Log("fbagfafiasnfias");
                
                m_rigidbody.AddForce(m_jumpPower * Vector3.up, ForceMode.Force);
                m_oneFlag = true;
            }   
        }   

    }
}
