using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject circle;
    public GameObject laser;
    public GameObject lightSaber;
    public GameObject timer;

    public float _freezeTime;  //凍結持續時間
    public float _fireTime;  //散射持續時間
    public float _splitTime; //雙向射擊持續時間
    
    float freezeTimer;
    float fireTimer;
    float splitTimer;
    int currentFireRate;  //玩家當前的射速
    bool split;  //雙向射擊是否開啟
    float fireRate;  //控制射速用
    float circleTriggerTimer;
    

    GameObject _Player; //玩家物件
    Player Player;  //玩家腳本

    
    
    void Start()
    {
        _Player = this.gameObject.transform.parent.gameObject;
        Player = _Player.GetComponent<Player>();
        InvokeRepeating("Loop", 0, 0.1f);
        freezeTimer = 0;
        fireTimer = 0;
        currentFireRate = Player._fireRate;
        split = false;
    }

    
    void Update()
    {
        ShootSplit();
    }

    void Loop(){
        if (freezeTimer <= 0){
            circle.GetComponent<Circle>().speed = circle.GetComponent<Circle>().speedInit;
        }
        if (fireTimer <= 0 && splitTimer <= 0){
            Player._fireRate = currentFireRate;
            laser.GetComponent<Laser>().shootMode = 0;
        }
        if (splitTimer <= 0){
            split = false;
        }

        freezeTimer -= 0.1f;
        fireTimer -= 0.1f;
        splitTimer -= 0.1f;

        if(freezeTimer < 0) freezeTimer = 0;
        if(fireTimer < 0) fireTimer = 0;
        if(splitTimer < 0) splitTimer = 0;

        if(Player._fireRate != 3) currentFireRate = Player._fireRate;
    }

    private void OnTriggerEnter2D(Collider2D other)  //處理玩家的碰撞
    {
        if (other.gameObject.tag == "PowerUp"){  //加快射速
            if (Player._fireRate > 10){
                Player._fireRate = (int)Mathf.Floor((float)Player._fireRate * 0.9f);
                currentFireRate = Player._fireRate;
            }
        }
        if (other.gameObject.tag == "Freeze"){  //將怪物變慢
            Freeze();
            freezeTimer += _freezeTime;
        }
        if (other.gameObject.tag == "Fire"){  //超快射速，散射
            Fire();
            fireTimer += _fireTime;
        }
        if (other.gameObject.tag == "Split"){  //雙向射擊
            split = true;
            splitTimer += _splitTime;
        }
        if (other.gameObject.tag == "Half"){  //場上怪物血量減半
            Circle[] Allcircles = FindObjectsOfType<Circle>();
            foreach (var circles in Allcircles){
                circles.GetComponent<Circle>().hp = Mathf.FloorToInt((float)circles.GetComponent<Circle>().hp / 2.0f);
            }
        }
        if (other.gameObject.tag == "Saber"){  //光劍掃兩圈
            Instantiate(lightSaber, transform.position, Quaternion.identity);
        }
        if (other.gameObject.tag == "Plague"){  //場上怪物中毒，快速掉寫
            Circle[] Allcircles = FindObjectsOfType<Circle>();
            foreach (var circles in Allcircles){
                circles.GetComponent<Circle>().Plague();
            }
        }
        if (other.gameObject.tag == "Circle"){  //被怪物碰到時，紀錄碰到的時間
            circleTriggerTimer = Time.time;  
        }
    }

    private void OnTriggerStay2D(Collider2D other) //碰到怪物的時間超過0.2秒，就死亡
    {
        if (other.gameObject.tag == "Circle"){  
            if (Time.time - circleTriggerTimer >= 0.2){
                timer.GetComponent<Timer>().GameOver();
            }
        }
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Circle"){  
            circleTriggerTimer = 999999;
        }
    }

    void Freeze(){
        circle.GetComponent<Circle>().speed = 1;
        Circle[] Allcircles = FindObjectsOfType<Circle>();
        foreach (var circles in Allcircles){
            circles.GetComponent<Circle>().speed = 1;
        }
    }

    void Fire(){
        Player._fireRate = 3;
        laser.GetComponent<Laser>().shootMode = 1;
    }

    void ShootSplit(){
        if(!split){
            return;
        }
        if (fireRate > 0){
            fireRate -= Time.deltaTime * 300;
        }else{
            if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)){
                GameObject Laser =  Instantiate(laser, transform.position, Quaternion.identity);
                Laser.GetComponent<Laser>().shootMode = 2;
                fireRate = currentFireRate;
            }
        }
    }
}
