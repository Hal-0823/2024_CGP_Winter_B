using UnityEngine;

[CreateAssetMenu(fileName="ProjectInfo", menuName="Project Info", order=1)]
public class ProjectInfo : ScriptableObject
{
    [TextArea(minLines: 5, maxLines: 30)]
     public string description;
}
