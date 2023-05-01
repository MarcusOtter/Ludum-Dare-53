using UnityEngine;

public class EditorDescription : MonoBehaviour
{
	[TextArea(5, 10)]
	[SerializeField] private string description;
}
