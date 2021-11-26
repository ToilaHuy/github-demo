using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Weapon : NetworkBehaviour
{
    
    [SerializeField] GameObject m_bulletPrefab; //vien dan
    [SerializeField] Transform m_firePoint; //nong sung

    //(3 mui ten)
    
    [SerializeField] GameObject m_arrow1;
    [SerializeField] GameObject m_arrow2;
    [SerializeField] GameObject m_arrow3;
    
    [SerializeField] float m_refillRate = 1;//thoi gian nap dan

    int m_bulletReady; //vien dan san sang ban
   float m_nextTimeToRefill;

    // Start is called before the first frame update
    void Start()
    {
        m_arrow1.SetActive(false);
        m_arrow2.SetActive(false);
        m_arrow3.SetActive(false);

        m_bulletReady = 0;

        m_nextTimeToRefill = 1f / m_refillRate + Time.time;

    }

    // Update is called once per frame
    [ClientRpc]
    void Update()
    {
        if(m_bulletReady <3 && Time.time > m_nextTimeToRefill)
        {
            m_bulletReady++;

            if(m_bulletReady == 1)
            {
                m_arrow1.SetActive(true);
            }
            else if (m_bulletReady == 2)
            {
                m_arrow2.SetActive(true);
            }
            else if(m_bulletReady == 3)
            {
                m_arrow3.SetActive(true);
            }
           

            m_nextTimeToRefill = 1f / m_refillRate + Time.time;
        }
        
    }
    [Command]
    public void CmdServerEvolve()
    {
        GameObject bullet = Instantiate(m_bulletPrefab, m_firePoint.position, m_firePoint.rotation);
        NetworkServer.Spawn(bullet);
        Shoot();
    }


    [ClientRpc]
    public void Shoot()
    {
        if (m_bulletReady <= 0)
        {
            return;
        }

        GameObject bullet = Instantiate(m_bulletPrefab, m_firePoint.position, m_firePoint.rotation);
        bullet.GetComponent<Bullet>().FirePoint = m_firePoint;
        
        
        if (m_bulletReady == 1)
        {
            m_arrow1.SetActive(false);
        }
        else if (m_bulletReady == 2)
        {
            m_arrow2.SetActive(false);
        }
        else if (m_bulletReady == 3)
        {
            m_arrow3.SetActive(false);
        }

        m_bulletReady--;
        m_nextTimeToRefill = 1f / m_refillRate + Time.time;
    }
}
