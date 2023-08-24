using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject _trippleShot;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _shieldVisualizer;
    [SerializeField] public int _lives = 3;
    [SerializeField] public int _badFruitsCount;
    [SerializeField] public int _goodFruitsCount;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private bool _isShieldActive = false;
    [SerializeField] private float _powerupTime;
    [SerializeField] private bool _isTrippleShotActive = false;
    private float _canFire = -1f;
    SpawnManager _spawnManager;
    UI_Mangarer ui_mangarer;
    [SerializeField] private AudioClip _shootSFX;

    void Start()
    {
        ui_mangarer = GameObject.Find("Canvas").GetComponent<UI_Mangarer>();
        _shieldVisualizer.SetActive(false);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if( _spawnManager == null ) // NULL check
        {
            Debug.Log("Spawn Manager was NULL");
        }
    }
    void Update()
    {
        CalculateMovement();

#if (UNITY_ANDROID || UNITY_IOS)
        if ( CrossPlatformInputManager.GetButtonDown("Fire") && Time.time > _canFire)
        {
            FireBullet();
        }
#else
        if (Input.GetKeyUp(KeyCode.Space) && Time.time > _canFire)
        {
            FireBullet();
        }
#endif
    }
    void CalculateMovement()
    {
        float _tempHor;
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        if (horizontal > 0) _tempHor = 1;
        else if(horizontal < 0) _tempHor = -1;
        else _tempHor = 0;
        Vector3 direction = new Vector3(_tempHor, 0, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        Vector3 currentPos = transform.position;
        if (currentPos.x >= 11.1)
        {
            transform.position = new Vector3(-11.1f, transform.position.y, 0);
            Sounds_SFX.Instance.PlayTeleportFromSideSFX();
        }
        if (currentPos.x <= -11.1)
        {
            transform.position = new Vector3(11.1f, transform.position.y, 0);
            Sounds_SFX.Instance.PlayTeleportFromSideSFX();
        }
    }
    public void FireBullet()
    {
        _canFire = Time.time + _fireRate;
        
        if(_isTrippleShotActive == true) Instantiate(_trippleShot, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
        else Instantiate(_bullet, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
        Sounds_SFX.Instance.PlayShootSFX(_shootSFX);
    }
    public void Damage()
    {
        if (_isShieldActive) return;
        else
        {
            _lives--;
            ui_mangarer.UpdateLives(_lives);
        }
        if( _lives <= 0 )
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void EnableTrippleShotPowerUp()
    {
        _isTrippleShotActive = true;
        StartCoroutine(TrippleShotPowerupHoldTime(_powerupTime));
    }
    public void EnableSpeedPowerUp()
    {
        _speed = 8.5f;
        StartCoroutine(SpeedPowerupHoldTime(_powerupTime));
    }
    public void EnableSheildPowerUp()
    {
        StartCoroutine(ShieldPowerupHoldTime(_powerupTime));
        _isShieldActive = true;
    }
    IEnumerator TrippleShotPowerupHoldTime(float _powerupTime)
    {
        yield return new WaitForSeconds(_powerupTime);
        _isTrippleShotActive = false;
    }
    IEnumerator SpeedPowerupHoldTime(float _powerupTime)
    {
        yield return new WaitForSeconds(_powerupTime);
        _speed = 5f;
    }
    IEnumerator ShieldPowerupHoldTime(float _powerupTime)
    {
        _shieldVisualizer.SetActive(true);
        _shieldVisualizer.transform.parent = transform;
        yield return new WaitForSeconds(_powerupTime);
        _isShieldActive = false;
        _shieldVisualizer.SetActive(false);
    }
    public void IncrementBadFruitCount()
    {
        _badFruitsCount++;
    }
    public void IncrementGoodFruitCount()
    {
        _goodFruitsCount++;
    }
}