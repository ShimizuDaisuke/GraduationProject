//=======================================================================================
//! @file   PaperController.cs
//! @brief  紙の処理
//! @author 田中歩夢
//! @date   10月24日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//紙のクラス
public class PaperController : MonoBehaviour
{
    //イベントクラス
    [SerializeField]
    private EventDirector m_event;

    //ハサミのクラス
    [SerializeField]
    private ScissorsController m_scissorsCon;

    //色クラス
    private Color m_color;

    //フェードアウトスピード
    [SerializeField]
    private float m_fadeOutSpeed = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        //色を取得
        m_color = gameObject.GetComponent<Renderer>().material.color;   
    }

    // Update is called once per frame
    void Update()
    {
        //ハサミが回転しきったか
        if(m_scissorsCon.RotationFinishFlag)
        {
            m_color.a -= m_fadeOutSpeed;
            //色を設定
            gameObject.GetComponent<Renderer>().material.color = m_color;

        }

        //Alphaが0以下になったら破棄
        if(m_color.a < 0)
        {
            m_event.IsEventKIND = EventDirector.EventKIND.NONE;
            Destroy(gameObject);
        }

    }
}
