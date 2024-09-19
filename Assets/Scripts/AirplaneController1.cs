using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AirplaneController1 : MonoBehaviour
{
    public BezierCurve bezierCurve; // Reference to the BezierCurve script
    public float speed = 5f; // Movement speed
    public float turnSpeed = 2f; // Rotation speed
    private float t = 0f; // Parameter for the Bezier curve

    private bool shouldMove = false;
    private bool engineOnFire = false; // Track engine fire state

    // UI Panels
    public GameObject winPanel;
    public GameObject losePanel;
    public Button readyButton; // Reference to the Ready button

    // Timer for losing condition
    private float timer = 0f;
    private float loseTimeLimit = 60f; // 1 minute

    // Reference to the SimpleEventTrigger
    public SimpleEventTrigger simpleEventTrigger;

    // Reference to the AudioManager
    public AudioManager audioManager;

    void Start()
    {
        if (bezierCurve == null)
        {
            Debug.LogError("BezierCurve not assigned!");
            return;
        }

        // Ensure UI panels are inactive at the start
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

        // Assign the Ready button and add a listener
        if (readyButton != null)
        {
            readyButton.onClick.AddListener(StartLevel);
        }

        // Register the event listener
        if (simpleEventTrigger != null)
        {
            simpleEventTrigger.myEvent.AddListener(MoveToLastPoint);
        }
    }

    void Update()
    {
        if (shouldMove)
        {
            MoveAlongCurve();

            // Update the timer
            timer += Time.deltaTime;
            if (timer >= loseTimeLimit)
            {
                CheckLoseCondition();
            }
        }

        // Key input controls for testing
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartMoving();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StopMovement();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceAlongCurve();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SetEngineOnFire(true);
        }
    }

    private void MoveAlongCurve()
    {
        Vector3 positionOnCurve = bezierCurve.GetPoint(t);
        Vector3 direction = (positionOnCurve - transform.position).normalized;

        // Move towards the position on the curve
        transform.position = Vector3.MoveTowards(transform.position, positionOnCurve, speed * Time.deltaTime);

        // Rotate towards the direction
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        // Check if reached the position on the curve
        if (Vector3.Distance(transform.position, positionOnCurve) < 0.1f)
        {
            AdvanceAlongCurve();
        }
    }

    public void AdvanceAlongCurve()
    {
        t += 0.1f; // Advance along the curve
        if (t >= 1f)
        {
            t = 1f; // Clamp to the end of the curve
            shouldMove = false; // Stop moving when the end is reached
            CheckWinCondition();
        }
    }

    public void StartMoving()
    {
        shouldMove = true;
        Debug.Log("Started Moving");
    }

    public void StopMovement()
    {
        shouldMove = false;
        Debug.Log("Movement stopped.");
    }

    public void SetEngineOnFire(bool onFire)
    {
        engineOnFire = onFire;
        if (onFire)
        {
            StopMovement(); // Stop the airplane when engine is on fire
            ShowWinUI(); // Show the win panel
            Debug.Log("Engine is on fire!");
        }
    }

    private void CheckWinCondition()
    {
        if (engineOnFire)
        {
            ShowWinUI();
        }
        else if (t >= 1f)
        {
            ShowLoseUI();
        }
    }

    private void CheckLoseCondition()
    {
        if (!engineOnFire && t >= 1f)
        {
            ShowLoseUI();
            shouldMove = false;
        }
    }

    private void ShowWinUI()
    {
        if (winPanel != null) winPanel.SetActive(true);
        Debug.Log("You Win!");
    }

    private void ShowLoseUI()
    {
        if (losePanel != null) losePanel.SetActive(true);
        Debug.Log("You Lose!");
    }

    // Methods for buttons
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("0"); // Replace with the actual scene name of your main menu
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    // Method to start the level
    public void StartLevel()
    {
        shouldMove = true;
        timer = 0f; // Reset the timer
        engineOnFire = false; // Ensure engine fire state is reset

        // Play the "Runway" sound when the level starts
        if (audioManager != null)
        {
            audioManager.Play("Runway");
        }
        else
        {
            Debug.LogError("AudioManager is not assigned!");
        }

        Debug.Log("Level Started");
    }

    // Method to move to the last point
    public void MoveToLastPoint()
    {
        t = 1f;
        shouldMove = false; // Stop moving when the end is reached
        CheckWinCondition();
        Debug.Log("Moved to the last point");
    }
}
