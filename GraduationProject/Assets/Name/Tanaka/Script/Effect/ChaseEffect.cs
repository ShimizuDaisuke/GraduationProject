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

    //プレイヤーのオブジェクト
    [SerializeField]
    private GameObject m_player;

    // プレイヤーとこのパーティクルの距離
    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        // パーティクルを呼ぶ
        m_particle = GetComponent<ParticleSystem>();

        // パーティクルを再生する
        m_particle.Play();

        //プレイヤー　－　こいつ　の距離計算
        dir = m_player.transform.position - transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        // パーティクルがない場合、何もしない
        if (m_particle == null) return;

        //プレイヤーの移動速度が0じゃなかったらエフェクトを出す
        if (m_playerCon.D.x != 0.0f || m_playerCon.D.y != 0.0f)
        {
            m_particle.Play();
        }
        else
        {
            m_particle.Stop();
        }

        //イベント中だったらエフェクトを止める
        if (m_event.IsEventKIND != EventDirector.EventKIND.NONE)
        {
            m_particle.Stop();

        }

        //プレイヤーの後ろにつく
        transform.position = m_player.transform.position - dir;



    }
}
