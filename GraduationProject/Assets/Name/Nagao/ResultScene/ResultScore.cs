//======================================================================================= 
//! @file       ResultScore
//! @brief      トータルのスコアの表示
//! @author     長尾昌輝
//! @date       2019/10/15
//! @note       メモ  ※書かなくてもいい 
//======================================================================================= 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{

    //リザルトメインのスクリプト
    private ClearManagement clearManager;

    //破棄しないように設定したオブジェクト
    [SerializeField]
    private GameObject ClearObject = default;

    //シーンのスコア
    [SerializeField]
    private GameObject ScoreDirector = default;
    // スクリプト：スコアマネージャー
    private ScoreManager Script_Score;

    //シーンのタイム
    [SerializeField]
    private GameObject TimeDirector = default;
    // スクリプト：タイムマネージャー
    private TimeManager Script_Time;

    //Resultからのシーン遷移
    [SerializeField]
    private GameObject resultSceneController = default;
    // スクリプト：タイムマネージャー
    private ResultSceneController Script_ResultSceneController;

    // スコアを表示する
    [SerializeField]
    private Text scoreText = null;

    // スコア
    private int score;

    //トータルのスコア
    private int totalScore = 0;

    //制限時間　
    private float timeLimit = 0;

    // Start is called before the first frame update
    void Start()
    {
        ///リザルトメインのスクリプトの割り当て
        clearManager = ClearObject.GetComponent<ClearManagement>();

        //スコアのスクリプトの割り当て
        Script_Score = ScoreDirector.GetComponent<ScoreManager>();

        //時間のスクリプトの割り当て
        Script_Time = TimeDirector.GetComponent<TimeManager>();

        Script_ResultSceneController = resultSceneController.GetComponent<ResultSceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        //スコアに代入
        score = Script_Score.IsPlayerScore;

        timeLimit = Script_Time.ClearTime;

        //トータルスコアの計算
        if (clearManager.IsPlayerClear == true)
        {
            //ゲームクリアした時
            totalScore = score + ((int)timeLimit * 100);
        }
        else
        {
            //ゲームオーバーした時
            totalScore = 0;
        }

        // スコアを表示する
        scoreText.text = totalScore.ToString();
     

    }

}
