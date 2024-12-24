using PlayerDefault.Scripts.Player.Additional;
using PlayerDefault.Scripts.Player.Main;
using UnityEngine;

namespace PlayerDefault.Scripts.Installers
{
    public class PlayerInstaller : MonoBehaviour
    {
        [SerializeField] private InputReader _playerInputReader;
        
        public InputReader PlayerInputReader
        {
            get
            {
                if (_playerInputReader == null)
                {
                    throw new MissingComponentException("No InputReader is attached to PlayerInstaller.");
                }
                return _playerInputReader;
            } 
        }
        
        [SerializeField] private PlayerMovement _playerMovement;

        public PlayerMovement PlayerMovement
        {
            get
            {
                if (_playerMovement == null)
                {
                    throw new MissingComponentException("No PlayerSystem is attached to PlayerInstaller.");
                }
                return _playerMovement;
            }
        }
        
        [SerializeField] private SprintSystem _sprintSystem;

        public SprintSystem SprintSystem
        {
            get
            {
                if (_sprintSystem == null)
                {
                    throw new MissingComponentException("No SprintSystem is attached to PlayerInstaller.");
                }
                return _sprintSystem;
            }
        }
        
        public static PlayerInstaller Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                throw new UnityException("Only one instance of PlayerInstaller can be active at a time.");
            }
        }
    }
}