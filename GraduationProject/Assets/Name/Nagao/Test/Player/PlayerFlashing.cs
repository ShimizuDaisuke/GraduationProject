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
    // 点滅の時間間隔
    [SerializeField]
    private float intervalTime = default;
    // 点滅による時間
    private float invincibleTime = 0.0f;

    [SerializeField]
    private float invincibleMaxTime = 0.0f;

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
    }

    //======================================================================================= 
    //! @brief          プレイヤーを点滅する関数
    //======================================================================================= 
    void Flashing()
    {
        // 点滅時間を計る
        invincibleTime += Time.deltaTime;
        // 点滅するか
        if (invincibleTime >= intervalTime)
        {
            // 時間をリセットする
            invincibleTime += -intervalTime;
        }

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
            invincibleTime += Time.deltaTime;
        }
        else
        {
            //無敵状態の解除
            IsFlashing = false;

            //無敵時間のリセット
            invincibleTime = 0.0f;

            //点滅時間のリセット
            invincibleTime = 0.0f;

            // 表示非表示を反転する
            player.SetActive(true);
        }
    }


    // 取得設定関数
    public bool IsPlayerFlashing { get { return IsFlashing; } set { IsFlashing = value; } }
}
