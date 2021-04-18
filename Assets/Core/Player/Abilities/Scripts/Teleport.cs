using Core.Player.Abilities.Scripts.Interfaces;
using UnityEngine;

namespace Core.Player.Abilities.Scripts
{
    public class Teleport : IPlayerAbility
    {
        private readonly Transform _teleportObject;
        private GameObject[] _teleportLocations;

        private int _currentLocation;
        private int _previousLocation;

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
            _previousLocation = Random.Range(0, _teleportLocations.Length);
            
        }
    }
}
