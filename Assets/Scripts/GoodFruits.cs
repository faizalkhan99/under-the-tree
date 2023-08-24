using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodFruits : MonoBehaviour
{
    
    [SerializeField] private float _fallSpeed;
    [SerializeField] AudioClip _goodFruitCatchclip;

    void Update()
    {
        GoodFruitsFallDown();
    }


    void GoodFruitsFallDown()
    {
        Vector3 fallDir = new Vector3(0, -1, 0);
        transform.Translate(fallDir * _fallSpeed * Time.deltaTime);
        if (transform.position.y < -4f)
        {
            DestroyObject();
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Sounds_SFX.Instance.PlayCatchSFX(_goodFruitCatchclip);
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            if(player != null)
            {
                player.IncrementGoodFruitCount();
            }
            DestroyObject();
        }
    }

    void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
