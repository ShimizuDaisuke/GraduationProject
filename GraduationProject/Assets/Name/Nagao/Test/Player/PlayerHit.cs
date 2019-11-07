//======================================================================================= 
//! @file       PlayerHit.cs 
//! @brief      刃物とプレイヤーの当たり判定を行う
//! @author     長尾昌輝 
//! @date       2019/10/08
//======================================================================================= 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    // プレイヤーの監督
    [SerializeField]
    private GameObject PlayerDirector = default;
    // スクリプト：Playerを点滅させる処理
    private PlayerFlashing Script_PlayerFlashing;

    //プレイヤーのサイズの最大値
    [SerializeField]
    private Vector3 maxSize = new Vector3(1.0f, 1.0f, 1.0f);

    //プレイヤーのサイズの最小値
    [SerializeField]
    private Vector3 minSize = new Vector3(0.1f, 0.1f, 0.1f);

    // プレイヤーの最大と最小の差
    private Vector3 SizeDifference = new Vector3(0.0f, 0.0f, 0.0f);

    // スクリプト : プレイヤー
    private Player Script_Player;

    // 消しゴムのカバーを付けたか
    private bool IsFixCover = false;

    // Start is called before the first frame update
    void Start()
    {
        Script_PlayerFlashing = PlayerDirector.GetComponent<PlayerFlashing>();

        Script_Player = GetComponent<Player>();

        // プレイヤーの最大と最小の差を求める
        SizeDifference = maxSize - minSize;
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
            // プレイヤーのサイズをMAXに変更する
            transform.localScale = maxSize;

            //プレイヤーのHPをMAXにする
            Script_Player.HP = Script_Player.MaxHP;

            //プレイヤーとカバーを親子関係にする
            col.gameObject.transform.parent = this.gameObject.transform;

            //プレイヤーとカバーを同じ位置にする
            col.gameObject.transform.position = this.gameObject.transform.position;

            //プレイヤーとカバーを同じ角度にする
            //col.gameObject.transform.rotation = this.gameObject.transform.rotation;

            // 消しゴムにカバーを付けた
            IsFixCover = true;
        }
    }
    
    void OnTriggerStay(Collider col)
    {
        //刃物との当たり判定
        if ((col.gameObject.tag == "Sword") && (Script_PlayerFlashing.IsPlayerFlashing == false)&& (IsFixCover == false))
        {
            //スクリプト　：
            EnemyDamage Script_EnemyDamage = col.gameObject.GetComponent<EnemyDamage>();
            // 障害物のダメージ量
            int damage = Script_EnemyDamage.IsDamage;
            // ダメージを受ける
            Script_Player.HP += -damage;

            // プレイヤーのサイズによる割合
            float sizerate = (float)Script_Player.HP / (float)Script_Player.MaxHP;
            // プレイヤーのサイズを変更する
            transform.localScale = minSize + SizeDifference * sizerate;
            // プレイヤーが刃物に触れた
            Script_PlayerFlashing.IsPlayerFlashing = true;
        }
    }

    public bool FixCover { get { return IsFixCover; } set { IsFixCover = value; } }
}
