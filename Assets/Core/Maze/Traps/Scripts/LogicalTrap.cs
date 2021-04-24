using System.Collections.Generic;
using Core.Maze.Traps.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Maze.Traps
{
    [CreateAssetMenu(fileName = "Data", menuName = "Trap", order = 1)]
    public class LogicalTrap : ScriptableObject
    {
        
        public float damageAmount;
        public TrapTypes[] TypesArray;
        public TrapTypes.Traps TrapType;

    }
}
