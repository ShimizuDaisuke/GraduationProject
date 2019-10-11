//=======================================================================================
//! @file   EffectRote.cs
//! @brief  エフェクトの回転処理
//! @author 田中歩夢
//! @date   10月10日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//エフェクトの回転クラス
public class EffectRote : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 常に回転する
        transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime, Space.World);
    }

}