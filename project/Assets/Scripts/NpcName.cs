using TMPro;
using UnityEngine;

public class NpcName : MonoBehaviour
{
    TextMeshProUGUI textNameNpc;
    [SerializeField] string[] nameBot;
    [SerializeField] Transform targetLook;


    private void Awake()
    {
        textNameNpc = GetComponentInChildren<TextMeshProUGUI>();

        nameBot = new string[]
        {
            "ЖелезныйКлык", "ТеньОхотника", "ФантомХодок", "СтальнойСтраж", "НочнойГонщик", 
            "БуряМолнии", "ОгненныйШторм", "КиберВолк", "ЛедянойВетер", "ТоксичныйУкус", "ГрозовойЗмей", 
            "НебесныйКлинок", "РыцарьТьмы", "ЗвёздныйРазбойник", "ПылающийФеникс", "Иван Иванов", "Пётр Смирнов", 
            "Александр Кузнецов", "Дмитрий Орлов", "Михаил Соколов", "Сергей Волков", "Владимир Захаров", "Николай Романов", 
            "Андрей Морозов", "Евгений Фёдоров", "Максим Васильев", "Антон Григорьев", "Виктор Яковлев", "Георгий Сергеев", "Олег Лебедев"
        };
    }
    private void Start()
    {

        string randomName = GetRandomName();
        textNameNpc.text = randomName;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            targetLook = player.transform;
        }
    }

    private string GetRandomName()
    {
        int randomIndex = Random.Range(0, nameBot.Length); // Получаем случайный индекс
        return nameBot[randomIndex];
    }

    private void LateUpdate()
    {
        if (targetLook == null) return; // Если цель не найдена, ничего не делаем

        Vector3 lookDirection = targetLook.position - transform.position;
        lookDirection.y = 0; // Игнорируем вертикальную составляющую, чтобы NPC не смотрел вверх/вниз
        transform.rotation = Quaternion.LookRotation(lookDirection);
        textNameNpc.transform.rotation = Quaternion.Euler(0, 180, 0) * transform.rotation;
    }
}
