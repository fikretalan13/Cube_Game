using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchSlider : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{

    public UnityAction OnPointerDownEvent;
    public UnityAction<float> OnPointerDragEvent;
    public UnityAction OnPointerUpEvent;

    Slider UISlider;

    private void Awake()
    {
        UISlider = GetComponent<Slider>();
        UISlider.onValueChanged.AddListener(OnSliderValueChanged);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownEvent?.Invoke();

        OnPointerDragEvent?.Invoke(UISlider.value);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpEvent?.Invoke();
        UISlider.value = 0;
    }

   

    void OnSliderValueChanged(float value)
    {
        OnPointerDragEvent?.Invoke(value);
    }

    private void OnDestroy()
    {
        UISlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

}
