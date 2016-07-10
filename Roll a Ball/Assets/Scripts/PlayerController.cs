using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text winText;
    public AudioClip swallowSound;
    public AudioClip winSound;
    public Camera theCamera;

    private AudioSource source;
    private Rigidbody rb;
    private int count;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        UpdateCount();
        countText.text = "Count: " + count.ToString();
        winText.text = string.Empty;
    }

    private void UpdateCount()
    {
        count += 1;
        if (count > 12) // If game over
        {
            winText.text = "You Win!";
            MakeSound(winSound);
            var cameraAudio = theCamera.GetComponent<AudioSource>();
            if (cameraAudio != null)
            {
                cameraAudio.Stop();
            }
            return;
        }
        countText.text = "Count: " + count.ToString();
    }
    
    // just before performing Physics
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            UpdateCount();
            MakeSound(swallowSound);
        }
    }

    private void MakeSound(AudioClip sound)
    {
        source.PlayOneShot(sound, 1f);
    }

}
