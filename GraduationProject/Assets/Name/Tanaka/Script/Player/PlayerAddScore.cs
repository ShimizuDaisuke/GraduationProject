﻿//=======================================================================================
//! @file   PlayerAddScore.cs
//! @brief  スコアを増やす処理
//! @author 田中歩夢
//! @date   10月07日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//スコアの増やすクラス
public class PlayerAddScore : MonoBehaviour
{
    //スコア
    [SerializeField]
    private Score m_score = default;
    //消しカスのクラス
    private EraserDust m_eraserDust = default;
    //ノートブックのクラス
    private Notebook m_notebook = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_score.Int_EraserScore);
    }

    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        //消しカスに当たったら
        if (collider.gameObject.tag == "EraserDust")
        {
            //消しカスのクラスを取得
            m_eraserDust = collider.gameObject.GetComponent<EraserDust>();

            // ノーマルの消しカスによってスコアを増やす
            if (m_eraserDust.IsEraserDustKind == EraserDust.EraserDustKIND.NORMAL)
            {
                // スコアを増やす
                m_score.Int_EraserScore += m_eraserDust.Point;
                //衝突した消しカスを消す
                Destroy(collider.gameObject);
            }
        }

        //ノートブックに当たったら
        if(collider.gameObject.tag == "Notebook")
        {
            //消しカスのクラスを取得
            m_notebook = collider.gameObject.GetComponent<Notebook>();

            // ノートブックに落書きがあるとき
            if (m_notebook.Graffiti)
            {
                // スコアを増やす
                m_score.Int_EraserScore += m_notebook.Score;
                // 衝突した落書きを消す
                m_notebook.Graffiti = false;
            }
        }
    }
}