//======================================================================================= 
//! @file       GameFinish
//! @brief      Escapeキーを押したらゲーム終了する
//! @author     長尾昌輝
//! @date       2019/09/26
//! @note       PCでゲーム終了させるように
//=======================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinish : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Escapeキーを押したらゲーム終了
        if (Input.GetKey(KeyCode.Escape))
        {
            //終了処理
            Application.Quit();
        }
    }


}
