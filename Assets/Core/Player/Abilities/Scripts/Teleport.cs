using Core.Player.Abilities.Scripts.Interfaces;
using UnityEngine;

namespace Core.Player.Abilities.Scripts
{
    public class Teleport : IPlayerAbility
    {
        private readonly Transform _teleportObject;
        private readonly GameObject[] _teleportLocations;

        private int _currentLocation;
        private int _previousLocation = 0;

        public Teleport(Transform teleportObject, GameObject[] teleportLocations)
        {
            _teleportObject = teleportObject;
            _teleportLocations = teleportLocations;
        }

        public void TriggerAbility()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Teleports the connected <see cref="_teleportObject"/> to a location.
        /// </summary>
        /// <param name="location"></param>
        private void TeleportToLocation(Transform location)
        {
            var tempPos = location.position;
            _teleportObject.position = new Vector3(tempPos.x, tempPos.y, tempPos.z);
        }

        private void TeleportHandler()
        {
            //For now basic if random picked same spot re roll
            //TODO: Find a better method to prevent random being chosen.
            _currentLocation = Random.Range(0, _teleportLocations.Length);
            if(_currentLocation.Equals(_previousLocation))
                _currentLocation = Random.Range(0, _teleportLocations.Length);
            
            TeleportToLocation(_teleportLocations[_currentLocation].transform);
            _previousLocation = _currentLocation;

        }
    }
}
