﻿//=======================================================================================
//! @file   Player1UPGage.cs
//! @brief  プレイヤーの1UPゲージの処理
//! @author 田中歩夢
//! @date   10月07日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの１UPゲージのクラス
public class Player1UPGage : MonoBehaviour
{
    //最大体力ゲージ
    [SerializeField]
    private int MAX1UP_LIFEGAGE = 2;
    //今まで溜まった体力ゲージ
    private int m_accumulateGage = 0;
    //ゲージが増える量
    [SerializeField]
    private int m_addGage = 0;
    //消しカスのクラス
    private EraserDust m_eraserDust = default;
    // プレイヤー
    private Player m_player = default;


    // Start is called before the first frame update
    void Start()
    {
        m_player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //1UP処理
        LifeUP();
    }

    //1UP処理
    private void LifeUP()
    {
        if(m_accumulateGage >= MAX1UP_LIFEGAGE)
        {
            m_player.Life += 1;
            m_accumulateGage = 0;
        }
    }

    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        //消しカスに当たったら
        if (collider.gameObject.tag == "EraserDust")
        {
                                               
           //消しカスのクラスを取得
           m_eraserDust = collider.gameObject.GetComponent<EraserDust>();

            // 1UPゲージ用の消しカスによってスコアを増やす
            if (m_eraserDust.IsEraserDustKind == EraserDust.EraserDustKIND.ONEUPGAUGE)
            {
                // ゲージを増やす
                m_accumulateGage += m_eraserDust.PointRandom;
                //衝突した消しカスを消す
                Destroy(collider.gameObject);
            }

            
        }
    }
}
