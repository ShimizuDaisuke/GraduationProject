//======================================================================================= 
//! @file       CoverOfEraserHit
//! @brief      何を行うファイルなのか 
//! @author     長尾昌輝
//! @date       2019/10/08
//! @note       メモ  ※書かなくてもいい 
//======================================================================================= 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 型名省略
using Particle = ParticleManager.Particle;

public class CoverOfEraserHit : MonoBehaviour
{
    //プレイヤーの属性
    [SerializeField]
    private PlayerType m_playerType = default;

    // スクリプト : 消しゴムカバー
    private CoverOfEraser Script_CoverOfEraser;

    // スクリプト :Playerとの当たり判定
    private PlayerHit Script_PlayerHit;

    // スクリプト：Playerを点滅させる処理
    private PlayerFlashing Script_PlayerFlashing;

    // プレイヤーの監督
    [SerializeField]
    private GameObject PlayerDirector = default;

    //プレイヤーの位置
    private Vector3 m_pos;

    //乱数の幅
    [SerializeField]
    private float m_max = 0.0f;

    [SerializeField]
    private float m_min = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Script_CoverOfEraser = GetComponent<CoverOfEraser>();

        Script_PlayerFlashing = PlayerDirector.GetComponent<PlayerFlashing>();

        //
        GameObject Player = GameObject.FindGameObjectWithTag("Player");

        Script_PlayerHit = Player.GetComponent<PlayerHit>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider col)
    {
        //刃物との当たり判定
        if ((col.gameObject.tag == "Sword") && (Script_PlayerFlashing.IsPlayerFlashing == false))
        {
            if (m_playerType.IsPlayerType == PlayerType.Type.ERASER)
            {
                //スクリプト　：
                EnemyDamage Script_EnemyDamage = col.gameObject.GetComponent<EnemyDamage>();
                // 障害物のダメージ量
                int damage = Script_EnemyDamage.IsDamage;
                // ダメージを受ける
                Script_CoverOfEraser.EraserHP += -damage;

                // プレイヤーが刃物に触れた
                Script_PlayerFlashing.IsPlayerFlashing = true;

                //SEの再生
                SoundManager.PlaySE(SoundManager.Sound.SE_PlayerCoverDamage);

                //プレイヤーの現在の位置を代入
                m_pos = this.gameObject.transform.position;

                //ランダム後の位置
                Vector3 m_randPos;

                m_randPos.x = Random.Range((m_pos.x + m_min), (m_pos.x + m_max));
                m_randPos.y = Random.Range((m_pos.y + m_min), (m_pos.y + m_max));
                m_randPos.z = Random.Range((m_pos.z + m_min), (m_pos.z + m_max));

                //ダメージエフェクト出現
                ParticleManager.PlayParticle(Particle.CoverDamegeEF, m_randPos);
            }

            // もし消しゴムのカバーによる体力が0以下の場合
            if (Script_CoverOfEraser.EraserHP <= 0)
            {
                //SEの再生
                SoundManager.PlaySE(SoundManager.Sound.SE_CoverComesOff);
                Script_PlayerHit.FixCover = false;
                Destroy(this.gameObject);
            }
        }
    }

}

