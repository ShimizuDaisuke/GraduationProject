//======================================================================================= 
//! @file       PlayerHit.cs 
//! @brief      刃物とプレイヤーの当たり判定を行う
//! @author     長尾昌輝 
//! @date       2019/10/08
//======================================================================================= 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 型名省略
using Particle = ParticleManager.Particle;

public class PlayerHit : MonoBehaviour
{
    // プレイヤーの監督
    [SerializeField]
    private GameObject PlayerDirector = default;
    // スクリプト：Playerを点滅させる処理
    private PlayerFlashing Script_PlayerFlashing;

    // スクリプト : プレイヤー
    private Player Script_Player;

    // 消しゴムのカバーを付けたか
    private bool IsFixCover = false;

    // Start is called before the first frame update
    void Start()
    {
        Script_PlayerFlashing = PlayerDirector.GetComponent<PlayerFlashing>();

        Script_Player = GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {

    }



    void OnTriggerEnter(Collider col)
    {
        //消しゴムカバーと当たった時
        if(col.gameObject.tag == "EraserDustCover")
        {

            //プレイヤーのHPをMAXにする
            Script_Player.HP = Script_Player.MaxHP;

            //プレイヤーとカバーを親子関係にする
            col.gameObject.transform.parent = this.gameObject.transform;

            //プレイヤーとカバーを同じ位置にする
            col.gameObject.transform.position = this.gameObject.transform.position;

            //プレイヤーとカバーを同じ角度にする
            col.gameObject.transform.rotation = this.gameObject.transform.rotation;

            //SEの再生
            SoundManager.PlaySE(SoundManager.Sound.SE_CoverComesOn);

            // 消しゴムにカバーを付けた
            IsFixCover = true;
        }
    }
    
    void OnTriggerStay(Collider col)
    {
        //刃物との当たり判定
        if ((col.gameObject.tag == "Sword") && (Script_PlayerFlashing.IsPlayerFlashing == false)&& (IsFixCover == false)&&(Script_Player.HP > 0))
        {
            //スクリプト　：
            EnemyDamage Script_EnemyDamage = col.gameObject.GetComponent<EnemyDamage>();
            // 障害物のダメージ量
            int damage = Script_EnemyDamage.IsDamage;
            // ダメージを受ける
            Script_Player.HP += -damage;
            // プレイヤーが刃物に触れた
            Script_PlayerFlashing.IsPlayerFlashing = true;

            //現在の位置を代入
            Vector3 m_pos = this.gameObject.transform.position;
            //ダメージエフェクト出現
            ParticleManager.PlayParticle(Particle.DamegeEF, m_pos);

            //SEの再生
            SoundManager.PlaySE(SoundManager.Sound.SE_PlayerNoCoverDamage);
        }
    }

    public bool FixCover { get { return IsFixCover; } set { IsFixCover = value; } }
}
