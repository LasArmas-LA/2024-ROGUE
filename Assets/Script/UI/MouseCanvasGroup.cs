using UnityEngine;
using UnityEngine.EventSystems;

namespace MouseCanvasGroup
{
    public class MouseCanvasGroup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public CanvasGroup canvasGroup;
        public float fadeDuration = 0.5f;

        public void OnPointerEnter(PointerEventData eventData)
        {
            FadeIn();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            FadeOut();
        }

        private void FadeIn()
        {
            StartCoroutine(Fade(0.5f, 1f));
        }

        private void FadeOut()
        {
            StartCoroutine(Fade(1f, 0.5f));
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
    }
}
