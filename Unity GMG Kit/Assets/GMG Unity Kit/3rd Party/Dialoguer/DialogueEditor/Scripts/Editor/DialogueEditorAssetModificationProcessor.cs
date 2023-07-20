using UnityEngine;
using UnityEditor;
using System.Collections;

using DialoguerEditor;

public class DialogueEditorAssetModificationProcessor : UnityEditor.AssetModificationProcessor {
	public static string[] OnWillSaveAssets(string[] paths){
		//DialogueEditorWindow window = (DialogueEditorWindow)EditorWindow.GetWindow(typeof(DialogueEditorWindow));
		if(EditorWindow.focusedWindow != null){
			if(EditorWindow.focusedWindow.titleContent.text == "Dialogue Editor" || EditorWindow.focusedWindow.titleContent.text == "Variable Editor" || EditorWindow.focusedWindow.titleContent.text == "Theme Editor"){
				DialogueEditorDataManager.save();
				DialoguerEnumGenerator.GenerateDialoguesEnum();
			}
		}
		return paths;
	}
}
