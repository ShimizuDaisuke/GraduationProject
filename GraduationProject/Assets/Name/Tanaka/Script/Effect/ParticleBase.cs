//=======================================================================================
//! @file   ParticleBase.cs
//! @brief  あとから追従するエフェクト処理
//! @author 田中歩夢
//! @date   11月01日
//! @note   ない
//=======================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//追従するエフェクトクラス
public class ParticleBase : MonoBehaviour
{
    void OnEnable()

    {

        StartCoroutine(ParticleWorking());

    }

    IEnumerator ParticleWorking()

    {

        var particle = GetComponent<ParticleSystem>();



        yield return new WaitWhile(() => particle.IsAlive(true));



        Destroy(gameObject);

    }

#if false

    
#endif
}
