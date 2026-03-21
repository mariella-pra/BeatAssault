using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;

public class LoadAlembicAtRuntime : MonoBehaviour
{
    public AlembicStreamPlayer alembicPlayer;
    public string name;
    void Start()
    {
        string alembicPath = Application.streamingAssetsPath + $"/{name}.abc";
        alembicPlayer.LoadFromFile(alembicPath);
    }
}