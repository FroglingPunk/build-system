using UnityEngine;

// Обычно я не использую подобные способы для инициализации сцены/проекта
// Но в данном случае не было времени на DI
public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private PlayerView _playerViewTemplate;
    [SerializeField] private Transform _spawnPoint;
    
    private Player _player;


    private void Start()
    {
        var playerView = Instantiate(_playerViewTemplate, Vector3.zero, Quaternion.identity, _spawnPoint);
        _player = new Player(playerView);
    }
}