using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _fallSpeed = 5f;
    PlayerMovement player;
    [SerializeField] private AudioClip _playerDamageClip;
    [SerializeField] private AudioClip _enemyDestroyClip;
    void Start()
    {
        transform.position = new Vector3(0,Random.Range(-10f,10f),0);
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        if(player == null)
        {
            Debug.Log("Player was null");
        }
    }
    void Update()
    {
        FallDown();
    }
    void FallDown()
    {
        transform.Translate(new Vector3(0,-1.0f,0) * _fallSpeed * Time.deltaTime);

        if(transform.position.y < -3.5f)
        {
            float randomX = Random.Range(-10.0f, 11.0f);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            player.IncrementBadFruitCount();
            Sounds_SFX.Instance.PlayRottenFruitDestroyMusic(_enemyDestroyClip);
            CameraShake.Invoke();
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            if (player != null) player.Damage();
            Sounds_SFX.Instance.PlayCatchSFX(_playerDamageClip);
            CameraShake.Invoke();
            Destroy(this.gameObject);
        }
    }
}
