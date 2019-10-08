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


    //OnTriggerEnter
    void OnTriggerStay(Collider col)
    {

        if ((col.gameObject.tag == "Sword") && (Script_PlayerFlashing.IsPlayerFlashing == false))
        {
            Debug.Log("うんち！");

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
}
