//=======================================================================================
//! @file   PlayerType.cs
//! @brief  プレイヤーの属性の処理
//! @author 田中歩夢
//! @date   11月28日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerType : MonoBehaviour
{
    //プレイヤーの属性
    public enum Type
    {
        ERASER,         //消しゴム
        IRON,           //鉄
        MARBLE,         //ビー玉
        MAX_TYPE,       // 最大数
    };

    //プレイヤーの属性
    [SerializeField]
    private Type m_playerType = Type.ERASER;

    //プレイヤーディレクター
    [SerializeField]
    private PlayerController m_playerCon = default;

    //カメラディレクター
    [SerializeField]
    private CameraDirector m_cameraDirector = default;

    //Rigidbody
    private Rigidbody m_rigid = default;

    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (m_playerType == Type.ERASER)
        {
            //if (m_cameraDirector.IsAppearCamera3D)
            //    m_rigid.constraints = RigidbodyConstraints.None;
            //else
            //    m_rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        }
        else if (m_playerType == Type.IRON)
        {
            //角度を固定
            transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
            ////回転しない
            //m_rigid.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    //プレイヤーの属性の取得・設定
    public Type IsPlayerType
    {
        get { return m_playerType; }
        set
        {
            m_playerType = value;
        }
    }

}
