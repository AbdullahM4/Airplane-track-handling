using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AirplaneController : MonoBehaviour
{
    public BezierCurve bezierCurve; // Reference to the BezierCurve script
    public float speed = 5f; // Movement speed
    public float turnSpeed = 2f; // Rotation speed
    private float t = 0f; // Parameter for the Bezier curve

    private bool shouldMove = false;

    // UI Panels
    public GameObject winPanel;
    public GameObject losePanel;
    public Button readyButton; // Reference to the Ready button

    // Timer for losing condition
    private float timer = 0f;
    private float loseTimeLimit = 60f; // 1 minute

    // Target bounds for winning
    private Vector3 pointA = new Vector3(-26.412113f, 0.7695477f, 70.223015f);
    private Vector3 pointB = new Vector3(-2.739038f, 0.5871181f, 67.520294f);

    // Reference to the AudioManager
    private AudioManager audioManager;
    private const string soundName = "Runway"; // Name of the sound to play

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

        // Get the AudioManager instance
        audioManager = AudioManager.instance;
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
            StopSound(); // Stop the sound when reaching the end
        }
    }

    public void StartMoving()
    {
        shouldMove = true;
        PlaySound();
        Debug.Log("Started Moving");
    }

    public void StopMovement()
    {
        shouldMove = false;
        StopSound();
        Debug.Log("Movement stopped.");
    }

    private void CheckWinCondition()
    {
        if (IsWithinBounds(transform.position, pointA, pointB))
        {
            ShowWinUI();
        }
        else
        {
            ShowLoseUI();
        }
    }

    private void CheckLoseCondition()
    {
        if (!IsWithinBounds(transform.position, pointA, pointB))
        {
            ShowLoseUI();
            shouldMove = false;
            StopSound(); // Ensure sound is stopped on losing
        }
    }

    private bool IsWithinBounds(Vector3 position, Vector3 pointA, Vector3 pointB)
    {
        float minX = Mathf.Min(pointA.x, pointB.x);
        float maxX = Mathf.Max(pointA.x, pointB.x);
        float minY = Mathf.Min(pointA.y, pointB.y);
        float maxY = Mathf.Max(pointA.y, pointB.y);
        float minZ = Mathf.Min(pointA.z, pointB.z);
        float maxZ = Mathf.Max(pointA.z, pointB.z);

        return position.x >= minX && position.x <= maxX &&
               position.y >= minY && position.y <= maxY &&
               position.z >= minZ && position.z <= maxZ;
    }

    private void PlaySound()
    {
        if (audioManager != null)
        {
            audioManager.Play(soundName);
        }
    }

    private void StopSound()
    {
        if (audioManager != null)
        {
            audioManager.Stop(soundName);
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
        PlaySound(); // Ensure sound starts when the level starts
        Debug.Log("Level Started");
    }
}
