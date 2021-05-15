using System;
using Core.Player.Abilities.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Player.Abilities.Scripts
{
    public class DrawGraffiti : IPlayerAbility
    {
        LayerMask mazeLayer = LayerMask.NameToLayer("Maze");
        private Transform _targetTransform;
        private GameObject _graffiti;
        private GraffitiEvent _graffitiEvent = new GraffitiEvent();
        public DrawGraffiti(Transform targetTransform, GameObject graffiti, UnityAction<Vector3, Quaternion> func)
        {
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

            Debug.Log("hit is: " + ray);
            if(Physics.Raycast(ray, out hit, 3, mazeLayer))
            {
                Debug.Log("hgey");
                var hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                var hitPoint = hit.point + hit.normal * 0.1f;
                hitPoint.y += 1f;
                _graffitiEvent?.Invoke(hitPoint, hitRotation);
                //audioSource.PlayOneShot(graffitiSound);
            }
        }
        
        
    }
}
