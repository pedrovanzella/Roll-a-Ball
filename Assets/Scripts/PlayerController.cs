using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    public float speed;
    public Text speedText;
    private int score;
    public Text scoreText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateSpeedText();
        score = 0;
        UpdateScoreText();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        UpdateSpeedText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Death Zone")) {
            Application.LoadLevel("GameOver");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Cow")) {
            UpdateScore();
            UpdateScoreText();
            collision.collider.gameObject.GetComponent<ParticleSystem>().Play();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Cow")) {
            collision.collider.gameObject.GetComponent<ParticleSystem>().Stop();
        }
    }

    void UpdateSpeedText() 
    {
        float mag = 0.0f;
        if (rb.velocity.magnitude > 0.1f) {
            mag = rb.velocity.magnitude;
        }
        speedText.text = "Speed: " + mag.ToString();
    }

    void UpdateScore()
    {
        score += (int)(rb.velocity.magnitude * 10);
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
