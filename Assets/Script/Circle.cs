using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Circle : MonoBehaviour
{
    public float speedInit; //移動速度(初始值:3)
    public float speed;
    public int hpInit;  //打多少下才會死(初始值:5)
    public int hp;
    
    public GameObject powerUp;
    public GameObject freeze;
    public GameObject fire;
    public GameObject split;
    public GameObject saber;
    public GameObject circleBoom;
    float _dir; //朝向中心點的角度(0~360)
    Player player;  //場景中的玩家
    Timer timer;
    
    void Start()
    {
        MoveToScreenOut();
        var startDir = transform.rotation;  
        var dir = new Vector3(0, 0, 0) - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _dir = transform.eulerAngles.z;
        transform.rotation = startDir;
        Changehp();
        timer = FindObjectOfType<Timer>();
    }

    
    void Update()
    {
        move();
        Changehp();
        if (hp <= 0){
                DropPower();
                MakeBoom();
                SoundManager.instance.BoomSound();
                Destroy(this.gameObject);
        }
    }

    void MoveToScreenOut(){  //將怪物移到螢幕外
        int side = Random.Range(0,4);
        float x = -18f;
        float y = -12f;
        if (side == 0){
            y = 12f;
            x = Random.Range(-18.0f, 18.0f);
        }
        if (side == 1){
            y = Random.Range(-12.0f, 12.0f);
            x = 18;
        }
        if (side == 2){
            y = -12f;
            x = Random.Range(-18.0f, 18.0f);
        }
        if (side == 3){
            y = Random.Range(-12.0f, 12.0f);
            x = -18f;
        }
        transform.position = new Vector3(x, y, 0);
    }

    void move(){  //朝向中心點移動
        float x = Mathf.Cos(_dir * Mathf.Deg2Rad);
        float y = Mathf.Sin(_dir * Mathf.Deg2Rad);
        transform.position += new Vector3(x*speed*Time.deltaTime, y*speed*Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D other)  //處理碰撞
    {
        if (other.gameObject.tag == "Laser"){
            hp--;
            if (hp <= 0){
                DropPower();
                MakeBoom();
                SoundManager.instance.BoomSound();
                Destroy(this.gameObject);
            }
            Changehp();
        }
        if (other.gameObject.tag == "LightSaber"){
            DropPower();
            MakeBoom();
            SoundManager.instance.BoomSound();
            Destroy(this.gameObject);
        }
    }

    void Changehp(){  //將文字更新
        var text = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        text.GetComponent<TextMeshProUGUI>().text = hp.ToString();
    }


    void DropPower(){  //機率性掉落道具
        player = FindObjectOfType<Player>();
        int t = Random.Range(0, 100);
        if (t < timer.GetComponent<Timer>().power){  //20%會掉落道具
            int i;
            if (player.GetComponent<Player>()._fireRate > 10){  //如果玩家攻速高於10，掉落PowerUp機率較大
                i = Random.Range(-2,4);
            }else{
                i = Random.Range(-1,4);
            }
            if (i == 0 || i == -1 || i == -2){ 
                if (player.GetComponent<Player>()._fireRate > 10){//如果攻速到頂，掉落Fire
                    Instantiate(powerUp, transform.position, Quaternion.identity); //掉落PowerUp
                }else{
                    Instantiate(fire, transform.position, Quaternion.identity); //掉落Fire
                }
            }
            if (i == 1)Instantiate(freeze, transform.position, Quaternion.identity); //掉落freeze
            if (i == 2)Instantiate(split, transform.position, Quaternion.identity); //掉落split
            if (i == 3)Instantiate(saber, transform.position, Quaternion.identity); //掉落saber
            timer.GetComponent<Timer>().power = 20;
        }else{
            timer.GetComponent<Timer>().power += 2;
        }
    }

    public void LevelUP(){  //血量增加
        if (hp <= 30){
            hp = Mathf.FloorToInt((float)hp*1.1f) + 2;
        }else{
            hp += 2;
        }
        
    }

    public void Plague(){
        InvokeRepeating("_Plague", 0, 0.05f);
    }
    void _Plague(){
        hp--;
    }

    void MakeBoom(){
        Instantiate(circleBoom, transform.position, Quaternion.identity);
    }
}
