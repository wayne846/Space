using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public GameObject circle;
    public GameObject specialCircle;
    public GameObject laser;
    public Text timeScore;
    public GameObject gameoverUI;
    public GameObject tutorialUI;
    public float makeCircleTimeInit;  //生成怪物的時間間隔的初始值(初始值:2)
    public float modeTime;  //切換模式的時間間隔(初始值:10)
    public float levelTime; //增加怪物血量的時間間隔(初始值:30)
    public float timeDownTime;  //減少怪物生成間隔的時間間隔(初始值:20)
    public float makeSpecialCircleTime; //生成特殊怪物的時間間隔(初始值:16)

    public int power; //掉落道具的機率

    float makeCircleTime; //生成怪物的時間間隔，隨時間降低
    float circleTimer = 0;  //生成怪物間隔用，隨時間變化
    float modeTimer = 0; 
    float levelUpTimer = 0;
    float timeDownTimer = 0;
    float specialCircleTimer = 0;

    

    bool mode = false;  //false離峰 true高峰
    
    void Start()
    {
        circle.GetComponent<Circle>().hp = circle.GetComponent<Circle>().hpInit;
        circle.GetComponent<Circle>().speed = circle.GetComponent<Circle>().speedInit;
        laser.GetComponent<Laser>().shootMode = laser.GetComponent<Laser>().shootModeInit;
        InvokeRepeating("TimerLoop", 0, 0.1f);
        makeCircleTime = makeCircleTimeInit;
        modeTimer = modeTime;
        levelUpTimer = levelTime;
        timeDownTimer = timeDownTime;
        specialCircleTimer = makeSpecialCircleTime;
        power = 20;
        StartCoroutine(Tutorial());
    }

    
    void Update()
    {
        timeScore.text = Time.timeSinceLevelLoad.ToString("0");
    }
    void TimerLoop(){ //此函數每0.1秒執行一次
        if (circleTimer <= 0){  //生成怪物
            Makecircle();
            if (mode){
                circleTimer = makeCircleTime/2;
            }else{
                circleTimer = makeCircleTime;
            }
        }
        if (modeTimer <= 0){ //切換模式
            mode = !mode;
            modeTimer = modeTime;
            if (mode && circle.GetComponent<Circle>().hp > 10){
                modeTimer -= 3;
                if (circle.GetComponent<Circle>().hp > 20){
                    modeTimer -= 2;
                }
            }
        }
        if (levelUpTimer <= 0){  //增加血量
            circle.GetComponent<Circle>().LevelUP();
            levelUpTimer = levelTime;
            if (makeCircleTime <= 1.2) levelUpTimer -= 10;
            print("level Up");
        }
        if (timeDownTimer <= 0 && makeCircleTime > 1){ //減少怪物生成間隔
            makeCircleTime -= 0.1f;
            timeDownTimer = timeDownTime;
            print(makeCircleTime);
        }
        if (specialCircleTimer <= 0 && circle.GetComponent<Circle>().hp >= 14){  //生成特殊怪物
            MakeSpecialCircle();
            specialCircleTimer = makeSpecialCircleTime; //16秒
            if (circle.GetComponent<Circle>().hp > 24)specialCircleTimer -= 6; //10秒
            if (circle.GetComponent<Circle>().hp > 30)specialCircleTimer -= 2; //8秒
            if (circle.GetComponent<Circle>().hp > 35)specialCircleTimer -= 1; //7秒
            if (circle.GetComponent<Circle>().hp > 40)specialCircleTimer -= 1; //6秒
        }

        specialCircleTimer -= 0.1f;
        timeDownTimer -= 0.1f;
        levelUpTimer -= 0.1f;
        modeTimer -= 0.1f;
        circleTimer -= 0.1f;
    }

    void Makecircle(){
        Instantiate(circle, transform.position, Quaternion.identity);
    }

    void MakeSpecialCircle(){
        Instantiate(specialCircle, transform.position, Quaternion.identity);
    }

    public void Restart(){  //重置遊戲
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void Quit(){  //退出遊戲
        Application.Quit();
    }

    public void GameOver(){  //玩家死亡時
        gameoverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    IEnumerator Tutorial(){
        yield return new WaitForSeconds(5);
        tutorialUI.SetActive(false);
    }
}
