using UnityEngine;

namespace HieuDev
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] GameData _gameData;
        [SerializeField] SerializationAndEncryption _serializationAndEncryption;

        public GameData GameData { get => _gameData; private set => _gameData = value; }

        private void Start()
        {
            LoadGameData();
        }

        public void LoadGameData()
        {
            _serializationAndEncryption.LoadGameData();
            GameData = _serializationAndEncryption._gameData;
        }

        public void SaveGameData()
        {
            _serializationAndEncryption.SaveGameData(GameData);
        }
    }

}