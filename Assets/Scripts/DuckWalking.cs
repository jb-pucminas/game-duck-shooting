using UnityEngine;

public class DuckWalking: MonoBehaviour
{
    public float speed = 2f;
    public bool direction = true;
    
    [InspectorName("Flying Configs")]
    public bool flying = false;
    public float flyingHeight = 2.0f;
    public float flyingFrequency = 2.0f;

    void Update()
    {
        int dir = (direction) ? 1 : -1;
        float dirX = transform.position.x + speed * Time.deltaTime * dir;

        if (flying == true)
        {
            float dirY = Mathf.Sin(Time.time * flyingFrequency) * flyingHeight;
            transform.position = new Vector3(dirX, dirY, transform.position.z);
        } 
        else
        {
            transform.position = new Vector3(dirX, transform.position.y, transform.position.z);
        }

        // NOTE: Reset position
        if (direction == true && transform.position.x >= Configs.MAX_HORIZONTAL)
        {
            transform.position = new Vector3(Configs.MIN_HORIZONTAL, transform.position.y, transform.position.z);
        }

        if (direction == false && transform.position.x <= Configs.MIN_HORIZONTAL)
        {
            transform.position = new Vector3(Configs.MAX_HORIZONTAL, transform.position.y, transform.position.z);
        }

    }
}
