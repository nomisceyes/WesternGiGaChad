using System.Collections;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Boss _boss;
    [SerializeField] private Transform _bossSpawn;
    [SerializeField] private Player _player;

    [SerializeField] private TextMeshProUGUI _victoryText;
    [SerializeField] private TextMeshProUGUI _prepareText;
    [SerializeField] private TextMeshProUGUI _bossText;

    [SerializeField] private int _maxWaveAmount;

    private int _currentWave;
    private bool _prerareToNextWave = false;
    private bool _bossFight = false;

    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        _inputService.Update();

        if (_currentWave == _maxWaveAmount)
        {
            if (_bossFight == false)
            {
                _bossFight = true;
                StartCoroutine(PrepareToBossFight());
            }
        }
        else if (_enemySpawner.CurrentEnemies == _enemySpawner.MaxAmountEnemy && _enemySpawner.Enemies.Count == 0)
        {
            if (_prerareToNextWave == false)
                StartCoroutine(PrepareToNextWaveRoutine());
        }
    }

    private void CreateBoss()
    {
        Boss boss = Instantiate(_boss, _bossSpawn.position, Quaternion.identity);
        boss.SetPlayerTarget(_player);
    }

    private IEnumerator PrepareToBossFight()
    {
        _bossText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        _bossText.gameObject.SetActive(false);

        CreateBoss();
    }

    private IEnumerator PrepareToNextWaveRoutine()
    {
        _victoryText.gameObject.SetActive(true);
        _prerareToNextWave = true;

        yield return new WaitForSeconds(3f);

        _victoryText.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        _prepareText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        _prepareText.gameObject.SetActive(false);

        Debug.Log("3");

        yield return new WaitForSeconds(1f);

        Debug.Log("2");

        yield return new WaitForSeconds(1f);

        Debug.Log("1");

        yield return new WaitForSeconds(0.5f);

        _currentWave++;

        if (_currentWave < _maxWaveAmount)
        {
            _enemySpawner.CurrentEnemies = 0;
            _enemySpawner.MaxAmountEnemy += 0;

            _enemySpawner.Spawn();
        }

        _prerareToNextWave = false;
    }
}