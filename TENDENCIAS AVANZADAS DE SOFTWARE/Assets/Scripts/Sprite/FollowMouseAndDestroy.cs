using UnityEngine;
using UnityEngine.EventSystems;

public class FollowMouseAndDestroy : MonoBehaviour
{
    private Camera mainCamera;
    public bool isFollowingMouse = false;
    public bool statusContent;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Revisar si el bot�n izquierdo del rat�n est� siendo presionado
        if (Input.GetMouseButton(0))
        {
            isFollowingMouse = true;
        }

        // Si se suelta el bot�n izquierdo del rat�n, dejar de seguirlo y destruir el objeto
        if (Input.GetMouseButtonUp(0))
        {
            isFollowingMouse = false;
            if (statusContent)
            {
                this.transform.SetParent(UIManager.Instance.contentTransform);
            }
            else
            {
               Destroy(gameObject);   
            }
        }

        // Si se est� siguiendo el rat�n, actualizar la posici�n del objeto
        if (isFollowingMouse)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)); // 10 es una profundidad arbitraria, ajusta seg�n necesites
            transform.position = mousePosition;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WorkArea"))
        {
            statusContent = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WorkArea"))
        {
            statusContent=false;
        }
    }

    private void OnMouseEnter()
    {
        isFollowingMouse=true;
    }

    private void OnMouseExit()
    {
        isFollowingMouse=false;
    }
}

