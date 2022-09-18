using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI livesText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject player;
    private Rigidbody rb;
    private int lives;
    private int count;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        lives = 3;
        SetCountText();
        livesText.text = "Lives: " + lives.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count == 12)
        {
            transform.position = new Vector3(0.0f, 35f, 0.0f);
        }
        if (count >= 20)
        {
            Destroy(rb);
            winTextObject.SetActive(true);
        }
    }

    void KillPlayer()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            Destroy(rb);
            player.SetActive(false);
            loseTextObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;

            SetCountText();
        }
        else if (other.gameObject.CompareTag("HurtPlayer"))
        {
            other.gameObject.SetActive(false);
            lives--;

            KillPlayer();
        }
    }
}