using System;
using Core.Player.Abilities.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Player.Abilities.Scripts
{
    public class DrawGraffiti : IPlayerAbility
    {
        private LayerMask mazeLayer;
        private Transform _targetTransform;
        private GameObject _graffiti;
        private GraffitiEvent _graffitiEvent = new GraffitiEvent();
        public DrawGraffiti(Transform targetTransform, GameObject graffiti, UnityAction<Vector3, Quaternion> func)
        {
            mazeLayer = LayerMask.GetMask("Maze");
            _targetTransform = targetTransform;
            _graffiti = graffiti;
            _graffitiEvent.AddListener(func);
        }
        
        public void TriggerAbility()
        {
            WallTracing();
        }

        private void WallTracing()
        {
            var hit = new RaycastHit();
            var directionRay = _targetTransform.rotation * Vector3.forward;
            var ray = new Ray(_targetTransform.position, directionRay);
            
            if(Physics.Raycast(ray, out hit, 30, mazeLayer))
            {
                var hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                var hitPoint = hit.point + hit.normal * 0.1f;
                hitPoint.y += 1f;
                _graffitiEvent?.Invoke(hitPoint, hitRotation);
                //audioSource.PlayOneShot(graffitiSound);
            }
        }
        
        
    }
}
