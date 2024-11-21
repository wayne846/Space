using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bullet;
    
    public int _fireRate; //射速 (初始值:40)

    float fireRate;  //控制射速用
    void Start()
    {
        fireRate = 0;
    }

    void Update()
    {
        LookAtMouse();
        shoot();
    }

    void LookAtMouse(){  //指向滑鼠
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void shoot(){  //如果按下滑鼠或空白鍵，就創造子彈
        if (fireRate > 0){
            fireRate -= Time.deltaTime * 300;
        }else{
            if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)){
                SoundManager.instance.LaserSound();
                Instantiate(bullet, transform.position, Quaternion.identity);
                fireRate = _fireRate;
                
            }
        }
    }
    
}
