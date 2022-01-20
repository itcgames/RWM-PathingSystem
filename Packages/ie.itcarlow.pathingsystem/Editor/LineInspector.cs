using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Line))]
public class LineInspector : Editor
{
	private Line line;
	private Transform handleTransform;
	private Quaternion handleRotation;

	private void OnSceneGUI()
	{
		// Target is the object drawn when OnSceneGUI is called
		// Cast this to our line variable
		line = target as Line;

		handleTransform = line.transform;
		handleRotation =
			Tools.pivotRotation == PivotRotation.Local ?
			handleTransform.rotation :
			Quaternion.identity;

		// Take the positions of our line points
		Vector3 p0 = handleTransform.TransformPoint(line.p0);
		Vector3 p1 = handleTransform.TransformPoint(line.p1);

		// Draw a white debug line along our line
		Handles.color = Color.white;
		Handles.DrawLine(p0, p1);

		// Poll for changes in the editor
		EditorGUI.BeginChangeCheck();

		// Set handles for both ends of our line
		p0 = Handles.DoPositionHandle(p0, handleRotation);
		p1 = Handles.DoPositionHandle(p1, handleRotation);

		if (EditorGUI.EndChangeCheck())
		{
			// Record the previous state of our point to undo to
			Undo.RecordObject(line, "Move Point");

			// Mark the line as changed (will prompt user to save before quitting)
			EditorUtility.SetDirty(line);

			// Convert the handle position from world-space to local-space and assign back
			line.p0 = handleTransform.InverseTransformPoint(p0);
			line.p1 = handleTransform.InverseTransformPoint(p1);
		}
	}
}