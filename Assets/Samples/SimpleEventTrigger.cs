using UnityEngine;
using UnityEngine.Events;

public class SimpleEventTrigger : MonoBehaviour
{
    // Create a public UnityEvent
    public UnityEvent myEvent;

    // This method can be called from the Inspector
    public void TriggerEvent()
    {
        if (myEvent != null)
        {
            myEvent.Invoke();
        }
    }

    void Start()
    {
            myEvent.Invoke();

    }
}
