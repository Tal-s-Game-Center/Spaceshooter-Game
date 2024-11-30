
# **Spaceship Shooter Game**

## **Description**
The Spaceship Shooter Game is a simple 2D game where the player controls a spaceship to shoot falling objects from the sky. The spaceship can move freely and shoot laser beams in any direction by clicking with the mouse. The game incorporates features like ammo tracking, shooting cooldown, and dynamically updating UI elements for ammo and score.

---

## **Features**
### **1. Shooting in Any Direction**
- The spaceship rotates to face the direction of the mouse click and shoots laser beams accordingly.
- Laser beams are spawned with the correct rotation and velocity.

### **2. Ammo System**
- The spaceship has limited ammo for each stage.
- Ammo is displayed on the left side of the spaceship using a **TextMeshPro** field.
- Each shot reduces the ammo count, and the UI updates dynamically.

### **3. Shooting Cooldown**
- To avoid rapid firing, a cooldown of 0.5 seconds is enforced between consecutive shots.

### **4. Rotational Behavior**
- The spaceship rotates to align with the shooting direction.
- Laser beams are also rotated to match the shooting direction.

---

## **Code Changes**
Here are the changes made to implement the features:

### **1. `NumberField.cs`**
- **Purpose**: Manages and displays numbers (like ammo and score) using a **TextMeshPro** field.
- **Changes**:
  - Added a `Reset` method to reset the field to a default value.
  ```csharp
  public void Reset()
  {
      SetNumber(defaultNumber);
  }
  ```

---

### **2. `LaserShooter.cs`**
- **Purpose**: Handles laser beam spawning and manages spaceship rotation, ammo reduction, and shooting cooldown.
- **Key Additions**:
  - **Rotation Handling**: Ensures the spaceship rotates to face the mouse click direction.
  - **Ammo Management**: Reduces ammo count on each shot and updates the UI.
  - **Cooldown Enforcement**: Integrates the cooldown timer.
  - **Code Highlights**:
    ```csharp
    ammoField.SetNumber(initAmmo);
    ammoField.AddNumber(-1); // Reduce ammo on shooting
    ammoField.transform.rotation = Quaternion.identity; // Keep ammo UI fixed
    ```

    ```csharp
    private void RotateTowardsClick()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -Camera.main.transform.position.z));
        Vector3 direction = worldMousePos - transform.position;

        this.angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 180;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        rotationOfSpawnedObject = Quaternion.Euler(0, 0, angle + 90);
    }
    ```

---

### **3. `ClickSpawner.cs`**
- **Purpose**: Base class for spawning objects, extended by `LaserShooter`.
- **Changes**:
  - Added speed adjustment for spawned objects.
  - Ensured proper rotation of spawned objects (spaceship and laser beams).
  ```csharp
  newObject.transform.rotation = rotationOfSpawnedObject;
  Mover newObjectMover = newObject.GetComponent<Mover>();
  if (newObjectMover)
  {
      newObjectMover.SetVelocity(velocityOfSpawnedObject * speed);
  }
  ```

---

### **4. `Timer.cs`** *(New Class)*
- **Purpose**: Implements a cooldown mechanism for shooting.
- **Highlights**:
  - Tracks time between shots and enforces a cooldown of 0.5 seconds.
  ```csharp
  public bool isCooldownComplete()
  {
      if (Time.time - lastTime >= cooldownInterval)
      {
          lastTime = Time.time; // Reset the cooldown
          return true;
      }
      return false;
  }
  ```

---

## **Game Controls**
- **Mouse Click**: Shoot laser beams in the direction of the cursor.
- **Spaceship Rotation**: The spaceship rotates to face the mouse cursor.

---

## **Game Object Structure**
```
Spaceship
│
├── AmmoField (TextMeshPro)
├── ScoreField (TextMeshPro)
├── LaserShooter (Script)
```

---

## **References**
- **Unity Documentation**:
  - [MonoBehaviour](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html)
  - [Input System](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/index.html)
  - [TextMeshPro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@1.3/manual/index.html)

- **Timer Implementation**:
  - Based on Unity's `Time.time` for efficient cooldown management.

---
