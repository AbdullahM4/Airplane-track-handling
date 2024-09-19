using UnityEngine;

public class GestureController : MonoBehaviour
{
    public AirplaneController airplaneController;

    void Update()
    {
        // This should be connected to your gesture detection system.
        // For example, calling GesturePerformed("ThumbsUp") when the thumbs up gesture is detected.
    }

    public void GesturePerformed(string gestureType)
    {
        if (airplaneController == null)
        {
            Debug.LogError("AirplaneController is not assigned.");
            return;
        }

        if (gestureType == "ThumbsUp")
        {
            ThumbsUp();
        }
        else if (gestureType == "ThumbsDown")
        {
            ThumbsDown();
        }
    }

    public void ThumbsUp()
    {
        
    }

    public void ThumbsDown()
    {
        
    }
}
