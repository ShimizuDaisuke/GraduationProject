//=======================================================================================
//! @file   CutterParent.cs
//! @brief  カッターの親変更の処理
//! @author 田中歩夢
//! @date   01月15日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カッターの親変更のクラス
public class CutterParent : MonoBehaviour
{
    //カッターが箱に当たったかフラグ
    [SerializeField]
    private CutterHitFlag m_cutterHitFlag = default;

    //リジットボディ
    private Rigidbody m_rigd = default;

    // Start is called before the first frame update
    void Start()
    {
        m_rigd = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_cutterHitFlag.HitFlag)
        {
            //回転をすべて固定
            m_rigd.constraints = RigidbodyConstraints.FreezeRotation; 
        }
        else
        {
            //回転をZだけ解除
            m_rigd.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            //transform.parent = null;
        }


    }


    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.layer == 12)
        {
            transform.parent = null;
            transform.parent = collider.transform;
        }
    }

}
