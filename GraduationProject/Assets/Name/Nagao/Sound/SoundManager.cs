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

    public enum Sound
    {
        SE_ChangeTitleGameSceneButton,      //タイトルシーンからゲームシーンへ変わる効果音
        SE_ChangeRezultGameSceneButton,     //リザルトシーンからタイトルシーンへ変わる効果音
        SE_PlayerNoCoverDamage,             //カバーが付いていないプレイヤーがダメージを受けた時の効果音
        SE_PlayerCoverDamage,               //カバーが付いているプレイヤーがダメージを受けた効果音
        SE_AdditionScore,                   //スコアが加算する効果音
        SE_OneUPGauge,                      //1UPゲージが増えた効果音
        SE_Change2D3DButton,                //2D⇔3D切り替え時のボタンの効果音
        SE_SeesawJump,                      //プレイヤーがシーソーによって、ジャンプする効果音
        SE_DominoHit,                       //本がドミノ倒しのように倒れて、他のオブジェクトに当たった効果音
        SE_EraseGraffiti,                   //プレイヤーが落書きのノートを消す効果音
        SE_ScissorsCutPaper,	            //紙をハサミで切る効果音
        SE_CoverComesOn,                    //消しゴムのカバーを付けた音
        SE_CoverComesOff                    //消しゴムのカバーを外した音
    }

    //======================================================================================= 
    //! @brief      SEを再生する関数
    //! @param[in]   enum SoundManager Sound     
    //======================================================================================= 
    public static void PlaySE(Sound kind)
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
