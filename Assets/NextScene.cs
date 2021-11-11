using Unity.LEGO.Game;
using UnityEngine;


    public class NextScene : MonoBehaviour
    {
        [Header("References")]

        [SerializeField, Tooltip("The canvas group that contains the fade image.")]
        CanvasGroup m_CanvasGroup = default;

        [Header("Fade")]

        [SerializeField, Tooltip("The delay in seconds before fading starts when winning.")]
        const float m_WinDelay = 4.0f;

        [SerializeField, Tooltip("The delay in seconds before fading starts when losing.")]
        const float m_LoseDelay = 2.0f;
        

        [SerializeField, Tooltip("The duration in seconds of the fade.")]
        float m_Duration = 1.0f;

        float m_Time;
        bool m_GameOver;
        bool m_gameStart;
        bool m_Won;

    public float Delay => m_LoseDelay + 1;

    void Start()
    {
        m_CanvasGroup.gameObject.SetActive(true);
        m_gameStart = true;
    }

        void Update()
        {
            if (m_GameOver)
            {
                // Update time.
                m_Time += Time.deltaTime;

                m_CanvasGroup.alpha = Mathf.Clamp01((m_Time - m_LoseDelay) / m_Duration);
            }

        if (m_gameStart)
        {
            // Update time.
            m_Time += Time.deltaTime;

            m_CanvasGroup.alpha = Mathf.Clamp01(( m_LoseDelay - m_Time) / m_Duration);
        }

        if (m_gameStart && m_Time >= m_LoseDelay)
        {
            m_gameStart = false;
            m_Time = 0;
            m_CanvasGroup.gameObject.SetActive(false) ;
        }
    }

        public void nextScene()
        {
        m_Time = 0;
                m_GameOver = true;
                m_CanvasGroup.gameObject.SetActive(true);
                m_GameOver = true;
                m_Won = false;
        }
    }
