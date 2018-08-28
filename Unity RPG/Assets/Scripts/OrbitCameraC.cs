using UnityEngine;
using System.Collections;

public class OrbitCameraC : MonoBehaviour
{
    //public Transform target;
    //public float targetHeight = 1.2f;
    //public float distance = 4.0f;
    //public float maxDistance = 6;
    //public float minDistance = 1.0f;
    //public float xSpeed = 250.0f;
    //public float ySpeed = 120.0f;
    //public float yMinLimit = -10;
    //public float yMaxLimit = 70;
    //public float zoomRate = 80;
    //public float rotationDampening = 3.0f;
    //private float x = 20.0f;
    //private float y = 32.0f;
    //public bool lockOn = false;
    //public bool freeze = false;

    ////Transform attackPoint;   

    //void Start()
    //{
    //    if (!target)
    //    {
    //        target = GameObject.FindWithTag("Character").transform;
    //    }
    //    Vector3 angles = transform.eulerAngles;
    //    x = angles.y;
    //    y = 32.0f;
    //}

    //void LateUpdate()
    //{
    //    if (!target)
    //        return;

    //    if (Time.timeScale == 0.0f)
    //    {
    //        return;
    //    }
    //    if (Input.GetButton("Fire2"))
    //    {
    //        x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
    //        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
    //    }

    //    distance -= (Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
    //    distance = Mathf.Clamp(distance, minDistance, maxDistance);

    //    y = ClampAngle(y, yMinLimit, yMaxLimit);

    //    Quaternion rotation = Quaternion.Euler(y, x, 0);
    //    transform.rotation = rotation;

    //    Vector3 position = target.position - (rotation * Vector3.forward * distance + new Vector3(0, -targetHeight, 0));
    //    transform.position = position;


    //    Vector3 trueTargetPosition = target.transform.position - new Vector3(0, -targetHeight, 0);

    //}

    //static float ClampAngle(float angle, float min, float max)
    //{
    //    if (angle < -360)
    //        angle += 360;
    //    if (angle > 360)
    //        angle -= 360;
    //    return Mathf.Clamp(angle, min, max);

    //}    
    public Transform target;
    public float distance = 10f;

    public float xSpeed = 250f;
    public float ySpeed = 120f;

    public int yMinLimit = -20;
    public int yMaxLimit = 80;

    public float xOffset;
    public float yOffset;

    float x;
    float y;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
       // target = BattleSceneManager.getInstance().Character[0].transform;
    }

    void LateUpdate()
    {
        //if (BattleSceneManager.getInstance().Character[0] == null && BattleSceneManager.getInstance().CharacterCount == 2)
        //{
        //    BattleSceneManager.getInstance().mainCharacter = BattleSceneManager.getInstance().Character[1];
        //    Debug.Log("사망후 캐릭터전환");
        //    target = BattleSceneManager.getInstance().Character[1].transform;
        //}
        //if (BattleSceneManager.getInstance().Character[1] == null && BattleSceneManager.getInstance().CharacterCount == 2)
        //{
        //    BattleSceneManager.getInstance().mainCharacter = BattleSceneManager.getInstance().Character[2];
        //    Debug.Log("사망후 캐릭터전환");
        //    target = BattleSceneManager.getInstance().Character[2].transform;
        //}
        //if (BattleSceneManager.getInstance().Character[2] == null && BattleSceneManager.getInstance().CharacterCount == 2)
        //{
        //    BattleSceneManager.getInstance().mainCharacter = BattleSceneManager.getInstance().Character[0];
        //    Debug.Log("사망후 캐릭터전환");
        //    target = BattleSceneManager.getInstance().Character[0].transform;
        //}
        //if (BattleSceneManager.getInstance().Character[0] == null && BattleSceneManager.getInstance().Character[1] == null)
        //{
        //    BattleSceneManager.getInstance().mainCharacter = BattleSceneManager.getInstance().Character[2];
        //    Debug.Log("사망후 캐릭터전환");
        //    target = BattleSceneManager.getInstance().Character[2].transform;
        //}
        //if (BattleSceneManager.getInstance().Character[0] == null && BattleSceneManager.getInstance().Character[2] == null)
        //{
        //    BattleSceneManager.getInstance().mainCharacter = BattleSceneManager.getInstance().Character[1];
        //    Debug.Log("사망후 캐릭터전환");
        //    target = BattleSceneManager.getInstance().Character[1].transform;
        //}
        //if (BattleSceneManager.getInstance().Character[1] == null && BattleSceneManager.getInstance().Character[2] == null)
        //{
        //    BattleSceneManager.getInstance().mainCharacter = BattleSceneManager.getInstance().Character[0];
        //    Debug.Log("사망후 캐릭터전환");
        //    target = BattleSceneManager.getInstance().Character[0].transform;
        //}
        if (target != null)
        {
            y = ClampAngle(y, yMinLimit, yMaxLimit);
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(xOffset, yOffset, -distance) + target.position;
            transform.rotation = rotation;
            transform.position = position;
        }
       
        if (Input.GetKeyDown(KeyCode.F1)&& BattleSceneManager.getInstance().Character[0]!=null)
        {
            Debug.Log("1번");
            target = BattleSceneManager.getInstance().Character[0].transform;
            BattleSceneManager.getInstance().mainCharacter = BattleSceneManager.getInstance().Character[0];
            BattleSceneManager.getInstance().CameraOne();
        }
        if (Input.GetKeyDown(KeyCode.F2) && BattleSceneManager.getInstance().Character[1] != null)
        {
            Debug.Log("2번");
            target = BattleSceneManager.getInstance().Character[1].transform;
            BattleSceneManager.getInstance().mainCharacter = BattleSceneManager.getInstance().Character[1];
            Debug.Log(BattleSceneManager.getInstance().mainCharacter.name);
            BattleSceneManager.getInstance().CameraTwo();
        }
        if (Input.GetKeyDown(KeyCode.F3) && BattleSceneManager.getInstance().Character[2] != null)
        {
            Debug.Log("3번");
            target = BattleSceneManager.getInstance().Character[2].transform;
            BattleSceneManager.getInstance().mainCharacter = BattleSceneManager.getInstance().Character[2];
            Debug.Log(BattleSceneManager.getInstance().mainCharacter.name);
            BattleSceneManager.getInstance().CameraThree();
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

}