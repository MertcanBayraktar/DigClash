
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class SaveAssetEditor : MonoBehaviour
{
	public KeyCode saveKey = KeyCode.F12;
	public string saveName = "SavedMesh";
	public Transform selectedGameObject;

	void Update()
	{
		if (Input.GetKeyDown(saveKey))
		{
			SaveAsset();
		}
	}

	void SaveAsset()
	{
		var mf = selectedGameObject.GetComponent<MeshFilter>();
		if (mf)
		{
			var savePath = "Assets/" + saveName + ".asset";
			Debug.Log("Saved Mesh to:" + savePath);
			AssetDatabase.CreateAsset(mf.mesh, savePath);
		}
	}
}

#endif
