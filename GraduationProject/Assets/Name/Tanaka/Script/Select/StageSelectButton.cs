//=======================================================================================
//! @file   StageSelectButton.cs
//! @brief  ステージ選択ボタンの処理
//! @author 田中歩夢
//! @date   11月28日
//! @note   ない
//=======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ステージ選択ボタンのクラス
public class StageSelectButton : MonoBehaviour
{
    //次のシーンの文字列
    [SerializeField]
    private string SelectScene = default;

    public void OnClick()
    {
        SceneManager.LoadScene(SelectScene);
    }
}
