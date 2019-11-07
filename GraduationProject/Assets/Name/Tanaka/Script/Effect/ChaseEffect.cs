//=======================================================================================
//! @file   ChaseEffect.cs
//! @brief  あとから追従するエフェクト処理
//! @author 田中歩夢
//! @date   11月01日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//追従するエフェクトクラス
public class ChaseEffect : MonoBehaviour
{
    //プレイヤーの操作のクラス
    [SerializeField]
    private PlayerController m_playerCon = default;

    //イベントクラス
    [SerializeField]
    private EventDirector m_event;

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
        //プレイヤーの移動速度が0じゃなかったらエフェクトを出す
        if(m_playerCon.DX != 0.0f || m_playerCon.DY != 0.0f)
        {
            m_particle.Play();
        }
        else
        {
            m_particle.Stop();
        }

        //イベント中だったらエフェクトを止める
        if(m_event.IsEventKIND != EventDirector.EventKIND.NONE)
        {
            m_particle.Stop();
        }

    }
}
