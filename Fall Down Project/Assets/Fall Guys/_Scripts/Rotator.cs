using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private bool y_direction;
    [SerializeField] private bool Opposite_direction;
    float num;

    private void Start()
    {
        if (Opposite_direction)
            speed *=-1;
    }
    // Update is called once per frame
    void Update()
    {
        if (y_direction)
            transform.Rotate(0f, speed * Time.deltaTime / 0.01f, 0f, Space.Self);
        else
            transform.Rotate(0f, 0f, speed * Time.deltaTime / 0.01f, Space.Self);

	}
}
