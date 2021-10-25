using UnityEngine;
using UnityEngine.Events;

public class TransformWarper : MonoBehaviour
{
    public float startSpeed = 10f;
    public Transform startSubject;
    public Transform startTarget;
    public bool playOnAwake = false;

    public float speed { get; set; }
    public Transform subject { get; set; }
    public Transform target { get; set; }
    public bool completed { get; private set; }
    public float elapsed { get; private set; }

    public UnityEvent onStartWarp;
    public UnityEvent onEndWarp;
    public UnityEvent<float> onWarpTime;


    private void Awake()
    {
        speed = startSpeed;
        target = startTarget;
        subject = startSubject;
        this.enabled = playOnAwake;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector3.Distance(subject.position, target.position) < 0.1f)
        {
            EndWarp();
        }
        else
        {
            transform.position = Vector3.MoveTowards(subject.position, target.position, speed * Time.deltaTime);
            elapsed += Time.deltaTime;
            onWarpTime.Invoke(elapsed);
        }
    }

    public void StartWarp()
    {
        if (subject != null && target != null)
        {
            elapsed = 0f;
            onStartWarp.Invoke();
            this.enabled = true;
        }
    }

    public void EndWarp()
    {
        onEndWarp.Invoke();
        completed = true;
        this.enabled = false;
    }
}
