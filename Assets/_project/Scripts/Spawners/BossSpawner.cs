using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private Boss _boss;
    [SerializeField] private Transform _bossSpawn;
    
    public void CreateBoss()
    {
        Boss boss = Instantiate(_boss, _bossSpawn.position, Quaternion.identity);
    }
}