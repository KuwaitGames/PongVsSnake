using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_script : MonoBehaviour {


    public GameObject ping_label;
    public GameObject gameover_label;
    public int ping_score = 0;
    public GameObject pong_label;
    public int pong_score = 0;
    public GameObject snake_label;
    public int snake_score = 0;
    public GameObject snake_obj;
    public GameObject RacketL_obj;
    public GameObject RacketR_obj;
    public GameObject Ball_obj;
    public GameObject snk_obj;
    private bool game_ended;
    public int winning_Score = 7;   //points needed to lose
    private Snake snk_scrt;
    private Ball ball_scrt;

    // Use this for initialization
    void Start () {
        game_ended = false;
        snk_scrt = snk_obj.GetComponent<Snake>();
        ball_scrt = Ball_obj.GetComponent<Ball>();
        gameover_label.GetComponent<Text>().text = "";

    }

    // Update is called once per frame
    public void game_hit(string player)
    {
        StartCoroutine(Delay());

        if (player=="ping"){
            ping_score++;
            ping_label.GetComponent<Text>().text = "" + ping_score;
        }else if(player=="pong"){
            pong_score++;
            pong_label.GetComponent<Text>().text = "" + pong_score;
        }
        else if(player=="snake"){
            snake_score++;
            snake_label.GetComponent<Text>().text = "" + snake_score;
        }
        else{
            //error
        }

        //reset racket positions
        RacketL_obj.GetComponent<Rigidbody2D>().MovePosition(new Vector2(-26, -3));
        RacketR_obj.GetComponent<Rigidbody2D>().MovePosition(new Vector2(23, 5));
        // print(Time.time);




    }

    private void checkGameover()
    {
        //check gameover
        if(ping_score >= winning_Score || pong_score >= winning_Score){
            //ping loses
            gameOver("Snake");
        }
        else if(snake_score >= winning_Score){
            //snake loses
            gameOver("Ping Pong");
        }
    }

    private void gameOver(string strIn)
    {
        gameover_label.GetComponent<Text>().text = "Game Over\n"+strIn +" wins";
        game_ended = true;
    }


    private IEnumerator Delay()
    {
        print(Time.time);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1.0f);
        print(Time.time);

        checkGameover();
        if(game_ended==false){
            ball_scrt.resetBall();
            snk_scrt.resetSnake();
            Time.timeScale = 1;
        }

    }
}
