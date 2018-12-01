using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float start_speed = 30;
    public bool test_mode = false;
    public GameObject snk_obj;
    private Snake snk_scpt;
    public GameObject camera_obj;
    private Game_script game_scrt;
    private string ball_type;
    private float speed;

    void Start()
    {
        speed = start_speed;
        // Initial Velocity
        game_scrt = camera_obj.GetComponent<Game_script>();

        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        setBallType("white");
    }


    float hitFactor(Vector2 ballPos, Vector2 racketPos,
                float racketHeight)
    {
        // ascii art:
        // ||  1 <- at the top of the racket
        // ||
        // ||  0 <- at the middle of the racket
        // ||
        // || -1 <- at the bottom of the racket
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Note: 'col' holds the collision information. If the
        // Ball collided with a racket, then:
        //   col.gameObject is the racket
        //   col.transform.position is the racket's position
        //   col.collider is the racket's collider

        print("Ball: " + col.gameObject.name);

        // Hit the left Racket?
        if (col.gameObject.name == "RacketLeft")
        {
            // Calculate hit Factor
            float y = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(1, y).normalized;

            // Set Velocity with dir * speed
            speed = speed + 2;
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }

        // Hit the right Racket?
        if (col.gameObject.name == "RacketRight")
        {
            // Calculate hit Factor
            float y = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(-1, y).normalized;
            speed = speed + 2;
            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }

        if (col.gameObject.name.StartsWith("Head"))
        {
            print("ball hits head");
            snk_scpt = snk_obj.GetComponent<Snake>();
            snk_scpt.hitBy_type = ball_type;
        }

        if (col.gameObject.name.StartsWith("TailPrefab"))
        {
            print("ball hits tail");
            snk_scpt = snk_obj.GetComponent<Snake>();
            snk_scpt.hitBy_type = ball_type;
        }

        if (col.gameObject.name.StartsWith("BorderLeft"))
        {
            print("ball hits BorderLeft");
            if (test_mode == false)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.right * 0;
            }

           game_scrt.game_hit("ping");


        }

        if (col.gameObject.name.StartsWith("BorderRight"))
        {
            print("ball hits BorderRight");
            if (test_mode == false)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.right * 0;

            }
            game_scrt.game_hit("pong");

        }
    }

    public void resetBall()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Rigidbody2D>().position = new Vector2(-25, -3);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        if(ball_type=="white"){
            ball_type = "red";
        }else{
            ball_type = "white";
        }
        setBallType(ball_type);

    }


    void setBallType(string typeIn)
    {
        speed = start_speed;
        if (typeIn=="white"){
            ball_type = "white";
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        if(typeIn=="red"){
            ball_type = "red";
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

}
