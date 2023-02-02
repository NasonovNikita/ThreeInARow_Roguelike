using UnityEngine;

public class LineVertex : MonoBehaviour
{
    private LineRenderer _line;

    public void Awake()
    {
        _line = GetComponent<LineRenderer>();
    }
    
    public void Draw(Vector3 pos1, Vector3 pos2)
    {
        _line.SetPosition(0, pos1);
        _line.SetPosition(1, pos2);
    }
}
