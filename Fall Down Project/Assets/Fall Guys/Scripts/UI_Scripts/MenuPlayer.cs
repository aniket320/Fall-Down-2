﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
   
    void Update()
    {
        transform.eulerAngles +=(new Vector3(0.0f, -Input.GetAxis("Mouse X"),0.0f));
    }
}
