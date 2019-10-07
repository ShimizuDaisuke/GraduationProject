//======================================================================================= 
//! @file       PlayerFlashing.cs 
//! @brief      Playerを点滅させる処理
//! @author     長尾昌輝 
//! @date       2019/10/07
//======================================================================================= 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashing : MonoBehaviour
{
    //無敵時間
    [SerializeField]
    float invincibleTime = 0.0f;

    //無敵時間の最大値
    [SerializeField]
    float invincibleMaxTime = 0.0f;

    // 点滅したか
    [SerializeField]
    private bool IsFlashing　=false;

    [SerializeField]
    private GameObject player = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFlashing == true)
        {
            //プレイヤーを点滅させる
            Flashing();
            //無敵状態に
            unrivaled();
        }
        else
        {
            // 常に表示する
            player.SetActive(true);
        }
    }

    //======================================================================================= 
    //! @brief          プレイヤーを点滅する関数
    //======================================================================================= 
    void Flashing()
    {
       // 現在のプレイヤーの表示状態
       bool state = player.activeInHierarchy;

       // 表示非表示を反転する
       player.SetActive(!state);
    }


    //======================================================================================= 
    //! @brief      無敵状態の時にカウントをアップする関数
    //======================================================================================= 
    void unrivaled()
    {
        //無敵時間の最大値よりも小さい時
        if (invincibleMaxTime > invincibleTime)
        {
            //無敵時間のカウントアップ
            invincibleTime += 1.0f;
        }
        else
        {
            //無敵状態の解除
            IsFlashing = false;

            //無敵時間のリセット
            invincibleTime = 0.0f;
        }
    }


    // 取得設定関数
    public bool IsPlayerFlashing { get { return IsFlashing; } set { IsFlashing = value; } }
}
