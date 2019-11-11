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
    void OnEnable()

    {

        StartCoroutine(ParticleWorking());

    }

    IEnumerator ParticleWorking()

    {

        var particle = GetComponent<ParticleSystem>();



        yield return new WaitWhile(() => particle.IsAlive(true));



        Destroy(gameObject);

    }

#if false

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
        // パーティクルを呼ぶ
        m_particle = GetComponent<ParticleSystem>();

        // パーティクルを再生する
        m_particle.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // パーティクルがない場合、何もしない
        if (m_particle != null) return;



        // パーティクルが再生終わったら、破棄する
        if (m_particle.time > m_particle.startLifetime)
        {
            Destroy(this.gameObject);
        }

        ////プレイヤーの移動速度が0じゃなかったらエフェクトを出す
        //if(m_playerCon.DX != 0.0f || m_playerCon.DY != 0.0f)
        //{
        //    m_particle.Play();
        //}
        //else
        //{
        //    m_particle.Stop();
        //}
        //
        ////イベント中だったらエフェクトを止める
        //if(m_event.IsEventKIND != EventDirector.EventKIND.NONE)
        //{
        //    m_particle.Stop();
        //
        //}

    }
#endif
}
