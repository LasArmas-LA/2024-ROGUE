using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

namespace MouseOverUI
{
    public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject descriptionPanel; // �����p�p�l��
        public TMP_Text descriptionText; // ������
        public string message; // �\�����郁�b�Z�[�W

        private RectTransform panelRect; // �����p�p�l���� RectTransform
        private Canvas canvas; // Canvas
        private RectTransform canvasRect; // Canvas��RectTransform

        // �t�F�[�h�p
        public CanvasGroup canvasGroup;
        public float fadeDuration = 0.5f;

        // �}�E�X�J�[�\���Ɛ������̋���
        public Vector2 descriptionOffset = new Vector2(0f, -30f);

        private void Start()
        {
            panelRect = descriptionPanel.GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            canvasRect = canvas.GetComponent<RectTransform>(); // Canvas��RectTransform���擾
            descriptionPanel.SetActive(false); // ������Ԃ͔�\��
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            descriptionPanel.SetActive(true);
            descriptionText.text = message;
            FitPanelToText(); // �p�l�����e�L�X�g�T�C�Y�ɍ��킹��
            SetDescriptionPosition(); // �������̈ʒu��ݒ�
            ClampPanelPosition(); // �p�l������ʊO�ɏo�Ȃ��悤�ɒ�������

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
            // �e�L�X�g�̃��C�A�E�g�������X�V�i�T�C�Y�v�Z�~�X��h���j
            LayoutRebuilder.ForceRebuildLayoutImmediate(descriptionText.rectTransform);

            // �e�L�X�g�̕��ƍ������擾
            float textWidth = descriptionText.preferredWidth;
            float textHeight = descriptionText.preferredHeight;

            // �ŏ��T�C�Y��ݒ�i�K�v�Ȃ璲���j
            float minWidth = 100f;  // �K�v�ɉ����Ē���
            float minHeight = 30f;

            // �p�l���̃T�C�Y��ݒ�iPadding���l���j
            panelRect.sizeDelta = new Vector2(
                Mathf.Max(textWidth + 20f, minWidth),
                Mathf.Max(textHeight + 20f, minHeight)
            );
        }

        private void SetDescriptionPosition()
        {
            Vector2 screenMousePosition = Input.mousePosition;
            Vector3 worldPoint;

            // �}�E�X�ʒu�����[���h���W�ɕϊ�
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                canvasRect, screenMousePosition, canvas.worldCamera, out worldPoint
            );

            // ���[���h���W��K�p
            descriptionPanel.transform.position = worldPoint + (Vector3)descriptionOffset;
        }

        private void ClampPanelPosition()
        {
            Vector2 clampedPosition = panelRect.anchoredPosition;

            float canvasWidth = canvasRect.rect.width / canvas.scaleFactor;
            float canvasHeight = canvasRect.rect.height / canvas.scaleFactor;
            float panelWidth = panelRect.rect.width;
            float panelHeight = panelRect.rect.height;

            // ��ʊO�ɏo�Ȃ��悤�ɒ���
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, 0, canvasWidth - panelWidth);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, 0, canvasHeight - panelHeight);

            panelRect.anchoredPosition = clampedPosition;
        }
    }
}