using UnityEngine;
using System.Collections;

public class NearByZoom : MonoBehaviour
{
    Vector3 originalPosition;
    GameObject whiteboard;
    bool is_zooming = false;
    private IEnumerator coroutine;

    // Use this for initialization
    void Start()
    {
        // Grab the original local position of the plane when the app starts.
        originalPosition = this.transform.localPosition;

        //get whiteboard plane which is child of current selection plane
        GameObject whiteboard = transform.FindChild("whiteboard").gameObject;
    }


    //starts a coroutine that runs every 100th of a second
    public IEnumerator do_zoom()
    {
        Vector3 camera_position = Camera.main.gameObject.transform.position;
        Vector3 wall_position = transform.position;

        var dist_between_camera_wall = Vector3.Distance(camera_position, wall_position);

        Vector3 zoom_amount = new Vector3(0.0f, 0.0f, -0.01f);

        /*while (dist_between_camera_wall >= 10f)
        {
            this.transform.position += zoom_amount;
            wall_position = this.transform.position;
            dist_between_camera_wall = Vector3.Distance(camera_position, wall_position);
            //print(dist_between_camera_wall);

            if (transform.position.y <= -2.5)
            {
                this.transform.position += new Vector3(0.0f, 0.01f, 0.0f);
            }

            //TODO: figure out how to change alpha with motion
            //whiteboard.renderer.renderer.m
            //   color.a += 0.1;
            yield return new WaitForSeconds(0.001f);
        }*/

        while (transform.position.y <= -7.15f)
        {
            
           wall_position = this.transform.position;
           dist_between_camera_wall = Vector3.Distance(camera_position, wall_position);
            //print(dist_between_camera_wall);

           
           this.transform.position += new Vector3(0.0f, 0.01f, 0.0f);

            if (dist_between_camera_wall >= 12)
            {
                this.transform.position += zoom_amount;
            }

            //TODO: figure out how to change alpha with motion
            //whiteboard.renderer.renderer.m
            //   color.a += 0.1;
            yield return new WaitForSeconds(0.001f);
        }



    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        // If the plane is selected, move closer
        //print("hello world");
        if (is_zooming == false)
        {
            print("start zooming");
            is_zooming = true;
            StartZoom();

            print("you should be zooming now");
        }
        else
        {
            print("Stop zooming");
            is_zooming = false;
            StopZoom();
            print("you should stop zooming now");
        }

    }

    void StartZoom()
    {
        StartCoroutine(do_zoom());
    }

    void StopZoom()
    {
        //StopCoroutine(coroutine);

        //this.transform.position = this.transform.position;
    }



    void Update()
    {
        /* if (Input.GetKeyDown("space")) {
             Application.LoadLevel("nearby_zoom");
         }*/
    }

}