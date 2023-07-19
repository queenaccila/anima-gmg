using UnityEngine;
using UnityEditor;
using System.Collections;

public class DialogueEditorHelp : Editor {

	public const int PRIORITY = 2000;
	public const int CONTACT_PRIORITY = PRIORITY + 1000;

	//[MenuItem ("Dialoguer/Official Website", false, PRIORITY)]
	[MenuItem ("Tools/Dialoguer/Official Website", false, PRIORITY)]
	//[MenuItem ("Assets/Dialoguer/Official Website", false, PRIORITY)]
	[MenuItem ("Help/Dialoguer/Official Website", false, PRIORITY)]
	public static void Website(){
		Application.OpenURL("http://www.dialoguer.tonycoculuzzi.com");
	}

	//[MenuItem ("Dialoguer/Getting Started", false, PRIORITY)]
	[MenuItem ("Tools/Dialoguer/Getting Started", false, PRIORITY)]
	//[MenuItem ("Assets/Dialoguer/Getting Started", false, PRIORITY)]
	[MenuItem ("Help/Dialoguer/Getting Started", false, PRIORITY)]
	public static void GettingStarted(){
		Application.OpenURL("http://www.dialoguer.tonycoculuzzi.com/docs.php");
	}

	//[MenuItem ("Dialoguer/Nodes", false, PRIORITY)]
	[MenuItem ("Tools/Dialoguer/Nodes", false, PRIORITY)]
	//[MenuItem ("Assets/Dialoguer/Nodes", false, PRIORITY)]
	[MenuItem ("Help/Dialoguer/Nodes", false, PRIORITY)]
	public static void Nodes(){
		Application.OpenURL("http://www.dialoguer.tonycoculuzzi.com/nodes.php");
	}

	//[MenuItem ("Dialoguer/Code", false, PRIORITY)]
	[MenuItem ("Tools/Dialoguer/Code", false, PRIORITY)]
	//[MenuItem ("Assets/Dialoguer/Code", false, PRIORITY)]
	[MenuItem ("Help/Dialoguer/Code", false, PRIORITY)]
	public static void Code(){
		Application.OpenURL("http://www.dialoguer.tonycoculuzzi.com/code.php");
	}

	//[MenuItem ("Dialoguer/Code", false, PRIORITY)]
	[MenuItem ("Tools/Dialoguer/FAQ", false, PRIORITY)]
	//[MenuItem ("Assets/Dialoguer/Code", false, PRIORITY)]
	[MenuItem ("Help/Dialoguer/FAQ", false, PRIORITY)]
	public static void Faq(){
		Application.OpenURL("http://www.dialoguer.tonycoculuzzi.com/faq.php");
	}

	//[MenuItem ("Dialoguer/Contact", false, PRIORITY)]
	[MenuItem ("Tools/Dialoguer/Contact", false, CONTACT_PRIORITY)]
	//[MenuItem ("Assets/Dialoguer/Contact", false, PRIORITY)]
	[MenuItem("Help/Dialoguer/Contact", false, CONTACT_PRIORITY)]
	public static void Contact(){
		Application.OpenURL("mailto:tonycoculuzzi@gmail.com");
	}
}
