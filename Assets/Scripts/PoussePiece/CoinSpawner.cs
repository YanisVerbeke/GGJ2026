using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    private GameObject _currentCoin;
    private float _spawnCooldown = 0.5f;

    private void Start()
    {
        _currentCoin = Instantiate(_coinPrefab, new Vector3(Mathf.Clamp((Camera.main.ScreenToViewportPoint(Input.mousePosition).x - 0.5f) * 30f, -5f, 5f), 17f, 6.85f), Quaternion.LookRotation(Vector3.down));
        ActiveCoin(_currentCoin, false);
    }

    private void Update()
    {
        //Debug.Log((Camera.main.ScreenToViewportPoint(Input.mousePosition).x - 0.5f) * 12f);

        _currentCoin.transform.position = new Vector3(Mathf.Clamp((Camera.main.ScreenToViewportPoint(Input.mousePosition).x - 0.5f) * 30f, -5f, 5f), 17f, 6.85f);

        if (Input.GetMouseButtonDown(0) && _spawnCooldown <= 0f)
        {
            //SfxManager.Instance.PlayCoin();
            ActiveCoin(_currentCoin, true);
            _currentCoin = Instantiate(_coinPrefab, new Vector3(Mathf.Clamp((Camera.main.ScreenToViewportPoint(Input.mousePosition).x - 0.5f) * 30f, -5f, 5f), 17f, 6.85f), Quaternion.LookRotation(Vector3.down));
            ActiveCoin(_currentCoin, false);
            _spawnCooldown = 0.3f;
        }

        if (_spawnCooldown > 0f)
        {
            _spawnCooldown -= Time.deltaTime;
        }
    }

    private void ActiveCoin(GameObject coin, bool active)
    {
        _currentCoin.GetComponent<Rigidbody>().useGravity = active;
        _currentCoin.GetComponent<Collider>().isTrigger = !active;
    }

}
