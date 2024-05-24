using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public int speed;
    public float timeToChangeDirection;
    private Vector3 direction;

    public enum Direction { ForwardBackward, LeftRight, UpDown }

    public Direction platformDirection = Direction.LeftRight;

    void Start()
    {
        switch (platformDirection)
        {
            case Direction.ForwardBackward:
                direction = Vector3.forward;
                break;
            case Direction.LeftRight:
                direction = Vector3.right;
                break;
            case Direction.UpDown:
                direction = Vector3.up;
                break;
        }

        StartCoroutine(ChangeDirection());
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToChangeDirection);
            direction = -direction;
        }
    }
}