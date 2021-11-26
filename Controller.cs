using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Controller : NetworkBehaviour

{


    const string HORIZONTAL = "Horizontal";
    const string VERTICAL = "Vertical";

    [SerializeField]
    float m_speed = 5f;

    Rigidbody m_rb;

    Weapon m_wp;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        gameObject.tag = "PlayerA";
        m_wp = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isLocalPlayer)
        ApplyInput();
    }

    private void ApplyInput()
    {
        
        float h = Input.GetAxis(HORIZONTAL);
        float v = Input.GetAxis(VERTICAL);
        


        var dir = new Vector3(h , .0f, v);
        //dieu khien player di chuyen
        m_rb.MovePosition(transform.position + m_speed * Time.fixedDeltaTime * dir);
        if(h != 0 || v != 0)
        m_rb.rotation = Quaternion.LookRotation(dir);

        if (Input.GetKeyUp("space"))
        {
            m_wp.Shoot();
        }
        if (Input.GetKeyUp("n"))
        {
            killedPlayer();
        }
    }

    public void killedPlayer()
    {
        GameManager.Instance.IsEndGame = true;
    }
}
