using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace B_Extensions.HierarchyStates
{
    [RequireComponent(typeof(NavigationRecordInstaller))]
    public class NavigationRecordController : Singleton<NavigationRecordController>
    {
        [field:SerializeField] public List<StateSetter> ignoreElements = new List<StateSetter>();
        public List<StateSetter> sceneSetters { get; private set; } = new List<StateSetter>();
        /// <summary>
        /// first list layer, is the records storage, second layer is for each GameObject on scene
        /// </summary>
        private static List<List<RecordElement>> record = new List<List<RecordElement>>();
        
        public HierarchyActivator Activator => transform.GetComponentInChildren<HierarchyActivator>();
        public NavigationRecordSearch Searcher => transform.GetComponentInChildren<NavigationRecordSearch>();

        #region Monobehaviours Methods
        protected override void Awake()
        {
            base.Awake();
            transform.SetParent(null);
        }
        

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                print(record.Count);
                //record.Last().ForEach(r => print(r.reference.handler.name));
            }
        }
        private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

        private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;
        #endregion

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (SceneManager.GetActiveScene().name.Equals(record.Last()[0].reference.sceneName))
            {
                StartCoroutine(ListenAmountOfSingleton());
            }
        }
        public void ScanElements()
        {
            record.Add(GetListElementsInScene());
            LimitRecord();
        }

        public void ResetStateFromParent(Transform parent)
        {
            var items = parent.GetComponentsInChildren<StateSetter>(true).ToList();
            items.ForEach(i => i.EnableOnHierarchy());
        }

        public void PopElement()
        {
            if (record.Count == 0)
            {
                print("NO HAY MÁS HISTORIAL");
                return;
            }
            ActivateLastElement();
        }

        private void ActivateLastElement()
        {
            bool remove = false;
            record.Last().ForEach(i =>
            {
                bool state = i.stateInHierarchy;
                remove = i.reference.SetActive(state);
            });
            if (remove)
                record.Remove(record.Last());
        }

        public void ClearRecord() => record.Clear();

        private void LimitRecord()
        {
            if (record.Count > 30)
                record.RemoveAt(0);
        }

        // Wait until the scene was load to active elements
        IEnumerator ListenAmountOfSingleton()
        {
            yield return new WaitForEndOfFrame();
            record.Last().ForEach(i =>
            {
                bool state = i.stateInHierarchy;
                i.reference.SetActive(state);
            });
            yield return new WaitForSecondsRealtime(0.05f);
            if(gameObject != null)
                record.Remove(record.Last());

            //print("TODO CARGAR AL CARGAR LA ESCENA");
        }

        List<RecordElement> GetListElementsInScene()
        {
            var elements = new List<RecordElement>();
#if UNITY_2019
            var elementsFound = Resources.FindObjectsOfTypeAll<StateSetter>();
            foreach (var item in elementsFound)
            {
                if (!item.gameObject.scene.isLoaded)
                    continue;
                elements.Add(new RecordElement
                {
                    reference = item.reference,
                    stateInHierarchy = item.gameObject.activeInHierarchy
                });
            }

#else
            var elementsFound = GameObject.FindObjectsByType<StateSetter>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var item in elementsFound)
            {
                // ignore objects
                if (ignoreElements.Contains(item))
                    continue;
                elements.Add(new RecordElement
                {
                    reference = item.reference,
                    stateInHierarchy = item.gameObject.activeInHierarchy
                });
            }
#endif
            return elements;
        }
    }
}
