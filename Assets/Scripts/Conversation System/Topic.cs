using UnityEngine;

[CreateAssetMenu(fileName = "New Topic", menuName = "Topic")]
public class Topic : ScriptableObject
{
    public new string name;
    public Topic[] relatedTo;
}
