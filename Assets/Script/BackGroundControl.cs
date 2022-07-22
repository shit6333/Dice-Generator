using UnityEngine;

public class BackGroundControl : MonoBehaviour
{
    public float bgMoveSpeed = 0.01f;
    Renderer bgMaterial;

    private void Start()
    {
        bgMaterial = GetComponent<Renderer>();
    }

    void Update()
    {
        Vector2 bgOffset = bgMaterial.material.GetTextureOffset("_MainTex");
        bgMaterial.material.SetTextureOffset("_MainTex", new Vector2(bgOffset.x + bgMoveSpeed, bgOffset.y + bgMoveSpeed));
    }

}
