using UnityEngine;

public class Trigger: MonoBehaviour
{
    public Transform door;
    public float speed = 1;
    public float maxOpenValue;

    private float currentValue = 0;
    private bool open = false;
    private bool close = false;

    void Update()
    {
        if (open) OpenDoor();
        if (close) CloseDoor();
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.transform.name == "Player") {
            open = true;
            close = false;
        }
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.transform.name == "Player") {
            open = false;
            close = true;
        }
    }

    void OpenDoor()
    {
        float move = speed * Time.deltaTime;
        currentValue += move;

        if (currentValue <= maxOpenValue) {
            door.position = new Vector3(door.position.x, door.position.y + move, door.position.z);
        } else {
            open = false;
        }
    }

    void CloseDoor()
    {
        float move = speed * Time.deltaTime;
        currentValue -= move;

        if (currentValue >= 0) {
            door.position = new Vector3(door.position.x, door.position.y - move, door.position.z);
        } else {
            close = false;
        }
    }
}
