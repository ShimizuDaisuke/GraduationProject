//=======================================================================================
//! @file   NotebookGauge.cs
//! @brief  落書きノートのゲージの処理
//! @author 田中歩夢
//! @date   12月10日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//落書きノートのゲージの処理
public class NotebookGauge : MonoBehaviour
{
    //ノートブッククラス
    [SerializeField]
    private NotebookController m_notebookCon = default;

    //レクトトランスフォーム
    private RectTransform m_rectTrans = default;

    // Start is called before the first frame update
    void Start()
    {
        m_rectTrans = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        m_rectTrans.sizeDelta = new Vector2(m_notebookCon.NoteBookGauge, 37.0f);
    }
}
