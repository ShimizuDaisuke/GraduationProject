//=======================================================================================
//! @file   CreateElectricity.cs
//! @brief  電気の生成処理
//! @author 田中歩夢
//! @date   12月13日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//電気の生成処理クラス
public class CreateElectricity : MonoBehaviour
{
    //回路クラス
    [SerializeField]
    private CircuitController m_circuitCon = default;

    //電気のプレハブ
    [SerializeField]
    private GameObject m_electricityPrehab = default;

    //遅らせ
    const float CREATE_DELAY_TIME = 0.2f;

    //生成までのカウント
    private float m_createDelayCount = 0.0f;

    //生成できるかどうかのフラグ
    private bool m_createFlag = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //回路がつながっているか
        if(m_circuitCon.ConnectFlag)
        {

            //生成できるか
            if (!m_createFlag)
            {
                Create(transform.position);
                m_createFlag = true;
            }
            
        }

        //生成したあとか
        if(m_createFlag)
        {
            //生成までのカウントに達したか
            if (m_createDelayCount <= CREATE_DELAY_TIME)
            {
                m_createDelayCount += Time.deltaTime;
            }
            else
            {
                m_createFlag = false;
                m_createDelayCount = 0.0f;
            }
                
        }


    }

    /// <summary>
    /// 生成処理
    /// </summary>
    /// <param name="pos">生成する座標</param>
    public void Create(Vector3 pos)
    {
        //生成
        GameObject obj = Instantiate(m_electricityPrehab, pos, Quaternion.identity);

        //回路クラスを設定する
        obj.GetComponent<ElectricityController>().IsCircuitController = m_circuitCon;
    }

}
