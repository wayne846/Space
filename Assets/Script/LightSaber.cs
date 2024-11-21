using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : MonoBehaviour
{
    public float speed;
    float dir;

    // Start is called before the first frame update
    void Start()
    {
        dir = 90;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, -1 * speed * Time.deltaTime * 300);
        dir += -1 * speed * Time.deltaTime * 300;
        if (dir < -550){
            Destroy(this.gameObject);
        }
    }
}
