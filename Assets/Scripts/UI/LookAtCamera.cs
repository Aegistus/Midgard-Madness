using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Camera main;

    private void Start()
    {
        main = Camera.main;
    }

    void Update()
    {
        transform.LookAt(main.transform);
    }
}
