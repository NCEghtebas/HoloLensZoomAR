using UnityEngine;
using System.Collections;

public class MultipleZoom : MonoBehaviour
{
    Vector3 originalPosition;
    GameObject whiteboard;
    bool is_zooming = false;
    private IEnumerator coroutine;
    int zoomtype = 0;

    // Use this for initialization
    void Start()
    {
        // Grab the original local position of the plane when the app starts.
        originalPosition = this.transform.localPosition;

        //get whiteboard plane which is child of current selection plane
        GameObject whiteboard = transform.FindChild("whiteboard").gameObject;
    }

    void ResetPosition()
    {
        this.transform.position = new Vector3(0.0f, 0.0f, 25.0f);
    }

    //starts a coroutine that runs every 100th of a second
    public IEnumerator do_zoom(float zoom_speed, float min_dist)
    {
        Vector3 camera_position = Camera.main.gameObject.transform.position;
        Vector3 wall_position = transform.position;

        var dist_between_camera_wall = Vector3.Distance(camera_position, wall_position);

        Vector3 zoom_amount = new Vector3(0.0f, 0.0f, -0.01f);

       while (dist_between_camera_wall >= 10.0f)
        {
            this.transform.position += zoom_amount;
            wall_position = this.transform.position;
            dist_between_camera_wall = Vector3.Distance(camera_position, wall_position);
            //print(dist_between_camera_wall)

            //TODO: figure out how to change alpha with motion
            //whiteboard.renderer.renderer.m
            //   color.a += 0.1;
            yield return new WaitForSeconds(zoom_speed);
        }

    }

    void ZoomLogic(float zoom_speed, float min_dist)
    {
        // If the plane is selected, move closer
        //print("hello world");
        if (is_zooming == false)
        {
            print("start zooming");
            is_zooming = true;
            StartZoom(zoom_speed, min_dist);

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


    void SpecialZoomLogic1()
    {
        // If the plane is selected, move closer
        //print("hello world");
        if (is_zooming == false)
        {
            print("start zooming");
            is_zooming = true;
            StartZoom(0.001f, 5.0f);
            StartZoom(0.01f, 10.0f);
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

    void SpecialZoomLogic2()
    {
        // If the plane is selected, move closer
        //print("hello world");
        if (is_zooming == false)
        {
            print("start zooming");
            is_zooming = true;
            StartZoom(0.01f, 5.0f);
            StartZoom(0.001f, 10.0f);

            print("you should be zooming now");
        }
        else
        {
            print("Stop zooming");
            is_zooming = false;
            //StopZoom();
            print("you should stop zooming now");
        }
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        switch (zoomtype)
        {
            case 0:
                print("first slow zoom");
                ZoomLogic(0.001f, 10.0f);
                zoomtype += 1;
                break;
            case 1:
                ResetPosition();
                print("second fast zoom");
                ZoomLogic(0.01f, 10.0f);
                zoomtype += 1;
                break;
            case 2:
                ResetPosition();
                print("third zoom. first slow then fast.");
                SpecialZoomLogic1();
                ResetPosition();
                break;
            case 3:
                ResetPosition();
                print("Forth zoom. First fast then slow.");
                SpecialZoomLogic2();
                ResetPosition();
                break;
            default:
                print("Done cycling through zoom types.");
                break;
        }
    }

    void StartZoom(float zoom_speed, float min_dist)
    {
        StartCoroutine(do_zoom(zoom_speed, min_dist));
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
