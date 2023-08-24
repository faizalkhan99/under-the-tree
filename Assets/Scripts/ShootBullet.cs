using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 10f;
    void Start()
    {
        
        bulletDir = new Vector3(0, 1, 0);
        
    }
    Vector3 bulletDir;
    void Update()
    {

        transform.Translate(bulletDir * _bulletSpeed * Time.deltaTime);

        if(transform.position.y > 9)
        {
            Destroy(this.gameObject);
            if(this.transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }
        }

    }
}
