using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riser : MonoBehaviour
{
    private float m_randomOffset;
    // Start is called before the first frame update
    void Start()
    {
        m_randomOffset = Random.Range(0f, 5f); //0 -> 5 se lam cho cac o nhun nhay
    }

    // Update is called once per frame
    void Update()
    {
        float perlin = Mathf.PerlinNoise(transform.position.x / m_randomOffset + Time.time, transform.position.z / m_randomOffset + Time.time); //gia tri tang tu 0 -1
        transform.localScale = new Vector3(1f, perlin * 4f, 1f);
    }
}
