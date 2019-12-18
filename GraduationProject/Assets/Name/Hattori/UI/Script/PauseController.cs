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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
    }

    public void ContinueGame()
    {
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
}
