//=======================================================================================
//! @file   BoardIronSideController.cs
//! @brief  天秤の鉄側の当たり判定処理
//! @author 田中歩夢
//! @date   12月16日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//天秤の鉄側の当たり判定処理
public class BoardIronSideController : MonoBehaviour
{
    //ヒットフラグ
    private bool m_hitFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Iron")
        {
            m_hitFlag = true;
        }
    }

    //回路がつながったフラグの取得・設定
    public bool HitFlag { get { return m_hitFlag; } set { m_hitFlag = value; } }
}
