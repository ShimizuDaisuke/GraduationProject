//======================================================================================= 
//! @file       EnemyDamage.cs 
//! @brief      ダメージを与える
//! @author     長尾昌輝 
//! @date       2019/10/08 
//! @note       メモ  ※書かなくてもいい 
//======================================================================================= 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //ダメージ
    [SerializeField]
    private int damage = 10;

    public int IsDamage { get { return damage; } set { damage = value; } }
}
