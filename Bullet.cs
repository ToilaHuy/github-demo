using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using EZCameraShake;

public class Bullet : NetworkBehaviour
{
    public Transform FirePoint { get; set; }

    public GameObject m_explosionPrefab;

    Controller m_c;


    [SerializeField] float m_speed = 30f;  //set toc do bay dan

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(FirePoint.forward * m_speed * 2f);  //de set toc do thuc te
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ServerCallback]
    private void OnCollisionEnter(Collision collision)
    {
        GameObject exp = Instantiate(m_explosionPrefab, this.transform.position, this.transform.rotation); //khi cham vao tuong thi clone ra hieu ung no tung

        Destroy(exp, 10);

        Destroy(gameObject);

        NetworkServer.Destroy(gameObject);

        CameraShaker.Instance.ShakeOnce(3f, 2f, 0.5f, 0.35f);
    }
}
