//======================================================================================= 
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

// 型名省略
using Particle = ParticleManager.Particle;


public class TitleSceneController : MonoBehaviour
{
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
        if(m_switchingFlag == true)
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

    //======================================================================================= 
    //! @brief      画面を触ったらエフェクトが発生する関数
    //======================================================================================= 
    public void OnRetry()
    {
        //マウスの座標を2Dに変換
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //ゲームシーン以外でタブレット上でタップしたら、出現するエフェクト
        ParticleManager.PlayParticle(Particle.TouchEF, pos);

        //フェードインが終了したか
        if (Fade.FadeIn == false)
        {
            //触れた判定に
            m_switchingFlag = true;
        }

        //SEの再生
        SoundManager.PlaySE(SoundManager.Sound.SE_TitleResultTap);
    }

    //======================================================================================= 
    //! @brief      シーンを切り替える関数
    //======================================================================================= 
    public void Scene()
    {
        //Stage1に切り替える切り替える
        SceneManager.LoadScene("Select");
    }
}
