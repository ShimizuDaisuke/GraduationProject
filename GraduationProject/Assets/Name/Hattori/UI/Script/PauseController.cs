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

    //セレクトに戻るボタン
    [SerializeField]
    private GameObject quitButton;

    //ポーズ画面の背景
    [SerializeField]
    private GameObject pauseBackground;

    //2D<->3Dボタン
    [SerializeField]
    private GameObject dimensionButton;

    //ジョイスティック
    [SerializeField]
    private GameObject joystickIcon;

    //現在のシーン名
    string sceneName;

    //プレイヤーを止めるため
    [SerializeField]
    private GameObject playerDirector;

    //時を止めるため
    [SerializeField]
    private GameObject timeDirector;

    //ジョイスティックの動き
    [SerializeField]
    private GameObject joystickDirector;

#if true
    //ポーズカウンター
    int pauseCounter = 0;
#else
    // ポーズするか
    bool IsPose = false;
#endif

    // Start is called before the first frame update
    void Start()
    {
        //ポーズボタンは最初から見える
        pauseButton.SetActive(true);

        //続けるボタンは最初は見えない
        continueButton.SetActive(false);

        //やり直しボタンは最初は見えない
        restartButton.SetActive(false);

        //セレクトに戻るボタンは最初は見えない
        quitButton.SetActive(false);

        //ポーズ画面の背景は最初は見えない
        pauseBackground.SetActive(false);

        //2D<->3Dボタンは最初から見える
        dimensionButton.SetActive(true);

        //ジョイスティックは最初から見える
        joystickIcon.SetActive(true);

        //現在のシーン名を取得
        sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickPoseButton()
    {
#if true

        //ポーズカウンターを一つ進める
        pauseCounter++;

        //ポーズカウンターが偶数ならポーズ画面解除
        if (pauseCounter % 2 == 0)
        {
            ContinueGame();
        }
        else
        {
            //奇数ならポーズ画面出す
            StopGame();
        }

#else

        // <別解>
        // ポーズする状態を反転する  ポーズしない⇔ポーズする
        IsPose = !IsPose;

        //ポーズをしない場合は、ポーズ画面を解除
        if (IsPose == false)
        {
            ContinueGame();
           
        }
        else
        {
            //ポーズする場合は、ポーズ画面出す
            StopGame();
        }

#endif

    }

    //======================================================================================= 
    //! @brief      ゲームを止めてポーズ画面を出す関数
    //======================================================================================= 
    public void StopGame()
    {
        //時を止める
        Time.timeScale = 0.0f;

        //続けるボタンを見えるようにする
        continueButton.SetActive(true);

        //やり直しボタンを見えるようにする
        restartButton.SetActive(true);

        //セレクトに戻るボタンを見えるようにする
        quitButton.SetActive(true);

        //ポーズ画面の背景を見えるようにする
        pauseBackground.SetActive(true);

        //2D<->3Dボタンを見えないようにする
        dimensionButton.SetActive(false);

        //ジョイスティックを使えないようにする
        joystickIcon.SetActive(false);

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

        //続けるボタンを見えなくする
        continueButton.SetActive(false);

        //やり直しボタンを見えなくする
        restartButton.SetActive(false);

        //セレクトに戻るボタンを見えなくする
        quitButton.SetActive(false);

        //ポーズ画面の背景を見えなくする
        pauseBackground.SetActive(false);

        //2D<->3Dボタンを見えるようにする
        dimensionButton.SetActive(true);

        //ジョイスティックを使えるようにする
        joystickIcon.SetActive(true);

        //時が動き出す
        Time.timeScale = 1.0f;

        pauseCounter++;
    }

    //======================================================================================= 
    //! @brief      現在のステージを最初からやり直す関数
    //======================================================================================= 
    public void ReStage()
    {
        //現在のシーンを読み込む
        SceneManager.LoadScene(sceneName);

        //プレイヤーと時が動き出す
        StopOrMoveScript(true);

        //時が動き出す
        Time.timeScale = 1.0f;
    }

    //======================================================================================= 
    //! @brief      タイトルに戻る関数
    //======================================================================================= 
    public void QuitGame()
    {
        //プレイヤーと時が動き出す
        StopOrMoveScript(true);

        //時が動き出す
        Time.timeScale = 1.0f;

        //セレクトに戻る
        SceneManager.LoadScene("Select");
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
        if (timeDirector != null)
        {
            //タイムディレクターのタイムコントローラーを止める
            timeDirector.GetComponent<TimerController>().enabled = stopormove;
        }

        //ジョイスティックディレクター内にあるスクリプト.表示状態　=
        if (joystickDirector != null)
        {
            //ジョイスティックコントローラーのジョイスティックコントローラーを止める
            joystickDirector.GetComponent<JoystickController>().enabled = stopormove;
        }
    }


    //ボタンが押されたとき
    public void OnClick()
    {

       
        //SEの再生
        SoundManager.PlaySE(SoundManager.Sound.SE_Change2D3DButton);

    }
}