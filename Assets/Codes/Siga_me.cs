using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siga_me : MonoBehaviour
{
    public GameObject seguindo;
    public float scale;
    public bool pode;

    private void Start()
    {
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = seguindo.transform.position;
        if (Mathf.Abs(transform.rotation.z) > 0.6f)
        {
            transform.localScale = new Vector3(scale, -scale, scale);
        }
        else
        {
            transform.localScale = Vector2.one * scale;
        }

        if (pode)
        {
            pode = false;
        }
    }
}
