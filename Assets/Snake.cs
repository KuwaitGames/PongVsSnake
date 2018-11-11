using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Snake : MonoBehaviour {

    Vector2 dir = Vector2.right;
    public bool test_mode = false;
    List<Transform> tail = new List<Transform>();
    public int start_len;
    public float max_speed = 3.0f;
    public int max_len = 20;

    public float snake_speed = 1.0f;
    // Did the snake eat something?
    public bool ate = false;
  //  bool lost = false;

    // Tail Prefab
    public GameObject tailPrefab;

    // Use this for initialization
    void Start()
    {
        // Move the Snake every 300ms
        if(start_len>=max_len)
        {
            start_len = max_len;
        }
        Vector2 v = transform.position;
        for (int i = 1; i <= start_len;i++) {
            v.x = v.x + 1;
            GameObject g = (GameObject)Instantiate(tailPrefab,
                                                v,
                                                Quaternion.identity);

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);
        }
        InvokeRepeating("Move", 0.1f, (0.1f/snake_speed));
    }
	
	// Update is called once per frame
	void Update () {
        // Move in a new Direction?
        if (Input.GetKey(KeyCode.RightArrow) && (dir!= Vector2.left))
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
        if (ate)
        {
            print("eating");
            start_len++;
            if (start_len >= max_len)
            {
                start_len = max_len;
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
                snake_speed = snake_speed + 0.1f;
                InvokeRepeating("Move", 0.1f, (0.1f / snake_speed));

            }
            // Reset the flag
            ate = false;
        }
        // Do we have a Tail?
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
            // Get longer in next Move call
            ate = true;
            print("ate");
            // Remove the Food
            Destroy(coll.gameObject);
        }
        // Collided with Tail or Border
        else if(coll.name.StartsWith("Border") || coll.name.StartsWith("Tail"))
        {
            print("hit wall/self");
           // lost = true;
            if(test_mode==false){
                CancelInvoke();
            }
        }
    }


}
