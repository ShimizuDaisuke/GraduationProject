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
    // 書き込む変数
    public Text timerText;

    //フレーム数
    public float totalTime;

    //時間
    int seconds;

    //時を進めるフラグ
    bool timerFlag;

    // Start is called before the first frame update
    void Start()
    {
        //実行時にタイマー作動させるため
        timerFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        //負の数(-1とか)にならないようにするif文
        if(timerFlag)
        {
            //フレームごとにフレームの秒数を引いてる
            totalTime -= Time.deltaTime;

            //キャストしたものを代入して小数点以下抹殺
            seconds = (int)totalTime;

            //文字列にしてからテキストに表示
            timerText.text = seconds.ToString();

            //テスト動作Zキーを押している間制限時間が減る
            if (Input.GetKey(KeyCode.Z))
            {
                seconds--;
            }

            //もしタイマーが0以下になりそうになったら
            if (seconds < 0)
            {
                //タイマーを停止
                timerFlag = false;

                //タイマーを0にする
                seconds = 0;
                totalTime = 0.0f;

                //文字列にしてからテキストに表示
                timerText.text = seconds.ToString();

                SceneManager.LoadScene("Result");
            }
        }
    }

    /// <summary>
    /// 取得・設定関数
    /// </summary>
        
        // 残り時間
        public int Int_RemainingTime { get { return seconds; } private set { seconds = value; } }

}
