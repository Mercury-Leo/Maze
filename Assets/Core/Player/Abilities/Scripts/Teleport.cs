using System;
using System.Linq;
using Core.Player.Abilities.Scripts.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Player.Abilities.Scripts
{
    public class Teleport : IPlayerAbility
    {
        private readonly Transform _teleportObject;
        private readonly GameObject[] _teleportLocations;

        private GameObject _currentLocation;
        private GameObject _previousLocation;

        /// <summary>
        /// Gets a teleport object and a teleport locations and can teleport the object randomly to any of the locations.
        /// </summary>
        /// <param name="teleportObject"></param>
        /// <param name="teleportLocations"></param>
        public Teleport(Transform teleportObject, GameObject[] teleportLocations)
        {
            _teleportObject = teleportObject;
            _teleportLocations = teleportLocations;
            _previousLocation = _teleportLocations[0];
        }

        public void TriggerAbility()
        {
            TeleportHandler();
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
            if (_teleportLocations.Length <= 1)
                throw new Exception("Teleport locations length is under 1, cannot teleport player.");
            
            var teleportLocations = _teleportLocations.Where(t => t != _previousLocation).ToList();
            _currentLocation = teleportLocations[Random.Range(0, teleportLocations.Count)];

            TeleportToLocation(_currentLocation.transform);
            _previousLocation = _teleportLocations.FirstOrDefault(t => t == _currentLocation);

            StateController.PlayerState = StateController.PlayerStates.Idle;
        }
    }
}
