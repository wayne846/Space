using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    float _dir;
    // Start is called before the first frame update
    void Start()
    {
        var startDir = transform.rotation;
        var dir = new Vector3(0, 0, 0) - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _dir = transform.eulerAngles.z;
        transform.rotation = startDir;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move(){  //朝向中心點移動
        float x = Mathf.Cos(_dir * Mathf.Deg2Rad);
        float y = Mathf.Sin(_dir * Mathf.Deg2Rad);
        transform.position += new Vector3(x*7*Time.deltaTime, y*7*Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player"){
            Destroy(this.gameObject);
        }
    }
}
