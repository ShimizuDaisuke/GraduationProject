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
        Script_Score = ScoreDirector.GetComponent<ScoreManager>();

        Script_Time = TimeDirector.GetComponent<TimeManager>();

        totalScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //スコアに代入
        score = Script_Score.IsPlayerScore;

        timeLimit = Script_Time.IsPlayerTime;

        //トータルスコアの計算
        totalScore = Script_Score.IsPlayerScore + ((int)timeLimit * 100);

        // スコアを表示する
        //scoreText.text = totalScore.ToString();

        if (int.Parse(scoreText.text) < totalScore)
        {
            //トータルのスコアを5fすすめる
            StartCoroutine(ScoreAnimation(totalScore, 5));
        }

    }

    // スコアをアニメーションさせる
    IEnumerator ScoreAnimation(int addScore, float time)
    {
        //前回のスコア
        int befor = score;
        //今回のスコア
        float after = score + addScore;
        //得点加算
        score += addScore;
        //0fを経過時間にする
        float elapsedTime = 0.0f;

        //timeが０になるまでループさせる
        while (elapsedTime < time)
        {
            float rate = elapsedTime / time;
            // テキストの更新
            scoreText.text = (befor + (after - befor) * rate).ToString("f0");

            elapsedTime += Time.deltaTime;
            // 0.01秒待つ
            yield return new WaitForSeconds(0.01f);
        }
        // 最終的な着地のスコア
        scoreText.text = after.ToString();
    }
}
