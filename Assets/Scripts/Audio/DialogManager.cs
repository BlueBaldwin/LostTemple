using System.Collections;
using UnityEngine;

namespace Audio
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] private AudioClip[] eventDialogClips;
        
        private SoundManager _soundManager;
        private static DialogManager _instance;
        private int _eventsTriggeredCount = 0;
        private bool _isIntroDialogPlayed = false;

        public static DialogManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<DialogManager>();
                    if (_instance == null)
                    {
                        Debug.LogError("DialogManager not found in the scene.");
                    }
                }

                return _instance;
            }
        }

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _soundManager = GetComponent<SoundManager>();
        }

        private void Start()
        {
            StartCoroutine(OpeningDialog());
        }

        private IEnumerator OpeningDialog()
        {
            PlayerController.CanMove = false;
            yield return new WaitForSeconds(1f);
            PlayEventDialog(0);
            yield return new WaitForSeconds(_soundManager.GetClipLength(eventDialogClips[0]));
            PlayerController.CanMove = true;
        }

        public void PlayEventDialog(int eventId)
        {
            if (eventId >= 0 && eventId < eventDialogClips.Length)
            {
                _soundManager.PlaySound(eventDialogClips[eventId], false);
                _eventsTriggeredCount++;
                if (!_isIntroDialogPlayed)
                {
                    _isIntroDialogPlayed = true;
                    StartCoroutine(PlayMusic(eventDialogClips[eventId]));
                }
            }
            else
            {
                Debug.LogError("Invalid event");
            }
        }

        public float GetDialogClipLength(int eventId)
        {
            if (eventId >= 0 && eventId < eventDialogClips.Length)
            {
                return eventDialogClips[eventId].length;
            }
            else
            {
                Debug.LogError("Invalid event");
                return 0f;
            }
        }
        
        public int GetEventsTriggeredCount()
        {
            return _eventsTriggeredCount;
        }
        
        private IEnumerator PlayMusic(AudioClip clip)
        {
            yield return new WaitForSeconds(clip.length);
            _soundManager.PlayMusic();
        }
    }
}