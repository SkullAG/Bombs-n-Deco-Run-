using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
	public Transform objective;

	public Transform[] panels;

	public float distanceBetweenPanels;

	public Directions dir;

	public enum Directions
	{
		X, Y, Z
	}

    private void Update()
    {

        if (dir == Directions.X)
        {
			for(int i = 0; i < panels.Length; i++)
            {
				if(objective.position.x >= panels[i].position.x + distanceBetweenPanels * panels.Length / 2)
                {
                    panels[i].position += new Vector3(distanceBetweenPanels * panels.Length, 0, 0);
                    RepositionedEvent re = panels[i].GetComponent<RepositionedEvent>();
                    if (re)
                    {
                        re.SendRepositioningMessage();
                    }
                }
                else if (objective.position.x <= panels[i].position.x - distanceBetweenPanels * panels.Length / 2)
                {
                    panels[i].position -= new Vector3(distanceBetweenPanels * panels.Length, 0, 0);
                    RepositionedEvent re = panels[i].GetComponent<RepositionedEvent>();
                    if (re)
                    {
                        re.SendRepositioningMessage();
                    }
                }
            }
        }
        if (dir == Directions.Y)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                if (objective.position.y >= panels[i].position.y + distanceBetweenPanels * panels.Length / 2)
                {
                    panels[i].position += new Vector3(0, distanceBetweenPanels * panels.Length, 0);
                    RepositionedEvent re = panels[i].GetComponent<RepositionedEvent>();
                    if (re)
                    {
                        re.SendRepositioningMessage();
                    }
                }
                else if (objective.position.y <= panels[i].position.y - distanceBetweenPanels * panels.Length / 2)
                {
                    panels[i].position -= new Vector3(0, distanceBetweenPanels * panels.Length, 0);
                    RepositionedEvent re = panels[i].GetComponent<RepositionedEvent>();
                    if (re)
                    {
                        re.SendRepositioningMessage();
                    }
                }
            }
        }
        if (dir == Directions.Z)
        {
			for(int i = 0; i < panels.Length; i++)
            {
				if(objective.position.z >= panels[i].position.z + distanceBetweenPanels * panels.Length / 2)
                {
                    panels[i].position += new Vector3(0, 0, distanceBetweenPanels * panels.Length);
                    RepositionedEvent re = panels[i].GetComponent<RepositionedEvent>();
                    if(re)
                    {
                        re.SendRepositioningMessage();
                    }
                }
                else if (objective.position.z <= panels[i].position.z - distanceBetweenPanels * panels.Length / 2)
                {
                    panels[i].position -= new Vector3(0, 0, distanceBetweenPanels * panels.Length);
                    RepositionedEvent re = panels[i].GetComponent<RepositionedEvent>();
                    if (re)
                    {
                        re.SendRepositioningMessage();
                    }
                }
            }
        }
    }
}
