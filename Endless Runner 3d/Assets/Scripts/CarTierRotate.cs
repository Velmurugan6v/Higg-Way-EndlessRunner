using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTierRotate : MonoBehaviour
{
    [Range(0,10000)]
    [SerializeField] int carTierRotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CarTierRotation()
    {
        transform.Rotate(Vector3.right * -carTierRotationSpeed * Time.deltaTime);
    }
}
