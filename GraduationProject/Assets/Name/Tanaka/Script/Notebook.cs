//=======================================================================================
//! @file   Notebook.cs
//! @brief  ノートブックの処理
//! @author 田中歩夢
//! @date   10月07日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ノートブックのクラス
public class Notebook : MonoBehaviour
{
    //スコア
    [SerializeField]
    private int m_score = 0;
    //落書きがあるかどうか
    [SerializeField]
    private bool m_graffiti = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ノートブックのスコアの取得・設定
    public int Score { get { return m_score; } set { m_score = value; } }
    //落書きの取得・設定
    public bool Graffiti { get { return m_graffiti; } set { m_graffiti = value; } }


}
