using UnityEngine;

public interface IObjectPoolable
{
    public int PoolSize { get; }
    public string Tag { get; }
    public bool IsInUse { get; set; }
    public void Use();
    public void Release();
}