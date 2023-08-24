using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour 

{
    [SerializeField] private float _speed;
    [SerializeField] private int _powerupID;
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(0, -1.0f, 0);
        transform.Translate(dir * _speed * Time.deltaTime);

        if(transform.position.y < -4.5f)
        {
            DestroyPowerup();
        }

    }

    [SerializeField] private AudioClip _powerupSFX;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //play sfx
            Sounds_SFX.Instance.PlayCatchSFX(_powerupSFX);
            //activate powerup by taking reference of that bool variable from PlayerMovement script.
            PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
            if(player != null)
            {
                switch (_powerupID)
                {
                    case 0: player.EnableSheildPowerUp(); break;
                    case 1: player.EnableSpeedPowerUp(); break;
                    case 2: player.EnableTrippleShotPowerUp(); break;
                }

            }
            DestroyPowerup();
        }
    }


    void DestroyPowerup()
    {
        Destroy(this.gameObject);
    }
}
