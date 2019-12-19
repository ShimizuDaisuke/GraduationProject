//===============================================
//! @file       PauseController
//! @brief      ポーズ画面の出現
//! @author     服部晃大    
//! @date       12/18
//! @note       
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    //ポーズ画面を出すボタン
    [SerializeField]
    private GameObject pauseButton;

    //続けるボタン
    [SerializeField]
    private GameObject continueButton;

    //やり直しボタン
    [SerializeField]
    private GameObject restartButton;

    //タイトルに戻るボタン
    [SerializeField]
    private GameObject quitButton;

    //ポーズ画面の背景
    [SerializeField]
    private GameObject pauseBackground;

    //現在のシーン名
    string sceneName;

    //プレイヤーを止めるため
    [SerializeField]
    private GameObject playerDirector;

    //時を止めるため
    [SerializeField]
    private GameObject timeDirector;

    // Start is called before the first frame update
    void Start()
    {
        //ポーズボタンは最初から見える
        pauseButton.SetActive(true);

        //続けるボタンは最初は見えない
        continueButton.SetActive(false);

        //やり直しボタンは最初は見えない
        restartButton.SetActive(false);

        //タイトルに戻るボタンは最初は見えない
        quitButton.SetActive(false);

        //ポーズ画面の背景は最初は見えない
        pauseBackground.SetActive(false);

        //現在のシーン名を取得
        sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //======================================================================================= 
    //! @brief      ゲームを止めてポーズ画面を出す関数
    //======================================================================================= 
    public void StopGame()
    {
        //時を止める
        Time.timeScale = 0.0f;

        //ポーズボタンを見えなくする
        pauseButton.SetActive(false);

        //続けるボタンを見えるようにする
        continueButton.SetActive(true);

        //やり直しボタンを見えるようにする
        restartButton.SetActive(true);

        //タイトルに戻るボタンを見えるようにする
        quitButton.SetActive(true);

        //ポーズ画面の背景を見えるようにする
        pauseBackground.SetActive(true);

        // プレイヤーと時を止める
        StopOrMoveScript(false);
    }

    //======================================================================================= 
    //! @brief      ポーズ画面を解除してゲームを再開する関数
    //======================================================================================= 
    public void ContinueGame()
    {
        //プレイヤーと時が動き出す
        StopOrMoveScript(true);

        //ポーズボタンを見えるようにする
        pauseButton.SetActive(true);

        //続けるボタンを見えなくする
        continueButton.SetActive(false);

        //やり直しボタンを見えなくする
        restartButton.SetActive(false);

        //タイトルに戻るボタンを見えなくする
        quitButton.SetActive(false);

        //ポーズ画面の背景を見えなくする
        pauseBackground.SetActive(false);

        //時が動き出す
        Time.timeScale = 1.0f;
    }

    //======================================================================================= 
    //! @brief      現在のステージを最初からやり直す関数
    //======================================================================================= 
    public void ReStage()
    {
        //現在のシーンを読み込む
        SceneManager.LoadScene(sceneName);
    }

    //======================================================================================= 
    //! @brief      タイトルに戻る関数
    //======================================================================================= 
    public void QuitGame()
    {
        //タイトルに戻る
        SceneManager.LoadScene("Title");
    }

    //======================================================================================= 
    //! @brief      プレイヤーと時を止める関数
    //======================================================================================= 
    private void StopOrMoveScript(bool stopormove)
    {
        // プレイヤー内にあるスクリプト.表示状態 =  
        if (playerDirector != null)
        {
            //プレイヤーコントローラーを止める
            playerDirector.GetComponent<PlayerController>().enabled = stopormove;
            
            //プレイヤーの重力を止める
            playerDirector.GetComponent<Gravity>().enabled = stopormove;
        }
        
        //タイムディレクター内にあるスクリプト.表示状態 =
        if(timeDirector != null)
        {
            //タイムディレクターのタイムコントローラーを止める
            timeDirector.GetComponent<TimerController>().enabled = stopormove;
        }
    }
}
