using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private Boss _boss;
    [SerializeField] private Transform _bossSpawn;

    private Player _player;
    
    public void SetTarget(Player player)
    {
       _player = player; 
    }
    
    public void CreateBoss()
    {
        Boss boss = Instantiate(_boss, _bossSpawn.position, Quaternion.identity);
        boss.SetPlayerTarget(_player);
    }
}