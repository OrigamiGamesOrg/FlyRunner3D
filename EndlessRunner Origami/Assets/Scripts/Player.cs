using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 speed;
    public float maxAngle, rotationspeed;
    public float HorizontalSpeed;
    public float Limitx;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ControlMovement();
        Rotate();
        LimitBounds();
    }

    private void FixedUpdate()
    {
        MoveForward();
    }

    void MoveForward()
    {
        rb.MovePosition(rb.position + speed*Time.deltaTime);
    }


    void ControlMovement()
    {
        if (Input.GetMouseButton(0))
        {
            float x = Input.mousePosition.x;
            if(x >=Screen.width/2 && x < Screen.width)
            {
                speed = new Vector3(HorizontalSpeed, 0f, speed.z);
            }else if(x<Screen.width/2 && x > 0)
            {
                speed = new Vector3(-HorizontalSpeed, 0f, speed.z);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            speed = new Vector3(0, 0, speed.z);
        }
    }

    void Rotate()
    {
        if (speed.x > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,maxAngle,0), rotationspeed * Time.deltaTime);

        }else if (speed.x < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -maxAngle, 0), rotationspeed * Time.deltaTime);

        }
        else if (speed.x == 0)
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0, 0), rotationspeed * Time.deltaTime);
        }
    }

    void LimitBounds()
    {
        
        Vector3 temp = transform.position;
        if (temp.x >= Limitx)
        {
            temp.x = Limitx;
        }if (temp.x <= -Limitx)
        {
            temp.x = -Limitx;
        }

        transform.position = temp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            SceneManager.LoadScene("Lose");
        }
    }
}
