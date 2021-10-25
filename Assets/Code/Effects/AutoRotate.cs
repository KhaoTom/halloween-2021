using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float startSpeed = 1f;
    public Vector3 startAxis = Vector3.right;
    public bool playOnAwake = true;

    public float speed { get; set; }
    public Vector3 axis { get; set; }
    public bool playing { get; set; }

    private void Awake()
    {
        speed = startSpeed;
        axis = startAxis;
        this.enabled = playOnAwake;
    }

    private void Update()
    {
        transform.Rotate(axis, speed * Time.deltaTime);
    }

    public void Play()
    {
        this.enabled = true;
    }

    public void Stop()
    {
        this.enabled = false;
    }
}
