using UnityEngine;

public interface IGameFactory : IService
{
    GameObject CreatePlayer(GameObject at);
}