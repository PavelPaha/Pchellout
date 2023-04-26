using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectPlacer : MonoBehaviour
{
    public Tilemap tilemap; // ссылка на компонент Tilemap
    public TileBase tile; // тайл, которым заполняется ячейка при создании объекта
    public TileBase HiveTile;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Получаем координаты тайла в Tilemap
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePos = tilemap.WorldToCell(worldPos);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider != null) 
            {
                Debug.Log(hit.collider.gameObject.name);
                // Обработка пересечения с объектом на сцене
            }
            // Если тайл пустой, создаем объект на сетке
            // if (tilemap.GetTile(tilePos) == HiveTile)
            // {
            //     // Создаем префаб объекта и помещаем его на сетку
            //     GameObject obj = Instantiate(gameObject);
            //     obj.gameObject.transform.position += new Vector3(0, 0, -10);
            //     Vector3 objPos = tilemap.CellToWorld(tilePos) + new Vector3(tilemap.tileAnchor.x, tilemap.tileAnchor.y, 0f); // задаем позицию объекта в центре ячейки
            //     obj.transform.position = objPos;
            //     tilemap.SetTile(tilePos, tile);
            // }
            // else
            // {
            //     // Удаляем объект по клику мыши на заполненную ячейку
            //     // Destroy(tilemap.GetInstantiatedObject(tilePos));
            //     tilemap.SetTile(tilePos, HiveTile);
            // }
        }
    }
}