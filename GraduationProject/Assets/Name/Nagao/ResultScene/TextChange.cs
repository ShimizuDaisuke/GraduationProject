//======================================================================================= 
//! @file       TextChange
//! @brief      ゲームクリアかゲームオーバーのTextの表示切り替え
//! @author     長尾昌輝 
//! @date       2019/09/27 
//! @note       無し
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChange : MonoBehaviour
{
    //Textの表示
    [SerializeField]　private Text text;

    //ゲームの結果判定フラグ
    [SerializeField]  private bool resultFlag = false;

   // Start is called before the first frame update
   void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (resultFlag == true)
        {
            //ゲームクリアした時
            text.text = "GameClear";
        }
        else
        {
            //ゲームオーバーした時
            text.text = "GameOver";
        }
    }
}
