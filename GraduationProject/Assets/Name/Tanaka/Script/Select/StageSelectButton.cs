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

    //フェードのスクリプト
    private Fade Fade;

    //破棄しないように設定したオブジェクト
    [SerializeField]
    private GameObject FadeObject = default;

    //画面を触ったか判定フラグ
    private bool m_switchingFlag = false;

    //タイマー
    private float m_timer = 0.0f;

    //最大値
    [SerializeField]
    private float m_maxTimer = 0.0f;

    //SEの再生フラグ
    private bool m_seFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        Fade = FadeObject.GetComponent<Fade>();
    }

    // Update is called once per frame
    void Update()
    {
        //画面をクリックしたら
        if (m_switchingFlag == true)
        {
            //時間を進める
            m_timer += Time.deltaTime;

            //タイマーが最大値を超えたら
            if (m_timer > m_maxTimer)
            {
                //SEを再生してないなら
                if (m_seFlag == false)
                {
                    //SEの再生
                    SoundManager.PlaySE(SoundManager.Sound.SE_ChangeTitleGameSceneButton);
                    //フェードアウト開始
                    Fade.FadeOut = true;
                }

                //再生終了
                m_seFlag = true;

                //フェードアウトが終了したか
                if (Fade.FadeOut == false)
                {
                    //シーン切り替え
                    Scene();
                }
            }
        }

    }

    public void OnClick()
    {
        //SEの再生
        SoundManager.PlaySE(SoundManager.Sound.SE_TitleResultTap);

        //フェードインが終了したか
        if (Fade.FadeIn == false)
        {
            //触れた判定に
            m_switchingFlag = true;
        }
    }

    //======================================================================================= 
    //! @brief      シーンを切り替える関数
    //======================================================================================= 
    public void Scene()
    {
        //シーン切り替え
        SceneManager.LoadScene(SelectScene);
    }
}
