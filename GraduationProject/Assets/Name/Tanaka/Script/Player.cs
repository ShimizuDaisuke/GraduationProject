//=======================================================================================
//! @file   Player.cs
//! @brief  プレイヤーの処理
//! @author 田中歩夢
//! @date   10月07日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーのクラス
public class Player : MonoBehaviour
{
    //最大HP
    [SerializeField]
    private int m_MaxHP = 100;
    //最大残機
    [SerializeField]
    private int m_MaxLife = 5;
    //プレイヤーを守っている消しゴムのカバー
    private GameObject m_cover = null;
    //現在の体力
    private int m_HP;
    //残機
    private int m_life;

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        m_HP = m_MaxHP;
        m_life = m_MaxLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //プレイヤーの最大HPを取得・設定
    public int MaxHP { get { return m_MaxHP; } set { m_MaxHP = value; } }
    //プレイヤーの最大残機を取得・設定
    public int MaxLife { get { return m_MaxLife; } set { m_MaxLife = value; } }
    //プレイヤーを守っているカバーを取得・設定
    public GameObject Cover { get { return m_cover; } set { m_cover = value; } }
    //プレイヤーのHPを取得・設定
    public int HP { get { return m_HP; } set { m_HP = value; } }
    //プレイヤーの残機を取得・設定
    public int Life { get { return m_life; } set { m_life = value; } }
    
}
