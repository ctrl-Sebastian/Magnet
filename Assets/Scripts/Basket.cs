using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Basket : MonoBehaviour
{
    public TMP_Text pointsText;

    private AudioSource audioSource;
        
    public enum ChargeType
    {
        positive,
        negative
    }
    public ChargeType chargeType;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        float x = transform.position.x;
        float y = Mathf.Sin(Time.time);
        float z = transform.position.z;

        transform.position = new Vector3(x, (-9-y) *2, z);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(chargeType.ToString()))
        {
            GameManager.points += 5;
            audioSource.Play();
            pointsText.text = "Points: " + GameManager.points.ToString();
            CountDownTimer.currentTime += 5f;
            Debug.Log("Points: " + GameManager.points);
        }
        Destroy(other.gameObject);
    }
}
