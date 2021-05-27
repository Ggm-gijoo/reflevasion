using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;

    private Vector2 offset = Vector2.zero;
    private MeshRenderer meshRenderer = null;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        offset.x += speed * Time.deltaTime;
        meshRenderer.material.SetTextureOffset("_MainTex", offset);
    }

}
