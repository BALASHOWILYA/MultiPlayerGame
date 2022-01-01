using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRotateObject : MonoBehaviour
{
    [SerializeField] private float speed = 3;

    private void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime, Space.World);
    }
}
