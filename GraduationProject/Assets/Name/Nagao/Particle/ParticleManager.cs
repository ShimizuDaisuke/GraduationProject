//======================================================================================= 
//! @file       ParticleManager.cs 
//! @brief      パーティクル再生用クラス
//! @author     長尾昌輝
//! @date       2019/11/08    
//======================================================================================= 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    //静的なParticleのリスト
    public static List<GameObject> s_ParticleObj;
    //Particleのリスト
    public List<GameObject> ParticleObj;

    // 現在再生中のParticleのリスト
    private static List<GameObject> ParticleObj_NowPlay;

    public enum Particle
    {
        NomalEraserDustEF,      //通常の消しカスに当たったら、出現するエフェクト
        SpacelEraserDustEF,     //特別な消しカスに当たったら、出現するエフェクト
        DamegeEF,               //プレイヤーがはさみなどの刃物に触れたら、出現するエフェクト
        JumpEF,                 //プレイヤーがジャンプしたら、足元に出現するエフェクト
        EventEF,                //イベント用のエフェクト　
        TouchEF,                //ゲームシーン以外でタブレット上でタップしたら、出現するエフェクト
        CoverDamegeEF           //消しゴムカバーを装備している時に刃物当たった時のエフェクト
    }


    void Awake()
    {
        // それぞれのリストを作成する
        s_ParticleObj = new List<GameObject>();
        ParticleObj_NowPlay = new List<GameObject>();

        // 動的変数から静的変数へ割り当てる
        foreach (GameObject obj in ParticleObj)
        {
            // 一つ一つに割り当てる
            s_ParticleObj.Add(obj);
        }

    }

    //======================================================================================= 
    //! @brief      Particleを再生する関数
    //! @param[in]   enum SoundManager Sound     
    //======================================================================================= 
    public static void PlayParticle(Particle kind,Vector3 pos)
    {
        // 指定されたオブジェクトがない場合、何もしない
        if (s_ParticleObj[(int)kind] == null) return;

        //オブジェクトを複製する 
        GameObject obj = Instantiate(s_ParticleObj[(int)kind]) as GameObject;

        //オブジェクトの位置を指定した位置にする
        obj.transform.position = pos;

    }


}
