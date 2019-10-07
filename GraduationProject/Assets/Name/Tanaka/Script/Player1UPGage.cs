//=======================================================================================
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
    //残機
    [SerializeField]
    private int m_life = 3;
    //今まで溜まった体力ゲージ
    private int m_accumulateGage = 0;
    //ゲージが増える量
    [SerializeField]
    private int m_addGage = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //1UP処理
        LifeUP();
        Debug.Log(m_life);
        Debug.Log(m_accumulateGage);
    }

    //1UP処理
    private void LifeUP()
    {
        if(m_accumulateGage >= MAX1UP_LIFEGAGE)
        {
            m_life += 1;
            m_accumulateGage = 0;
        }
    }

    //衝突判定
    void OnTriggerEnter(Collider collider)
    {
        //消しカスに当たったら
        if (collider.gameObject.tag == "EraserDust")
        {
            //ゲージを増やす
            m_accumulateGage += m_addGage;
            //消しカスを消滅


        }
    }
}
