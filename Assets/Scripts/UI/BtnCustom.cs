using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnCustom : MonoBehaviour
{
    private Button button;
    private Vector3 originalScale;
    private AudioSource audioSource;

    public AudioClip hoverSound;

    void Start()
    {
        // Lấy tham chiếu đến Button component
        button = GetComponent<Button>();

        // Lưu giữ kích thước ban đầu của button
        originalScale = transform.localScale;

        // Thêm AudioSource vào GameObject và cấu hình nó
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Đặt âm thanh muốn sử dụng khi rê chuột vào
        audioSource.clip = hoverSound;

        // Đăng ký sự kiện khi con trỏ chuột vào và ra khỏi button
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
        pointerEnter.eventID = EventTriggerType.PointerEnter;
        pointerEnter.callback.AddListener((data) => { OnPointerEnter(); });
        trigger.triggers.Add(pointerEnter);

        EventTrigger.Entry pointerExit = new EventTrigger.Entry();
        pointerExit.eventID = EventTriggerType.PointerExit;
        pointerExit.callback.AddListener((data) => { OnPointerExit(); });
        trigger.triggers.Add(pointerExit);
    }

    void OnPointerEnter()
    {
        // Khi con trỏ chuột vào, thay đổi kích thước của button
        transform.localScale = originalScale * 2f;

        // Phát âm thanh
        audioSource.Play();
    }

    void OnPointerExit()
    {
        // Khi con trỏ chuột ra khỏi, khôi phục kích thước ban đầu của button
        transform.localScale = originalScale;
    }
}