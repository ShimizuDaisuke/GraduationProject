//======================================================================================= 
//! @file       SoundManager.cs 
//! @brief      SEを鳴らす
//! @author     長尾昌輝
//! @date       2019/11/06    
//======================================================================================= 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    //静的なSEのリスト
    public static List<AudioClip> s_SoundSE;
    //SEのリスト
    public List<AudioClip> SoundSE;

    enum Sound
    {
        OneUPGaugeSE,           //1UPゲージが増えた効果音
        AdditionScoreSE,        //スコアが加算する効果音
        ChangeButtonSE,         //2D⇔3D切り替え時のボタンの効果音
        DominoHitSE,            //本がドミノ倒しのように倒れて、他のオブジェクトに当たった効果音
        EraseGraffitiSE,        //プレイヤーが落書きのノートを消す効果音
        NotChangeButtonSE,      //2D⇔3D切り替え以外のボタンの効果音
        PlayerCoverDamageSE,    //カバーが付いているプレイヤーがダメージを受けた効果音
        PlayerDamageSE,         //プレイヤーがダメージを受けた時の効果音
        SeesawJumpSE            //プレイヤーがシーソーによって、ジャンプする効果音
    }

    //======================================================================================= 
    //! @brief      SEを再生する関数
    //! @param[in]   enum SoundManager Sound     
    //======================================================================================= 
    static void PlaySE(Sound kind)
    {
        audioSource.PlayOneShot(s_SoundSE[(int)kind]);
    }

    //オーディオソース
    private static AudioSource audioSource;

  
    void Awake()
    {
        //Componentを取得
        audioSource = gameObject.GetComponent<AudioSource>();

        //staticに代入する
        s_SoundSE = SoundSE;
    }
}
