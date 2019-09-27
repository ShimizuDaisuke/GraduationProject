//=======================================================================================
//! @file   Player.cs
//! @brief  プレイヤーの処理
//! @author 田中歩夢
//! @date   9月27日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤークラス
public class Player : MonoBehaviour
{
    //ジェイスティック
    [SerializeField]
    private Joystick m_joystick = null;
    //速度
    [SerializeField]
    private float m_vel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ジョイスティックで動かした方向
        float dx = m_joystick.Horizontal;
        float dy = m_joystick.Vertical;

        //移動
        transform.Translate(dx * m_vel, 0.0f, dy * m_vel);
    }

    
}
