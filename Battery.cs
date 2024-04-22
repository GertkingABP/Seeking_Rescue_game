using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Battery : MonoBehaviour
{
    private float chargeAmount; // Количество заряда, которое добавляется к фонарику
    public AudioClip pickupSound; // Звук подбора батарейки
    public Text pickupText; // Текст для отображения подсказки о подборе батарейки
    public GameObject player; // Игрок (можно также использовать тег для поиска игрока)

    private FlashLight flashlight; // Ссылка на скрипт фонарика
    private bool canPickup = false; // Флаг для проверки возможности подбора батарейки
    private bool isPickedUp = false; // Флаг для проверки, была ли батарейка уже подобрана

    private void Start()
    {
        flashlight = player.GetComponent<FlashLight>(); // Получаем компонент FlashLight из игрока
        HidePickupText(); // Начинаем с скрытия текста
    }

    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E) && !isPickedUp && flashlight.batteryCharge < 100) // Если игрок в зоне, нажата кнопка E, батарейка еще не подобрана и заряд меньше 100%
        {
            PickupBattery();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player) // Проверяем, что столкнулись с игроком
        {
            canPickup = true; // Разрешаем подбор батарейки
            ShowPickupText(); // Показываем текст подсказки
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player) // Проверяем, что игрок покинул зону
        {
            canPickup = false; // Запрещаем подбор батарейки
            HidePickupText(); // Скрываем текст подсказки
        }
    }

    private void ShowPickupText()
    {
        pickupText.gameObject.SetActive(true); // Включаем объект текста
        pickupText.text = "Нажмите E, чтобы подобрать"; // Устанавливаем текст
    }

    private void HidePickupText()
    {
        pickupText.gameObject.SetActive(false); // Отключаем объект текста
    }

    private void PickupBattery()
    {
        StartCoroutine(PickupSequence()); // Запускаем корутину
    }

    private IEnumerator PickupSequence()
    {
        isPickedUp = true; // Устанавливаем флаг, что батарейка подобрана
        chargeAmount = Random.Range(10, 100);
        pickupText.text = "Подобрана батарейка"; // Устанавливаем текст
        AudioSource.PlayClipAtPoint(pickupSound, transform.position); // Воспроизводим звук подбора батарейки
        pickupText.gameObject.SetActive(true); // Включаем объект текста
        yield return new WaitForSeconds(0.7f);
        flashlight.RechargeBattery(chargeAmount); // Пополняем заряд фонарика
        gameObject.SetActive(false); // Отключаем объект батарейки
        pickupText.gameObject.SetActive(false); // Выключаем объект текста
    }
}
