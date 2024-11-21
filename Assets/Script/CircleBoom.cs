using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBoom : MonoBehaviour
{
    // Start is called before the first frame update
    float count;

    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("Boom", 0, 0.01f);
    }

    void Boom(){
        transform.localScale += new Vector3(0.01f, 0.01f, 0);
        this.gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.01f);
        count += 0.01f;
        if (count > 200) Destroy(this.gameObject);
    }
}
