using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//[RequireComponent(typeof(Tilemap))] //при добавлении автоматически добавляет компонент Tilemap и не позволяет его удалить, пока присутствует данный скрипт.
public class MoveByClick : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    private Tilemap _map;
    private Camera _camera;
        
    private void Start()
    {
        _map = GetComponent<Tilemap>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 clickWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition); //получаем координату позиции в которую кликнули мышкой на экране, при это преобразуем координату из глобальной в локальную
            Vector3Int clickCellPosition = _map.WorldToCell(clickWorldPosition); //не уловил сути, но сказано, что нужно сделать целое значение, чтобы попадать в ячейку, даже если кликнули немного правее или левее ячейки
            _player.transform.position = clickCellPosition; //перемещаем игрока в нужную позицию.
        }
    }
}
