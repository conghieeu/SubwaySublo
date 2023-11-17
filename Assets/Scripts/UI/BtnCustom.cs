using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnCustom : MonoBehaviour
{
    [SerializeField] Transform whoCallThis;
    public AudioClip hoverSound;

    [Space]
    [Tooltip("Đây là đối tượng nó sẽ kích hoạt hoặc tắt đi khi nhấn vào và điều kiện của nó là isActiveOther")]
    [SerializeField] Transform otherActive;
    [SerializeField] BtnCustom otherBtnBack;
    [SerializeField] Transform thisMainUI; // là giao diện cha lưu trữ thằng này
    [SerializeField] bool isActiveOther = true;
    [SerializeField] bool isThisSwitch;

    Button button;
    Vector3 originalScale;
    AudioSource audioSource;

    public Transform WhoCallThis { get => whoCallThis; set => whoCallThis = value; }

    void Start()
    {
        SetPointerEnter();
    }

    public void GoBack()
    {
        otherActive = WhoCallThis;
        SetActiveOther();
    }

    public void SetActiveOther()
    {
        if (isThisSwitch)
        {
            isActiveOther = !isActiveOther;
        }

        otherActive.gameObject.SetActive(isActiveOther);

        if (thisMainUI)
        {
            thisMainUI.gameObject.SetActive(!isActiveOther);

            if (otherBtnBack)
                otherBtnBack.WhoCallThis = thisMainUI;
        }


    }

    void SetPointerEnter()
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