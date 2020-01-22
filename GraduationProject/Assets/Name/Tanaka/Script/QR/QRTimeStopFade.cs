//=======================================================================================
//! @file   QRTimeStopFade.cs
//! @brief  時間停止中のフェード？の処理
//! @author 田中歩夢
//! @date   01月21日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//時間停止中のフェードのクラス
public class QRTimeStopFade : MonoBehaviour
{
    //QRTimeStop
    [SerializeField]
    private QRTimeStop m_qrTimeStop = default;

    //イメージ
    private Image m_image = default;

    //アルファの最大値Unity側で設定
    private float m_maxAlfa = 0.0f;

    //代入するアルファ
    private float m_alfa = 0.0f;

    //初期aの代入フラグ
    private bool m_initalFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        m_image = GetComponent<Image>();
        m_maxAlfa = m_image.color.a * 255.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (m_qrTimeStop.TimeStop == QRTimeStop.TIMESTOP_COUNT.NONE)
        {
            m_alfa = 0.0f;
            SetAlpha();
            m_initalFlag = false;
        }
        else
        {
            if(!m_initalFlag)
            {
                m_alfa = m_maxAlfa;
                m_initalFlag = true;
            }

            if(m_alfa >= 0.0f)
            {
                m_alfa = m_alfa - (m_maxAlfa / ((int)m_qrTimeStop.TimeStop * 60.0f));
                Debug.Log(m_alfa);
                SetAlpha();
            }
            
            
        }
        
    }


    void SetAlpha()
    {
        m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, m_alfa / 255.0f);
    }

}
