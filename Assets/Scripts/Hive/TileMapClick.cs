using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapClick : MonoBehaviour
{
    public Tilemap tilemap; // Ссылка на Tilemap
    public GameObject objectPrefab; // Ссылка на ваш объект префаб
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // создаем луч в точке клика мышью
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null)
            {
                // получаем компонент Tilemap
                Tilemap tilemap = hit.collider.gameObject.GetComponent<Tilemap>();

                if (tilemap != null)
                {
                    Debug.Log("Объект Tilemap найден.");
                    Debug.Log("Тип объекта Tilemap: " + tilemap.GetType());
                }
                else
                {
                    Debug.Log("Объект Tilemap не найден.");
                }
            }
        }
        
        // if (Input.GetMouseButtonDown(0))
        // {
        //     // Конвертируем позицию мыши на экране в мировую позицию
        //     Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     // Определяем ячейку на Tilemap, на которую был сделан клик
        //     Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        //     // Получаем позицию левого нижнего угла выбранной ячейки
        //     Vector3 cellBottomLeftWorldPos = tilemap.CellToWorld(cellPosition);
        //
        //     // Создаем объект на позиции левого нижнего угла ячейки
        //     GameObject instance = Instantiate(objectPrefab, cellBottomLeftWorldPos, Quaternion.identity);
        //
        //     // Устанавливаем позицию Z для объекта, чтобы избежать перекрытия с тайлами
        //     instance.transform.position = new Vector3(instance.transform.position.x, instance.transform.position.y, -1f);
        // }
    }
}
