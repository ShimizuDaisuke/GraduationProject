//=======================================================================================
//! @file   HitChangeDirection.cs
//! @brief  当たったら向きを変えるの処理
//! @author 田中歩夢
//! @date   12月13日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//当たったら向きを変えるの処理
public class HitChangeDirection : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //向きの取得・設定
    public Vector3 Direction { get { return m_direction; } set { m_direction = value; } }
}
