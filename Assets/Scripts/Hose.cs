using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class HoseSection
{
    public Vector2 start;
    public Vector2 end;

    public HoseSection(Vector2 start, Vector2 end)
    {
        this.start = start;
        this.end = end;
    }
}

[RequireComponent(typeof(LineRenderer))]
public class Hose : MonoBehaviour
{

    List<HoseSection> hoseSections;
    public bool hasHose = false;
    LineRenderer lineRenderer;

    public bool retracting = false;
    public float retractSpeed = 5f;

    private void Start()
    {
        hoseSections = new List<HoseSection>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = .1f;
    }

    void startHose(Vector2 pos)
    {
        hoseSections = new List<HoseSection>();
        hoseSections.Add(new HoseSection(pos, pos));
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, pos);
        lineRenderer.SetPosition(1, pos);
        hasHose = true;
    }

    void startRetracting()
    {
        retracting = true;
    }

    void resetLineRenderer()
    {
        if (hoseSections.Count > 0)
        {
            Vector3[] positions = new Vector3[hoseSections.Count + 1];
            positions[0] = hoseSections[0].start;
            for (int i = 0; i < hoseSections.Count; i++)
            {
                positions[i + 1] = hoseSections[i].end;
            }
            lineRenderer.SetPositions(positions);
            lineRenderer.positionCount = positions.Length;
        }
    }

    void retractHose()
    {
        float retractDist = retractSpeed * Time.deltaTime;
        while (retractDist > 0 && hoseSections.Count > 0)
        {
            HoseSection firstSection = hoseSections[0];
            float firstSectionLength = Vector2.Distance(firstSection.start, firstSection.end);

            if (retractDist > firstSectionLength)
            {
                print("REMOVE");
                retractDist -= firstSectionLength;
                hoseSections.RemoveAt(0);
                resetLineRenderer();
            }
            else
            {
                firstSection.start += (firstSection.end - firstSection.start).normalized * retractDist;
                lineRenderer.SetPosition(0, firstSection.start);
                retractDist = 0;
            }
        }
        if (hoseSections.Count == 0)
        {
            retracting = false;
            hasHose = false;
        }
        resetLineRenderer();


    }

    public void moveHoseEnd(Vector2 newPos)
    {
        HoseSection lastSection = hoseSections[hoseSections.Count - 1];
        Vector2 lastDir = lastSection.end - lastSection.start;

        if (hoseSections.Count > 1)
        {
            HoseSection prevSection = hoseSections[hoseSections.Count - 2];

            // check if last two sections have to be merged
            Vector2 newDir = newPos - lastSection.start;
            Vector2 prevDir =  prevSection.end - prevSection.start;
            Vector2 prevNormal = new Vector2(prevDir.y, -prevDir.x);
            if (Mathf.Sign(Vector2.Dot(lastDir, prevNormal)) != Mathf.Sign(Vector2.Dot(newDir, prevNormal))){
                // merge sections if no longer pulling on corner in same direction
                hoseSections.RemoveAt(hoseSections.Count - 1);
                prevSection.end = newPos;

                lineRenderer.positionCount--;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPos);
                Debug.Log("Combined");
                return;
            }

        }

        // check if last section has to be split
        RaycastHit2D hit = Physics2D.Raycast(lastSection.start + lastDir.normalized * .01f, lastDir, Vector2.Distance(lastSection.start, lastSection.end));
        if (hit.collider != null)
        {

            if (hit.collider is PolygonCollider2D)
            {
                PolygonCollider2D collider = hit.collider as PolygonCollider2D;

                Vector2 normal = new Vector2(lastDir.y, -lastDir.x).normalized;
                if (Vector2.Dot(newPos - lastSection.start, normal) > 0)
                {
                    normal = -normal;
                }

                float maxDist = -1;
                Vector2 targetCorner = new Vector2(0, 0);

                foreach (Vector2 localCorner in collider.GetPath(0))
                {
                    Vector2 corner = hit.collider.transform.TransformPoint(localCorner);
                    float distFromLine = Vector2.Dot(corner - lastSection.start, normal);
                    if (distFromLine > maxDist)
                    {
                        maxDist = distFromLine;
                        targetCorner = corner;
                    }
                }
                lastSection.end = targetCorner;
                hoseSections.Add(new HoseSection(targetCorner, newPos));
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 2, targetCorner);
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPos);

                Debug.Log("Split");
            }
        }
        else
        {
            lastSection.end = newPos;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPos);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) {
            startHose(new Vector2(transform.position.x, transform.position.y));
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            startRetracting();
        }

        if (hasHose) {
            moveHoseEnd(new Vector2(transform.position.x, transform.position.y));

            foreach (HoseSection section in hoseSections)
            {
                Debug.DrawLine(new Vector3(section.start.x, section.start.y,0),
                                new Vector3(section.end.x, section.end.y,0));
            }
        }

        if (retracting)
        {
            retractHose();
        }
    }
}
