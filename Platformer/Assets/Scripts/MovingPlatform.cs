using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 finishpos = Vector3.zero;
    [SerializeField] private float speed = 100f;
    [SerializeField] private SettingsImage img; // menu
    [SerializeField] private QuestImage img2; // question
    [SerializeField] private PlatformerPlayer pl;

    private Vector3 startpos;
    private float trackPercent = 0;
    private int direction = -1;

    private void Start()
    {
        startpos = transform.position;
    }

    private void Update()
    {
        
        if (!img.Status() && !img2.Status() && pl.game)
        {
            trackPercent += direction * speed * Time.deltaTime;
            float x = (finishpos.x - startpos.x) * trackPercent + startpos.x;
            float y = (finishpos.y - startpos.y) * trackPercent + startpos.y;
            transform.position = new Vector3(x, y, startpos.z);

            if ((direction == 1 && trackPercent > 0.9f) || (direction == -1 && trackPercent < 0.1f))
                direction *= -1;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, finishpos);
    }

}
