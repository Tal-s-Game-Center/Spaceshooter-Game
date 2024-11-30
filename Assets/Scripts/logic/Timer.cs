using UnityEngine;

[System.Serializable]
public class Timer
{
    [SerializeField] private float cooldownInterval = 0.5f; // Serialized for the Inspector
    private float lastTime;

    // Check if the cooldown is complete
    public bool isCooldownComplete()
    {
        if (Time.time - lastTime >= cooldownInterval)
        {
            lastTime = Time.time; // Reset the cooldown
            return true;
        }
        return false;
    }

    public void Reset()
    {
        lastTime = Time.time;
    }
}
