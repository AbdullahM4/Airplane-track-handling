using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GestureController1 : MonoBehaviour
{
    public AirplaneController1 airplaneController;
    
    private XRNode inputSource;
    private InputDevice device;

    private void Start()
    {
        // Set the input source for the controller (left or right)
        inputSource = XRNode.RightHand; // Change to LeftHand if needed
        device = InputDevices.GetDeviceAtXRNode(inputSource);
    }

    void Update()
    {
        if (device.isValid)
        {
            // Detect thumbs up (using a specific button or gesture detection)
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonPressed) && primaryButtonPressed)
            {
                GesturePerformed("ThumbsUp");
            }
            // Detect thumbs down (using a specific button or gesture detection)
            else if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonPressed) && secondaryButtonPressed)
            {
                GesturePerformed("ThumbsDown");
            }
            // Detect fist bump (using a specific button or gesture detection)
            else if (device.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButtonPressed) && gripButtonPressed)
            {
                GesturePerformed("FistBump");
            }
        }
    }

    public void GesturePerformed(string gestureType)
    {
        if (gestureType == "ThumbsUp")
        {
            ThumbsUp();
        }
        else if (gestureType == "ThumbsDown")
        {
            ThumbsDown();
        }
        else if (gestureType == "FistBump")
        {
            FistBump();
        }
    }

    public void ThumbsUp()
    {
        
    }

    public void ThumbsDown()
    {
        
    }

    public void FistBump()
    {

    }
}
