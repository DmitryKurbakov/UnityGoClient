using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingScript : MonoBehaviour
{

    public static GameObject back;
    public Vector2 speed = new Vector2(2,2);
    public Vector3 direction = new Vector3(-1, 0, 0);
    public bool isLinkedToCamera = false;
    
	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {

        if (transform.localPosition.x < -4500)
        {
            transform.localPosition = new Vector3(9500, 0, 10);
        }

        Vector3 movement = new Vector3(speed.x * direction.x, speed.y * direction.y, 0);

	    movement *= Time.deltaTime;
        transform.Translate(movement);
	   
	}
}
