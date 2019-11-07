//=======================================================================================
//! @file   NomalEraserDustEffect.cs
//! @brief  通常の消しカスのエフェクト処理
//! @author 田中歩夢
//! @date   11月01日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//通常の消しカスのエフェクトクラス
public class NomalEraserDustEffect : MonoBehaviour
{
    //パーティクルシステム
    private ParticleSystem m_particle;

    // Start is called before the first frame update
    void Start()
    {
        m_particle = GetComponent<ParticleSystem>();

        m_particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            m_particle.Play();
        }
        

    }
}
