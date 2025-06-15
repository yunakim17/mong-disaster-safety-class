using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteUser : MonoBehaviour
{
    void Start()
    {
        // uuid 삭제 (개발용)
        PlayerPrefs.DeleteKey("uuid");
    }
}
