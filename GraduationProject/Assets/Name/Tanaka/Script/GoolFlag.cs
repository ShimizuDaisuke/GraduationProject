//=======================================================================================
//! @file   GoolFlag.cs
//! @brief  ゴールの旗の処理
//! @author 田中歩夢
//! @date   9月27日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ゴールフラグのクラス
public class GoolFlag : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 衝突判定（Enter）
    /// </summary>
    /// <param name="other">衝突したオブジェクト</param>
    void OnCollisionEnter(Collision other)
    {
        //ゴールの旗に当たったら
        if (other.gameObject.tag == "Player")
        {
            //リザルトシーンへ
            SceneManager.LoadScene("Result");
        }
    }
}
