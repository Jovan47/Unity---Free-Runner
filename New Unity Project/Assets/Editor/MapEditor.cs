using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]

public class MapEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		MapGenerator map = target as MapGenerator;
		map.GenerateMap();

		GUILayout.BeginHorizontal();

		if(GUILayout.Button("Generate Color"))
		{
			Debug.Log("BOJI");
			
			foreach (GameObject obj in Selection.gameObjects)
			{

				Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {    
					renderer.material.color = Color.red;
                }
            }
		}
		GUILayout.EndHorizontal();
	}

}