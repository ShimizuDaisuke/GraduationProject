//=======================================================================================
//! @file   ExclamationMark.cs
//! @brief  ！の処理
//! @author 田中歩夢
//! @date   10月07日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//！のクラス
public class ExclamationMark : MonoBehaviour
{
    //上下フラグ
    private bool m_upDownFlag;

    //最初の座標
    private Vector3 m_startPos;

    //移動する高さ
    [SerializeField]
    private float m_height = 0.3f;

    //移動速度
    [SerializeField]
    private float m_moveSpeed = 0.01f;

    //サイズ変更速度
    [SerializeField]
    private float m_scaleSpeed = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        m_upDownFlag = false;

        //最初の座標を設定
        m_startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //上下移動
        UpDownMove();
        //
        UpDownScale();
    }

    //上下移動
    private void UpDownMove()
    {
        //最初の座標＋高さより今の座標のほうが上か、最初の座標より今の座標が下の時
        if (m_startPos.y - m_height > transform.position.y || m_startPos.y < transform.position.y)
        {
            //フラグを反転させる
            m_upDownFlag = !m_upDownFlag;
        }
        
        if (!m_upDownFlag)
        {
            //下へ移動する
            Vector3 speed = new Vector3(0.0f, m_moveSpeed, 0.0f);
            transform.position -= speed;
        }
        else
        {
            //上へ移動する
            Vector3 speed = new Vector3(0.0f, m_moveSpeed, 0.0f);
            transform.position += speed;
        }
    }

    private void UpDownScale()
    {
        if(!m_upDownFlag)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - m_scaleSpeed, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + m_scaleSpeed, transform.localScale.z);
        }
    }

}
