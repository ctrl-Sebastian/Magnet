using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPoint : MonoBehaviour
{
    public KeyCode positiveKey;
    public KeyCode negativeKey;
    public enum MagnetType
    {
        positive,
        negative
    }

    public MagnetType magnetType;
    public float forceFactor = 500f;
    public float impulseFactor = 50f;
    public Renderer mr;
    public Material positiveMaterial;
    public Material negativeMaterial;

    public Transform grabPoint;
    public Transform orientation;

    private AudioSource audioSource;
    public AudioClip changePolaritySfx, grabSfx;

    private void MagnetTypeHandler()
    {
        if(Input.GetKey(positiveKey))
        {
            audioSource.clip = changePolaritySfx;
            magnetType = MagnetType.positive;
            audioSource.Play();
            if(grabPoint.childCount > 0){
                grabPoint.DetachChildren();
            }

        }else if(Input.GetKey(negativeKey)){
            audioSource.clip = changePolaritySfx;
            magnetType = MagnetType.negative;
            audioSource.Play();
            if(grabPoint.childCount > 0){
                grabPoint.DetachChildren();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        magnetType = MagnetType.positive;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MagnetTypeHandler();
        if(magnetType == MagnetType.positive)
            mr.material = positiveMaterial;
        else if(magnetType == MagnetType.negative)
            mr.material = negativeMaterial;

        if(grabPoint.childCount > 0){
            audioSource.clip = grabSfx;
            audioSource.Play();
        }
    }

    void OnTriggerStay(Collider other)
    {
        Rigidbody otherRb = other.GetComponent<Rigidbody>();
        Transform otherTr = other.GetComponent<Transform>();
        if((other.CompareTag("positive") && magnetType == MagnetType.negative) || (other.CompareTag("negative") && magnetType == MagnetType.positive ))
        {
            Debug.Log("deben unirse");
            if(Vector3.Distance(otherTr.position, grabPoint.position) >= 0.5f){
                otherRb.AddForce((grabPoint.position - otherTr.position) * forceFactor * Time.fixedDeltaTime, ForceMode.Force);
            } else if(Vector3.Distance(otherRb.position, grabPoint.position) < 0.5f){
                otherTr.position = grabPoint.position;
                otherTr.parent = grabPoint;
                otherTr.localRotation = Quaternion.identity;
                otherRb.constraints = RigidbodyConstraints.FreezePosition;
            }
        } 
        else if((other.CompareTag("positive") && magnetType == MagnetType.positive) || (other.CompareTag("negative") && magnetType == MagnetType.negative))
        {
            Debug.Log("deben repelerse");
            otherRb.constraints = RigidbodyConstraints.None;
            otherRb.AddForce((grabPoint.position - orientation.position) * impulseFactor * Time.fixedDeltaTime, ForceMode.Impulse);
        } 
    }
}
