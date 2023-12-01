using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnPrefab : MonoBehaviour, IPointerDownHandler
{
    public GameObject prefabToSpawn;
    public Transform spawnPoint; // Aseg�rate de que este tenga un VerticalLayoutGroup
    public UIManagerSelect uiManagerSelect; // Referencia al UIManager que maneja la selecci�n

    // Se ha eliminado el m�todo Update porque la l�gica se manejar� con IPointerDownHandler

    public void OnPointerDown(PointerEventData eventData)
    {
        // Comprobar que el clic es con el bot�n izquierdo del mouse
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SpawnObject();
        }
    }

    public void SpawnObject()
    {
        if (prefabToSpawn != null && uiManagerSelect != null)
        {
            Transform parentTransform = spawnPoint ? spawnPoint : transform;

            // Instanciar el objeto como hijo de parentTransform
            GameObject newObject = Instantiate(prefabToSpawn, parentTransform, false);

            // A�adir el EventTrigger al objeto instanciado
            EventTrigger trigger = newObject.AddComponent<EventTrigger>();

            // Crear una nueva entrada de evento para el clic
            EventTrigger.Entry clickEntry = new EventTrigger.Entry();
            clickEntry.eventID = EventTriggerType.PointerClick;
            // Asignar una llamada al m�todo OnElementClicked en UIManagerSelect
            clickEntry.callback.AddListener((data) => { uiManagerSelect.OnElementClicked((PointerEventData)data); });

            // A�adir la entrada al EventTrigger del objeto
            trigger.triggers.Add(clickEntry);
        }
    }
}
