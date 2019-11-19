//======================================================================================= 
//! @file       ResultSceneController
//! @brief      Resultからのシーン遷移
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

public class ResultSceneController : MonoBehaviour
{
    //画面を触ったか判定フラグ
    private bool m_switchingFlag = false;

    //画面を二回触ったか判定フラグ
    private bool m_switchingTwoFlag = false;

    //タイマー
    private float m_timer = 0.0f;

    //最大値
    [SerializeField]
    private float m_maxTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        //画面をクリックしたら
        if (m_switchingTwoFlag == true)
        {
            //時間を進める
            m_timer += Time.deltaTime;

            //タイマーが最大値を超えたら
            if (m_timer > m_maxTimer)
            {
                //シーン切り替え
                Scene();
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

        if(m_switchingFlag == true)
        {
            m_switchingTwoFlag = true;
        }
        //触れた判定に
        m_switchingFlag = true;

    }

    //======================================================================================= 
    //! @brief      シーンを切り替える関数
    //======================================================================================= 
    public void Scene()
    {

        //Titleに切り替える切り替える
        SceneManager.LoadScene("Title");
    }

    // 取得設定関数
    public bool SwitchingFlag { get { return m_switchingFlag; } set { m_switchingFlag = value; } }
}
