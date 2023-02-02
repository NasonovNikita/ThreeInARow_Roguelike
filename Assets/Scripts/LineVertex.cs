using UnityEngine;

public class LineVertex : MonoBehaviour
{
    private LineRenderer _line;

    public void Start()
    {
        _line = GetComponent<LineRenderer>();
    }
    
    public void Draw(Vector3 pos)
    {
        _line.positionCount++;
        _line.SetPosition(0, transform.position);
        _line.SetPosition(1, pos);
    }
}
