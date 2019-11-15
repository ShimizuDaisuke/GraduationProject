//=======================================================================================
//! @file   PencilCaseCoverClose.cs
//! @brief  筆箱のカバーを閉じる処理
//! @author 田中歩夢
//! @date   11月13日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//筆箱のカバーを閉じるクラス
public class PencilCaseCoverClose : MonoBehaviour
{
    //当たったらゴール座標に移動のクラス
    [SerializeField]
    private GoolPosMovePlayer m_goolPosMovePlayer = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_goolPosMovePlayer.GoolINFlag)
        {
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + 1,1.0f); 
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 90.0f, 200.0f), 0.5f);
        }


    }
}
