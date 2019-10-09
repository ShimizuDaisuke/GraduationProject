//======================================================================================= 
//! @file       CoverOfEraser
//! @brief      何を行うファイルなのか 
//! @author     長尾昌輝
//! @date       2019/10/08
//! @note       メモ  ※書かなくてもいい 
//======================================================================================= 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverOfEraser : MonoBehaviour
{

    // 最大HP
    [SerializeField]
    private int MaxHP = 50;
    // 現在のHP
    private int Hp;
    // Start is called before the first frame update
    void Start()
    {
        Hp = MaxHP;
    }

    // 取得設定関数
    // 現在のHP
    public int EraserHP { get { return Hp; } set { Hp = value; } }

    // 最大HP
    public int EraserMaxHP { get { return MaxHP; } set { MaxHP = value; } }
}
