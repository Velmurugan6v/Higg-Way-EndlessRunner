using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl2 : MonoBehaviour
{
    public static PlayerControl2 instance;

    [SerializeField] Rigidbody rb;
    [SerializeField] float sideSpeed;
    public int moveSpeed;

    [SerializeField] Transform[] Tiers;
    [SerializeField] int tierRotationSpeed;

    public int CurrentSpeed { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CurrentSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float hori = Input.GetAxisRaw("Horizontal");
        float verti = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(hori * sideSpeed * Time.deltaTime, rb.velocity.y,CurrentSpeed * Time.deltaTime);

        foreach (Transform tierTran in Tiers)
        {
            tierTran.Rotate(Vector3.forward * tierRotationSpeed * Time.deltaTime);
        }
    }
}
