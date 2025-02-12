using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

namespace MouseOverUI
{
    public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject descriptionPanel; // 説明用パネル
        public TMP_Text descriptionText; // 説明文
        public string message; // 表示するメッセージ

        private RectTransform panelRect; // 説明用パネルの RectTransform
        private Canvas canvas; // Canvas
        private RectTransform canvasRect; // CanvasのRectTransform

        // フェード用
        public CanvasGroup canvasGroup;
        public float fadeDuration = 0.5f;

        // マウスカーソルと説明文の距離
        public Vector2 descriptionOffset = new Vector2(0f, -30f);

        private void Start()
        {
            panelRect = descriptionPanel.GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            canvasRect = canvas.GetComponent<RectTransform>(); // CanvasのRectTransformを取得
            descriptionPanel.SetActive(false); // 初期状態は非表示
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            descriptionPanel.SetActive(true);
            descriptionText.text = message;
            FitPanelToText(); // パネルをテキストサイズに合わせる
            SetDescriptionPosition(); // 説明文の位置を設定
            ClampPanelPosition(); // パネルが画面外に出ないように調整する

            StartCoroutine(Fade(0f, 1f));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            descriptionPanel.SetActive(false);
        }

        private System.Collections.IEnumerator Fade(float startAlpha, float endAlpha)
        {
            float time = 0f;
            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
                yield return null;
            }
            canvasGroup.alpha = endAlpha;
        }

        private void FitPanelToText()
        {
            // テキストのレイアウトを強制更新（サイズ計算ミスを防ぐ）
            LayoutRebuilder.ForceRebuildLayoutImmediate(descriptionText.rectTransform);

            // テキストの幅と高さを取得
            float textWidth = descriptionText.preferredWidth;
            float textHeight = descriptionText.preferredHeight;

            // 最小サイズを設定（必要なら調整）
            float minWidth = 100f;  // 必要に応じて調整
            float minHeight = 30f;

            // パネルのサイズを設定（Paddingを考慮）
            panelRect.sizeDelta = new Vector2(
                Mathf.Max(textWidth + 20f, minWidth),
                Mathf.Max(textHeight + 20f, minHeight)
            );
        }

        private void SetDescriptionPosition()
        {
            Vector2 screenMousePosition = Input.mousePosition;
            Vector3 worldPoint;

            // マウス位置をワールド座標に変換
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                canvasRect, screenMousePosition, canvas.worldCamera, out worldPoint
            );

            // ワールド座標を適用
            descriptionPanel.transform.position = worldPoint + (Vector3)descriptionOffset;
        }

        private void ClampPanelPosition()
        {
            Vector2 clampedPosition = panelRect.anchoredPosition;

            float canvasWidth = canvasRect.rect.width / canvas.scaleFactor;
            float canvasHeight = canvasRect.rect.height / canvas.scaleFactor;
            float panelWidth = panelRect.rect.width;
            float panelHeight = panelRect.rect.height;

            // 画面外に出ないように調整
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, 0, canvasWidth - panelWidth);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, 0, canvasHeight - panelHeight);

            panelRect.anchoredPosition = clampedPosition;
        }
    }
}