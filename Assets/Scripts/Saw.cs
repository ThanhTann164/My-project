using UnityEngine;

public class Saw : MonoBehaviour
{
    float speed = 2.0f;
    int direction = 1;
    public Transform RightCheck;
    public Transform LeftCheck;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);
        if (Physics2D.Raycast(RightCheck.position, Vector2.down, 2) == false)
        {
            direction = -1;
        }
        if (Physics2D.Raycast(LeftCheck.position, Vector2.down, 2) == false)
        {
            direction = 1;
        }
    }
}
