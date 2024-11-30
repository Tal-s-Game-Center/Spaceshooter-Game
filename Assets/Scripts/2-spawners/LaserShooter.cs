using UnityEngine;

/**
 * This component spawns the given laser-prefab whenever the player clicks a given key.
 * It also updates the "scoreText" field of the new laser.
 */
public class LaserShooter : ClickSpawner
{
    [SerializeField]
    [Tooltip("How many points to add to the shooter, if the laser hits its target")]
    int pointsToAdd = 1;

    [SerializeField]
    private Timer timer;

    [SerializeField]
    private int initAmmo = 10;

    // A reference to the field that holds the score that has to be updated when the laser hits its target
    [SerializeField]
    private NumberField scoreField;

    [SerializeField]
    private NumberField ammoField;

    private float angle = 0f;

    private void Start()
    {
        ammoField.SetNumber(initAmmo);
        ammoField.SetDefaultNumber(initAmmo);
    }

    protected override GameObject spawnObject()
    {
        if (timer.isCooldownComplete() && ammoField.GetNumber() > 0)
        {
            RotateTowardsClick();
            SetVelocity();
            ammoField.AddNumber(-1);

            GameObject newObject = base.spawnObject(); // base = super

            // Modify the text field of the new object
            ScoreAdder newObjectScoreAdder = newObject.GetComponent<ScoreAdder>();
            if (newObjectScoreAdder)
            {
                newObjectScoreAdder.SetScoreField(scoreField).SetPointsToAdd(pointsToAdd);
            }
            ammoField.transform.rotation = Quaternion.identity ;
            scoreField.transform.rotation = Quaternion.identity ;
            return newObject;
        }
        else
        {
            return null;
        }
    }

    private void RotateTowardsClick()
    {
       
        // Get mouse position in world coordinates
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -Camera.main.transform.position.z));

        // Calculate the direction from the object to the click position
        Vector3 direction = worldMousePos - transform.position;

        // Calculate the angle in degrees, ignoring the Z axis
        this.angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 180;

        // Apply the rotation to this object
        transform.rotation = Quaternion.Euler(0, 0, angle);
        rotationOfSpawnedObject = Quaternion.Euler(0, 0, angle + 90);
    }

    private void SetVelocity()
    {
        // Convert the angle to radians
        float angleInRadians = (180 + angle) * Mathf.Deg2Rad;

        // Calculate the direction vector based on the angle
        Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        // Apply velocity in the direction of the rotation
        velocityOfSpawnedObject = direction;
    }
}
