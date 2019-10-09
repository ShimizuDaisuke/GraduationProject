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

public class CoverOfEraserHit : MonoBehaviour
{
    // スクリプト : 消しゴムカバー
    private CoverOfEraser Script_CoverOfEraser;

    // スクリプト :Playerとの当たり判定
    private PlayerHit Script_PlayerHit;

    // スクリプト：Playerを点滅させる処理
    private PlayerFlashing Script_PlayerFlashing;

    // プレイヤーの監督
    [SerializeField]
    private GameObject PlayerDirector = default;

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
            Debug.Log("うんち");

            //スクリプト　：
            EnemyDamage Script_EnemyDamage = col.gameObject.GetComponent<EnemyDamage>();
            // 障害物のダメージ量
            int damage = Script_EnemyDamage.IsDamage;
            // ダメージを受ける
            Script_CoverOfEraser.EraserHP += -damage;
      
            // プレイヤーが刃物に触れた
            Script_PlayerFlashing.IsPlayerFlashing = true;


            // もし消しゴムのカバーによる体力が0以下の場合
            if (Script_CoverOfEraser.EraserHP <= 0)
            {
                Script_PlayerHit.FixCover = false;
                Destroy(this.gameObject);
            }
            Debug.Log(Script_CoverOfEraser.EraserHP);
        }
    }

}

