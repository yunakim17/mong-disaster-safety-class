using UnityEngine;

public class ValveRotator : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 90f;
    public SceneChanger sceneChanger;

    private bool rotatingLeft = false;
    private bool returningLeft = false;
    private bool rotatingRight = false;

    private float tempLeftAngle = 30f;
    private float rightTargetAngle = -90f;

    private bool valveCompleted = false;

    void Update()
    {
        if (rotatingLeft)
        {
            float step = rotationSpeed * Time.deltaTime;
            target.Rotate(0, 0, step);
            if (GetZAngle() >= tempLeftAngle)
            {
                rotatingLeft = false;
                returningLeft = true;
            }
        }
        else if (returningLeft)
        {
            float step = rotationSpeed * Time.deltaTime;
            float newZ = Mathf.MoveTowardsAngle(GetZAngle(), 0f, step);
            target.localEulerAngles = new Vector3(0, 0, newZ);
            if (Mathf.Approximately(newZ, 0f)) returningLeft = false;
        }
        else if (rotatingRight)
        {
            float step = rotationSpeed * Time.deltaTime;
            float newZ = Mathf.MoveTowardsAngle(GetZAngle(), rightTargetAngle, step);
            target.localEulerAngles = new Vector3(0, 0, newZ);

            if (Mathf.Approximately(newZ, rightTargetAngle))
            {
                rotatingRight = false;
                if (!valveCompleted)
                {
                    valveCompleted = true;
                    Debug.Log("¹ëºê ¼º°ø ¡æ ´ÙÀ½ ¾ÀÀ¸·Î");

                    if (sceneChanger != null)
                    {
                        sceneChanger.Eq_Step4_S3(); 
                    }
                }
            }
        }
    }

    float GetZAngle()
    {
        float z = target.localEulerAngles.z;
        return (z > 180) ? z - 360 : z;
    }

    public void OnRotateLeft()
    {
        rotatingLeft = true;
        rotatingRight = false;
        returningLeft = false;
    }

    public void OnRotateRight()
    {
        rotatingRight = true;
        rotatingLeft = false;
        returningLeft = false;
    }
}
