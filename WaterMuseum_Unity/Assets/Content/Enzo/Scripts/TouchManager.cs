using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour
{
    //public int tapCount;
    public Text tCount;
    GameObject Gobj = null;
    Plane objPlane;
    Vector3 m0;

    Ray GenerateMouseRay(Vector2 touchPos)
    {
        Vector3 mousePosFar = new Vector3(touchPos.x, touchPos.y, Camera.main.farClipPlane);
        Vector3 mousePosNear = new Vector3(touchPos.x, touchPos.y, Camera.main.farClipPlane);

        Vector3 mousePosF = Camera.main.ScreenToWorldPoint(mousePosFar);
        Vector3 mousePosN = Camera.main.ScreenToWorldPoint(mousePosNear);

        Ray mr = new Ray(mousePosN, mousePosF - mousePosN);
        return mr;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Touch.
    }


    void Update()
    {
        tCount.text = Input.touchCount.ToString();


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray mouseRay = GenerateMouseRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit))
                {
                    Gobj = hit.transform.gameObject;
                    objPlane = new Plane(Camera.main.transform.forward * -1, Gobj.transform.position);

                    Ray mRay = Camera.main.ScreenPointToRay(touch.position);
                    float rayDistance;
                    objPlane.Raycast(mRay, out rayDistance);
                    m0 = Gobj.transform.position - mRay.GetPoint(rayDistance);
                }
            }
            else if (touch.phase == TouchPhase.Moved && Gobj)
            {
                Ray mRay = Camera.main.ScreenPointToRay(touch.position);
                float rayDistance;
                if (objPlane.Raycast(mRay, out rayDistance))
                    Gobj.transform.position = mRay.GetPoint(rayDistance) + m0;
            }
            else if (touch.phase == TouchPhase.Ended && Gobj)
            {
                Gobj = null;
            }
        }
    }
}
