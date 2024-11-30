using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This component spawns the given object whenever the player clicks a given key.
 */
public class ClickSpawner : MonoBehaviour
{
    [SerializeField] protected InputAction spawnAction = new InputAction(type: InputActionType.Button);
    [SerializeField] protected GameObject prefabToSpawn;
    [SerializeField] protected Vector3 velocityOfSpawnedObject;
    [SerializeField] protected Quaternion rotationOfSpawnedObject;
    [SerializeField] protected float speed = 5f; // Speed multiplier for velocity

    void OnEnable()
    {
        spawnAction.Enable();
    }

    void OnDisable()
    {
        spawnAction.Disable();
    }

    protected virtual GameObject spawnObject()
    {
        // Step 1: spawn the new object
        Vector3 positionOfSpawnedObject = transform.position; // Spawn at the containing object's position
        GameObject newObject = Instantiate(prefabToSpawn, positionOfSpawnedObject, transform.rotation);
        newObject.transform.rotation = rotationOfSpawnedObject;

        // Step 2: modify the velocity of the new object
        Mover newObjectMover = newObject.GetComponent<Mover>();
        if (newObjectMover)
        {
            newObjectMover.SetVelocity(velocityOfSpawnedObject * speed); // Multiply velocity by speed
        }

        return newObject;
    }

    private void Update()
    {
        if (spawnAction.WasPressedThisFrame())
        {
            spawnObject();
        }
    }
}
