using UnityEngine;

public class DetectPlayer
{
    private readonly GameObject _detectableObject;
    private readonly GameObject _searchingObject;
    private const float SearchDistance = 50f;

    public DetectPlayer(GameObject detectableObject, GameObject searchingObject)
    {
        _detectableObject = detectableObject;
        _searchingObject = searchingObject;
    }

    public bool DetectedPlayer()
    {
        return Vector3.Distance(_detectableObject.transform.position, _searchingObject.transform.position) > SearchDistance;
    }
}
