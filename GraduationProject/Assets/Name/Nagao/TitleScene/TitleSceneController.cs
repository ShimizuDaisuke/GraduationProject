﻿//======================================================================================= 
//! @file       TitleSceneController
//! @brief      Titleからのシーン遷移
//! @author     長尾昌輝 
//! @date       2019/09/27 
//! @note       無し
//=======================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ボタンを使用するためUIとSceneManagerを使用ためSceneManagementを追加
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //======================================================================================= 
    //! @brief      シーンを切り替える関数
    //======================================================================================= 
    public void OnRetry()
    {
        //Baseに切り替える切り替える
        SceneManager.LoadScene("Base");
    }
}
