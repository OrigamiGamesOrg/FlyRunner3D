using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform target;
    public float distance, height;
    public float HeightDamping, RotationDamping;
    public float skyboxRotateSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float WantedRotationAngle = target.eulerAngles.y;
        float WantedHeight = target.position.y + height;

        float CurrentRotationAngle = transform.eulerAngles.y;
        float CurrentHeight = transform.position.y;

        CurrentRotationAngle = Mathf.LerpAngle(CurrentRotationAngle,WantedRotationAngle,RotationDamping * Time.deltaTime);
        CurrentHeight = Mathf.Lerp(CurrentHeight,WantedHeight,HeightDamping * Time.deltaTime);

        Quaternion CurrentRotation = Quaternion.Euler(0f, CurrentRotationAngle, 0f);
        transform.position = target.position;
        transform.position -= CurrentRotation * Vector3.forward * distance;
        transform.position = new Vector3(transform.position.x, CurrentHeight, transform.position.z);

    }

    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyboxRotateSpeed);
    }
}
