//===============================================
//! @file       TimerController
//! @brief      プレイシーンの時計の処理
//! @author     服部晃大
//! @date       9/28
//! @note       
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//シーン遷移に必要なためSceneManager追加
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{

    //シーンのタイム
    [SerializeField]
    private GameObject TimeDirector = default;

    //スタートラインフラグ
    [SerializeField]
    private StartLineFlag m_startLineFlag = default;

    // スクリプト：タイムマネージャー
    private TimeManager Script_Time;

    // 書き込む変数
    public Text timerText;

    //フレーム数
    private float totalTime = 0;

    //時間
    int seconds;

    //時を進めるフラグ
    bool timerFlag;

    //残り時間の桁数
    string remainingTime = "D3";


    // Start is called before the first frame update
    void Start()
    {
        Script_Time = TimeDirector.GetComponent<TimeManager>();

        //実行時にタイマー作動させるため
        timerFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        //負の数(-1とか)にならないようにするif文
        if(m_startLineFlag.StartFlag)
        {
            if (timerFlag)
            {
                //フレームごとにフレームの秒数を引いてる
                totalTime += Time.deltaTime;

                //キャストしたものを代入して小数点以下抹殺
                seconds = (int)totalTime;

                //文字列にしてからテキストに表示
                timerText.text = seconds.ToString();

                // ステージを冒険し始めてから掛かった時間を取得する
                Script_Time.ClearTime = totalTime;
            }
        }
       
        
        //文字列にしてからテキストに表示
        timerText.text = seconds.ToString(remainingTime);
    }
    /// <summary>
    /// 取得・設定関数
    /// </summary>

    // 残り時間
    public int Int_RemainingTime { get { return seconds; } private set { seconds = value; } }

    // 時を進めるフラグ
    public bool TimerFlag { get { return timerFlag; }  set { timerFlag = value; } }
}
