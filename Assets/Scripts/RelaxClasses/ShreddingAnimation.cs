using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShreddingAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform inputField;
    [SerializeField] private Button shredButton;
    [SerializeField] private float fallDistance = 800f; // how far down it travels
    [SerializeField] private float fallDuration = 0.6f; // how long it takes

    private Vector2 startPos;

    private void Start()
    {
        startPos = inputField.anchoredPosition;
        shredButton.onClick.AddListener(OnShredButtonClicked);
    }

    private void OnShredButtonClicked()
    {
        StartCoroutine(FallAndDisappear());
    }

    private IEnumerator FallAndDisappear()
    {
        var endPos = startPos - new Vector2(0f, fallDistance);
        var time = 0f;

        while (time < fallDuration)
        {
            time += Time.deltaTime;
            var t = time / fallDuration;
            inputField.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        inputField.gameObject.SetActive(false);

        inputField.anchoredPosition = startPos;
    }
}