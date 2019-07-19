using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    //set up camera's movespeed and location
    public float followSpeed;
    public Transform followLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //make a smooth camera transition
        //lerp: a minimum value, a maximum value, and a time value
        float t = followSpeed * Time.deltaTime;
        //move by follow speed
        transform.position = Vector3.Lerp(transform.position, followLocation.position, t);
    }
}
