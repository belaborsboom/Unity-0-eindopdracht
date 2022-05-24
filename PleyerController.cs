using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PleyerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public float jumpForce;
    public static Vector3 startpos = new Vector3(0,0.5f,0);
    public float playerposy;
    public Vector3 checkpos;

    private Rigidbody rb;
    public int count;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
      	rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
        startpos = transform.position;
    }


    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count:" + count.ToString();
        if(count >= 9)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
        if(other.gameObject.CompareTag("Checkpoint"))
        {
            checkpos = GameObject.Find(other.gameObject.name).transform.position;
            startpos = checkpos;
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void Update()
    {
        playerposy = GameObject.Find("Player").transform.position.y;
        if(playerposy <= -3)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = startpos;
        }
    }
}