using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed;  //移動速度(初始值:40)
    public int shootModeInit;
    public int shootMode;
    public GameObject slip;
    
    void Start()
    {
        if (shootMode == 0){
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(slip.transform.position);  //朝向滑鼠
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (shootMode == 1){
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(slip.transform.position);  //朝向滑鼠
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            float t = Random.Range(-15.0f, 15.0f);  //隨機偏移15度
            transform.Rotate(0, 0, t);
        }
        if (shootMode == 2){
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(slip.transform.position);  //朝向滑鼠
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.Rotate(0, 0, 180f);
        }
    }

    
    void Update()
    {
        move();
        EdgeCheck();
    }

    void EdgeCheck(){  //如果超出螢幕外就刪除
        if(transform.position.x > 17 || transform.position.x < -17){
            Destroy(this.gameObject);
        }
        if(transform.position.y > 11 || transform.position.y < -11){
            Destroy(this.gameObject);
        }
    }

    void move(){ //向前移動
        float x = Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
        float y = Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
        transform.position += new Vector3(x*speed*Time.deltaTime, y*speed*Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) //處理碰撞
    {
        if (other.gameObject.tag == "Circle" || other.gameObject.tag == "SpecialCircle"){
            Destroy(this.gameObject);
        }
    }
}
