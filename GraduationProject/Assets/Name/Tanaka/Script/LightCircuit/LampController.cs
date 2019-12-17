//=======================================================================================
//! @file   CircuitController.cs
//! @brief  ライトの回路の動き
//! @author 田中歩夢
//! @date   12月04日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController : MonoBehaviour
{
    //ライト
    [SerializeField]
    private Light[] m_lamp = null;

    //明るさ
    [SerializeField]
    private float m_brightness = 10.0f;

    //モータークラス
    [SerializeField]
    private MotorController m_motorCon = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //回路がつながってる時
        if (m_motorCon.MotorConConnectFlag)
        {
            for(int i = 0; i < m_lamp.Length;i++)
            {
                //明かりをつける
                m_lamp[i].intensity = m_brightness;
            }
            
        }
        else
        {
            for (int i = 0; i < m_lamp.Length; i++)
            {
                //明かりを消す
                m_lamp[i].intensity = 0.0f;
            }
        }
    }
}
