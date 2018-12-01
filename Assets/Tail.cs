using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour {

    public GameObject head_obj;
    private Snake snk_scpt2;

    void OnTriggerEnter2D(Collider2D coll)
    {
        print("tail trigger: "+ coll.name);

        // Food?
        if (coll.name.StartsWith("Ball"))
        {
         //  print("tail trigger by ball");
            //// Get longer in next Move call
           // snk_scpt2 = head_obj.GetComponent<Snake>();
           // snk_scpt2.ate = true;

        }
        // Collided with Tail or Border
        if (coll.name.StartsWith("Head"))
        {
            print("stop snake");
          //  snk_scpt2 = head_obj.GetComponent<Snake>();
         //   snk_scpt2.CancelInvoke();
        }
    }
}
