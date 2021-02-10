using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float radiusMain = 10f;
    public Transform finish;
    public GameObject arrowPin;

    public float power = 10f;

    public Camera cam;
    public LineRenderer line;
    public GameObject forcePoint;
    LineRenderer arrow;

    Vector3 force;
    Rigidbody rb;
    Vector3 startPoint;
    Vector3 endPoint;
    Vector3 currentPoint;

    Vector3 myPos = Vector3.zero;

    bool pressed = false;
    bool draw = false;
    GameObject arrowObj;

    void Start()
    {
        rb = forcePoint.GetComponent<Rigidbody>();
        arrow = GetComponent<LineRenderer>();
        arrowObj = Instantiate(arrowPin);
        arrowObj.SetActive(false);
    }

    void Update()
    {

        UIManager.Instance.CompletedProc(100 - 100 * Vector3.Distance(forcePoint.transform.position, finish.position) / Vector3.Distance(GameManager.Instance.startPos.position, finish.position));
        UIManager.Instance.ChangeTrafficLight(rb.IsSleeping());
        if (transform.position.y < -10f)
        {
            rb.velocity = Vector3.zero;
            forcePoint.transform.position = myPos;
        }
        if (rb.IsSleeping())
        {
            if (Input.GetMouseButtonDown(0))
            {
                endPoint = Vector3.zero;
                pressed = true;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    startPoint = hit.point;
                }

            }
            if (Input.GetMouseButton(0) && pressed)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (startPoint.y - hit.point.y > 0)
                    {
                        endPoint = hit.point;
                        draw = true;
                    }
                }
                if (draw)
                    DrawLine(startPoint, endPoint);
            }
            if (Input.GetMouseButtonUp(0) && draw)
            {
                draw = false;
                pressed = false;
                myPos = forcePoint.transform.position;
                DestroyLine();
                force = Vector3.ClampMagnitude((startPoint - endPoint), 10f) * power;
                force.y = 0;
                startPoint = Vector3.zero;
                endPoint = Vector3.zero;
                rb.AddForce(force * power, ForceMode.Impulse);
            }
        }

    }

    void DrawLine(Vector3 start, Vector3 end)
    {

        /*line.positionCount = 2;
        line.SetPosition(0, start);
        line.SetPosition(1, end);*/

        arrow.positionCount = 2;

        arrow.SetPosition(0, forcePoint.transform.position);
        arrow.SetPosition(1, forcePoint.transform.position + Vector3.ClampMagnitude((start - end), 10f) / 3);

        arrowObj.transform.position = forcePoint.transform.position + Vector3.ClampMagnitude((start - end), 10f) / 3;
        arrowObj.transform.LookAt(forcePoint.transform.position);
        arrowObj.transform.Rotate(0, 180, 0);

        UIManager.Instance.tensionPower(Mathf.Clamp(100 * Vector3.Distance(startPoint, endPoint) / 10f, 0, 100));
        arrowObj.SetActive(true);
    }

    void DestroyLine()
    {
        UIManager.Instance.tensionPower(0);
        arrowObj.SetActive(false);
        line.positionCount = 0;
        arrow.positionCount = 0;
    }
}
