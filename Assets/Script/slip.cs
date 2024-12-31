using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slip : MonoBehaviour
{

    public float maxDragDistance;
    Vector2 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = GetComponent<Rigidbody2D>().position;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    void move(){
        if (Input.GetMouseButton(0)){
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 desiredPosition = mousePosition;

            float distance = Vector2.Distance(desiredPosition, startPosition);
            if (distance > maxDragDistance){
                Vector2 _dir = desiredPosition - startPosition;
                _dir.Normalize();
                desiredPosition = startPosition + _dir * maxDragDistance;
            }
            GetComponent<Rigidbody2D>().position = desiredPosition;

            

        }else{
            GetComponent<Rigidbody2D>().position = startPosition;
        }
    }
}
