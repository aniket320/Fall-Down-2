using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    [SerializeField] float rotspeed = 1f;
    void Update()
    {
      transform.eulerAngles += (new Vector3(0, FixedTouchField.instance.TouchDist.x * Time.deltaTime * -rotspeed,0));

    }
}
