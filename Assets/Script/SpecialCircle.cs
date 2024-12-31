using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecialCircle : MonoBehaviour
{
    public float speed; //移動速度(初始值:)
    public int hp; //打多少下才會死(初始值:)
    public GameObject half;
    public GameObject plague;
    public GameObject specialCircleBoom;
    
    
    float _dir; //面向的方向(0~360)
    float time;
    Player player;  //場景中的玩家
    
    void Start()
    {
        var startDir = transform.rotation;
        positionSetUp();
        _dir = transform.eulerAngles.z;
        transform.rotation = startDir;
        MoveToScreenOut();
        Changehp();
        time = Time.time;
    }

    
    void Update()
    {
        move();
        if (Time.time - time > 20){
            Destroy(this.gameObject);
        }
    }

    void positionSetUp(){ //設定位置及角度
        int t = Random.Range(0, 4);
        if (t == 0) transform.position = new Vector3(0, 4, 0);
        if (t == 1) transform.position = new Vector3(3, 0, 0);
        if (t == 2) transform.position = new Vector3(0, -4, 0);
        if (t == 3) transform.position = new Vector3(-3, 0, 0);
        int i = Random.Range(0, 2);
        if (t == 0 || t == 2){ //上下兩側
            if (i == 0) transform.Rotate(0, 0, 0);
            if (i == 1) transform.Rotate(0, 0, 180);
        }
        if (t == 1 || t == 3){ //左右兩側
            if (i == 0) transform.Rotate(0, 0, 90);
            if (i == 1) transform.Rotate(0, 0, -90);
        }
        float j = Random.Range(-15.0f, 15.0f); //偏移15度
        transform.Rotate(0, 0, j);
    }

    void MoveToScreenOut(){
        float x = Mathf.Cos(_dir * Mathf.Deg2Rad);
        float y = Mathf.Sin(_dir * Mathf.Deg2Rad);
        transform.position -= new Vector3(x*20, y*20, 0);
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

    void DropPower(){  //掉落道具
        player = FindObjectOfType<Player>();
        int i = Random.Range(0, 2);
        if (i == 0){
            Instantiate(half, transform.position, Quaternion.identity);
        }
        if (i == 1){
            Instantiate(plague, transform.position, Quaternion.identity);
        }
        
    }

    void MakeBoom(){
        Instantiate(specialCircleBoom, transform.position, Quaternion.identity);
    }
}
