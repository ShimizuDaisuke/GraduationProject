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
    [SerializeField] private Text text;

    //リザルトメインのスクリプト
    private ClearManagement clearManager;

    //破棄しないように設定したオブジェクト
    [SerializeField]
    private GameObject ClearObject;


    // Start is called before the first frame update
    void Start()
    {
        ///リザルトメインのスクリプトの割り当て
        clearManager = ClearObject.GetComponent<ClearManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (clearManager.IsPlayerClear == true)
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
