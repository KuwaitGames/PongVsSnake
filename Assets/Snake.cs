using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Snake : MonoBehaviour {

    Vector2 dir = Vector2.right;
    public bool test_mode = false;
    List<Transform> tail = new List<Transform>();
    public int start_len;
    private int snake_len;
    public float max_speed = 3.0f;
    public int max_len = 20;
    public GameObject camera_obj;
    private Game_script game_scr;
    public int num_eat = 1; //number of length increase every eat
    private int num_eat_counter;
    public float snake_speed = 1.0f;
    private float faster_speed;
    // Did the snake eat something?
    public string hitBy_type;
  //  bool lost = false;

    // Tail Prefab
    public GameObject tailPrefab;

    // Use this for initialization
    void Start()
    {
        hitBy_type = "none";
        game_scr = (Game_script)camera_obj.GetComponent<Game_script>();
        snake_len = start_len;
        faster_speed = snake_speed;
        CancelInvoke();
        // Move the Snake every 300ms
        if (snake_len >= max_len)
        {
            snake_len = max_len;
        }
        Vector2 v = transform.position;
        for (int i = 1; i <= snake_len; i++)
        {
            v.x = v.x + 1;
       //     Transform g = (((GameObject)Instantiate(tailPrefab,
                      //                          v,
                        //                            Quaternion.identity)).transform);

            // Keep track of it in our tail list
            tail.Insert(0, (((GameObject)Instantiate(tailPrefab,
                                                v,
                                                    Quaternion.identity)).transform));
        }
        InvokeRepeating("Move", 0.1f, (0.1f / faster_speed));
        num_eat_counter = num_eat;

    }

    // Update is called once per frame
    void Update () {
        // Move in a new Direction?
        if (((Input.GetKey(KeyCode.RightArrow))||(Input.GetKey(KeyCode.JoystickButton10))) && (dir!= Vector2.left))
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow) && (dir != Vector2.up))
            dir = -Vector2.up;    // '-up' means 'down'
        else if (Input.GetKey(KeyCode.LeftArrow) && (dir != Vector2.right))
            dir = -Vector2.right; // '-right' means 'left'
        else if (Input.GetKey(KeyCode.UpArrow) && (dir != Vector2.down))
            dir = Vector2.up;
    }

    void Move()
    {
        // Save current position (gap will be here)
        Vector2 v = transform.position;

        // Move head into new direction (now there is a gap)

        // Ate something? Then insert new Element into gap
        if (hitBy_type=="white")   //white ball makes snake longer
        {
            print("eating");
            snake_len++;
            if (snake_len >= max_len)
            {
                snake_len = max_len;
            }
            else
            {

                CancelInvoke();
                transform.Translate(dir);
                    // Load Prefab into the world
                GameObject g = (GameObject)Instantiate(tailPrefab,
                                                          v,
                                                          Quaternion.identity);
                    // Keep track of it in our tail list
                tail.Insert(0, g.transform);
                faster_speed = faster_speed + 0.1f;
                InvokeRepeating("Move", 0.1f, (0.1f / faster_speed));

            }
            // Reset the flag after required number of increases
            if(num_eat_counter==0){
                hitBy_type = "none";
                num_eat_counter = num_eat;
            }
            else{
                num_eat_counter--;
            }
        }else if(hitBy_type =="red"){    //red ball makes snake die
            killSnake();

        }
        else if (tail.Count > 0)
        {
            transform.Translate(dir);

            // Move last Tail Element to where the Head was
            tail.Last().position = v;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        print("trigger:"+coll.name);

        // Food?
        if (coll.name.StartsWith("foodPrefab"))
        {
        /*    // Get longer in next Move call
            ate = true;
            print("ate");
            // Remove the Food
            Destroy(coll.gameObject);*/
        }
        // Collided with Tail or Border
        else if(coll.name.StartsWith("Border") || coll.name.StartsWith("Racket"))
        {
            killSnake();

        }
        else if(coll.name.StartsWith("Tail")){
            killSnake();
        }
    }

    private void killSnake()
    {
        print("snake dies");
        // lost = true;
        if (test_mode == false)
        {
            CancelInvoke();
        }
        game_scr.game_hit("snake");
    }

    public void resetSnake()
    {
        num_eat_counter = 0;
        dir = Vector2.right;
        hitBy_type = "none";

        GetComponent<Rigidbody2D>().MovePosition(new Vector2(1, 0));
        CancelInvoke();
        faster_speed = snake_speed;
        if (tail.Count > start_len)
        {
            for (int p = start_len;p<(tail.Count-start_len);p++)
            {
                GameObject g = tail.ElementAt<Transform>(p).gameObject;
                tail.RemoveAt(p);
                Destroy(g);
            }
           // tail.RemoveRange(start_len, (tail.Count - start_len));
        }
        print("tail count=" + tail.Count);
        for (int i = 0; i < tail.Count; i++)
        {
            tail.ElementAt<Transform>(i).SetPositionAndRotation(new Vector2(-1*i, 0), Quaternion.identity);
            
        }

      // print("snake_len=" + snake_len + " start_len=" + start_len + " count=" + tail.Count);

        snake_len = start_len;

        InvokeRepeating("Move", 0.1f, (0.1f / faster_speed));
    }


}
