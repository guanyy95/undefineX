using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;


public static class EditorTools
{
    public class EditPrefabAssetScope : IDisposable
    {
        public readonly string assetPath;
        public readonly GameObject prefabRoot;

        public EditPrefabAssetScope(string assetPath)
        {
            this.assetPath = assetPath;
            prefabRoot = PrefabUtility.LoadPrefabContents(assetPath);
        }

        public void Dispose()
        {
            PrefabUtility.SaveAsPrefabAsset(prefabRoot, assetPath);
            PrefabUtility.UnloadPrefabContents(prefabRoot);
        }
    }
    
    public static void FilterShowAssets(int[] instanceIds)
    {
        MethodInfo m_GetBrowser = typeof(ProjectWindowUtil).GetMethod("GetProjectBrowserIfExists", BindingFlags.NonPublic | BindingFlags.Static);
        object browser = m_GetBrowser.Invoke(null, null);
        if (browser != null)
        {
            FieldInfo f_viewMode = browser.GetType().GetField("m_ViewMode", BindingFlags.NonPublic | BindingFlags.Instance);
            if (f_viewMode != null && (int)f_viewMode.GetValue(browser) == 1)
            {
                MethodInfo m_ShowObjects = browser.GetType().GetMethod("ShowObjectsInList", BindingFlags.NonPublic | BindingFlags.Instance);
                m_ShowObjects.Invoke(browser, new object[1] {instanceIds});
                return;
            }
        }

        Selection.instanceIDs = instanceIds;
    }
    
    [MenuItem("Assets/Find References", true)]
    static private bool ReferencesFindObj()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        return (!string.IsNullOrEmpty(path));
    }
    
    [MenuItem("Assets/Find Script References", false, 29)]
    static private void FindScriptReferences()
    {
        string curPathName = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());
        EditorUtility.DisplayProgressBar("searching references...", "", 0f);
        List<int> resultFileList = new List<int>();

        string[] allGuids = AssetDatabase.FindAssets("t:Prefab t:Scene", new string[] { "Assets/Scenes", "Assets/InBundle" });

        float i = 0;

        foreach (string guid in allGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            EditorUtility.DisplayProgressBar("searching references...", assetPath, i++ / allGuids.Length);

            string[] names = AssetDatabase.GetDependencies(new string[] { assetPath });
            foreach (string name in names)
            {
                if (name.Equals(curPathName))
                {
                    resultFileList.Add(AssetDatabase.LoadMainAssetAtPath(assetPath).GetInstanceID());
                    break;
                }
            }
        }

        FilterShowAssets(resultFileList.ToArray());
        EditorUtility.ClearProgressBar();
    }
    
    [MenuItem("Assets/Find Script References", true)]
    static private bool FindScriptReferences_ValidateFunc()
    {
        return Selection.activeObject is MonoScript;
    }

    [MenuItem("Util/Find Missing Prefab")]
    static void Init()
    {
        string[] allPrefabs = GetAllPrefabs();

        int count = 0;
        EditorUtility.DisplayProgressBar("Processing...", "Begin Job", 0);

        foreach (string prefab in allPrefabs)
        {
            UnityEngine.Object o = AssetDatabase.LoadMainAssetAtPath(prefab);

            if (o == null)
            {
                Debug.Log("prefab " + prefab + " null?");
                continue;
            }

            GameObject go;
            try
            {
                go = (GameObject)PrefabUtility.InstantiatePrefab(o);
                EditorUtility.DisplayProgressBar("Processing...", go.name, ++count / (float)allPrefabs.Length);
                FindMissingPrefabInGO(go, prefab, true);

                GameObject.DestroyImmediate(go);

            }
            catch
            {
                Debug.Log("For some reason, prefab " + prefab + " won't cast to GameObject");

            }
        }
        EditorUtility.ClearProgressBar();
    }
    
    static void FindMissingPrefabInGO(GameObject g, string prefabName, bool isRoot)
    {
        if (g.name.Contains("Missing Prefab"))
        {
            Debug.LogError($"{prefabName} has missing prefab {g.name}");
            return;

        }

        if (PrefabUtility.IsPrefabAssetMissing(g))
        {
            Debug.LogError($"{prefabName} has missing prefab {g.name}");
            return;
        }

        if (PrefabUtility.IsDisconnectedFromPrefabAsset(g))
        {
            Debug.LogError($"{prefabName} has missing prefab {g.name}");
            return;
        }

        if (!isRoot)
        {
            if (PrefabUtility.IsAnyPrefabInstanceRoot(g))
            {
                return;
            }

            GameObject root = PrefabUtility.GetNearestPrefabInstanceRoot(g);
            if (root == g)
            {
                return;
            }
        }

        // Now recurse through each child GO (if there are any):
        foreach (Transform childT in g.transform)
        {
            //Debug.Log("Searching " + childT.name  + " " );
            FindMissingPrefabInGO(childT.gameObject, prefabName, false);
        }
    }

    public static string[] GetAllPrefabs()
    {
        string[] temp = AssetDatabase.GetAllAssetPaths();
        List<string> result = new List<string>();
        foreach (string s in temp)
        {
            if (s.Contains(".prefab")) result.Add(s);
        }
        return result.ToArray();
    }
}