using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float WalkSpeed, RunSpeed, FlySpeed;
    public int Stage = 1;
    public Vector3 speed;
    public float flyingHeight;
    public float maxAngle, rotationspeed;
    public float HorizontalSpeed;
    public float Limitx;
    private Animator anim;
    public GameObject Broom;
    public int PointCount;
    public int PointRequireForLevel2, PointRequireForLevel3;
    private bool takeoff = false,statechanged=false;
    private void Awake()
    {
        anim = GetComponent<Animator>();    
    }


    // Start is called before the first frame update
    void Start()
    {
        if (Stage == 1)
        {
            speed.z = WalkSpeed;
        }
        else if (Stage == 2)
        {
            speed.z = RunSpeed;
        }
        else if (Stage == 3)
        {
            speed.z = FlySpeed;
        }
        Stage = 1;
        PointCount = 0;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        ControlMovement();
        Rotate();
        LimitBounds();
        CheckStage();
        if (Stage == 1)
        {
            speed.z = WalkSpeed;
        }
        else if (Stage == 2)
        {
            speed.z = RunSpeed;
            anim.SetBool("Run", true);
        }
        else if (Stage == 3)
        {
            speed.z = FlySpeed;
        }
        if (takeoff)
        {
            anim.SetBool("Flying", true);
           transform.position= Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, flyingHeight,transform.position.z), Time.deltaTime*1f);
        }
    }

    private void FixedUpdate()
    {
        if (Stage == 1)
        {
            speed.z = WalkSpeed;
        }
        else if (Stage == 2)
        {
            speed.z = RunSpeed;
            anim.SetBool("Run", true);
        }
        else if (Stage == 3)
        {
            speed.z = FlySpeed;
        }else if (Stage == 0)
        {
            speed.z = 0;
        }
        MoveForward();
    }

    void MoveForward()
    {
        rb.MovePosition(rb.position + speed * Time.deltaTime);
    }


    void ControlMovement()
    {
        if (Input.GetMouseButton(0))
        {
            float x = Input.mousePosition.x;
            if (x >= Screen.width / 2 && x < Screen.width)
            {
                speed = new Vector3(HorizontalSpeed, 0f, speed.z);
            }
            else if (x < Screen.width / 2 && x > 0)
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
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, maxAngle, 0), rotationspeed * Time.deltaTime);

        }
        else if (speed.x < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -maxAngle, 0), rotationspeed * Time.deltaTime);

        }
        else if (speed.x == 0)
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), rotationspeed * Time.deltaTime);
        }
    }

    void LimitBounds()
    {

        Vector3 temp = transform.position;
        if (temp.x >= Limitx)
        {
            temp.x = Limitx;
        }
        if (temp.x <= -Limitx)
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

        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            PointCount += 1;

        }
    }

    void CheckStage()
    {
        if (PointCount >=PointRequireForLevel2 && PointCount<PointRequireForLevel3)
        {
            Stage = 2;
        }else if(PointCount>=PointRequireForLevel3 && statechanged==false)
        {
            StartCoroutine(LastStage());
            
        }
    }


    IEnumerator LastStage()
    {
        Stage = 0;
        takeoff = true;
        if (transform.position.y >= 1.5f)
        {
            Broom.SetActive(true);
        }
        if (transform.position.y >= 2f)
        {
            yield return new WaitForSeconds(0.2f);
            Stage = 3;
            statechanged = true;
        }
    }
}
