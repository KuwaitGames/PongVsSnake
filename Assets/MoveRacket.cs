using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRacket : MonoBehaviour {
    public float speed = 30;
    public string axis = "Vertical";
    public bool cpu = false;     //is controlled by cpu?
    public GameObject ball_obj;
    public float diff = 0.0f;
    void Start()
    {
       if(cpu==true)
        {

        }
    }

    void FixedUpdate()
    {
    }

    private void Update()
    {

        if (cpu == false)
        {
            float v = Input.GetAxisRaw(axis);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, v) * speed;
        }
        else
        {
            float ball_y = ball_obj.transform.position.y;
            float my_y = transform.position.y;

            if (ball_y > (my_y + diff))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * speed;
            }
            else if (ball_y < (my_y - diff))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * speed;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0) * speed;
            }
        }
    }



}
