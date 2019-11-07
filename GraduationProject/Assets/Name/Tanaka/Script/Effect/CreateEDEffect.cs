//=======================================================================================
//! @file   CreateEDEffect.cs
//! @brief  通常の消しカスのエフェクトの生成処理
//! @author 田中歩夢
//! @date   11月01日
//! @note   ない
//======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//通常の消しカスのエフェクトの生成クラス
public class CreateEDEffect : MonoBehaviour
{

    //通常の消しカスのエフェクトのプレハブ
    [SerializeField]
    private GameObject objPrefab;

    public void Create(Vector3 pos)
    {
        //生成
        Instantiate(objPrefab, pos, Quaternion.identity);

    }
}
