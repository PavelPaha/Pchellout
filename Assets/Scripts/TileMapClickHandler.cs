using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapClickHandler : MonoBehaviour
{
    public Tilemap tilemap; // Ссылка на Tilemap
    public GameObject objectPrefab; // Ссылка на ваш объект префаб
    void Update()
    {
        // Проверяем нажатие левой кнопки мыши
        if (Input.GetMouseButtonDown(0))
        {
            // Конвертируем позицию мыши на экране в мировую позицию
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Определяем ячейку на Tilemap, на которую был сделан клик
            Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
            // Получаем позицию левого нижнего угла выбранной ячейки
            Vector3 cellBottomLeftWorldPos = tilemap.CellToWorld(cellPosition);

            // Создаем объект на позиции левого нижнего угла ячейки
            GameObject instance = Instantiate(objectPrefab, cellBottomLeftWorldPos, Quaternion.identity);

            // Устанавливаем позицию Z для объекта, чтобы избежать перекрытия с тайлами
            instance.transform.position = new Vector3(instance.transform.position.x, instance.transform.position.y, -1f);
        }
    }


}