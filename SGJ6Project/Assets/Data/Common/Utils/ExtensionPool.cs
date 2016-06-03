﻿using UnityEngine;
using System.Collections.Generic;

namespace Common.Utils
{
    public static class ExtensionPool
    {
        private static Dictionary<GameObject, List<GameObject>> pools = new Dictionary<GameObject,List<GameObject>>();
        private static Dictionary<GameObject,  List<GameObject>> activeObjects = new Dictionary<GameObject,List<GameObject>>();

        public static void CreatePool(this GameObject prefab, bool persistent = false, int initialCount = 1)
        {
            List<GameObject> prefabPool = null;
            if (!ExtensionPool.pools.TryGetValue(prefab, out prefabPool))
            {
                //si no existe la creamos
                Debug.Log("Creando la pool de " + prefab.name);
                prefabPool = new List<GameObject>(initialCount);
                for (int i = 0; i < initialCount; ++i)
                {
                    GameObject instance = GameObject.Instantiate<GameObject>(prefab);
                    instance.SetActive(false);
                    prefabPool.Add(instance);
                    if (persistent)
                    {
                        GameObject.DontDestroyOnLoad(instance);
                    }
                }
                ExtensionPool.pools.Add(prefab, prefabPool);
            }
            else
            {
                Debug.LogWarning("Ya existia la pool de " + prefab.name);
            }
        }
        
        public static GameObject SpawnPool(this GameObject prefab)
        {
            return prefab.SpawnPool(Vector3.zero, Vector3.one, Quaternion.identity);
        }
        public static GameObject SpawnPool(this GameObject prefab, Vector3 position)
        {
            return prefab.SpawnPool(position, Vector3.one, Quaternion.identity);
        }
        public static GameObject SpawnPool(this GameObject prefab, Vector3 position, Vector3 scale)
        {
            return prefab.SpawnPool(position, scale, Quaternion.identity);
        }
        public static GameObject SpawnPool(this GameObject prefab, Vector3 position, Vector3 scale, Quaternion rotation)
        {
            GameObject result = null;
            List<GameObject> prefabPool = null;
            if (ExtensionPool.pools.TryGetValue(prefab, out prefabPool))
            {
                if (prefabPool.Count > 0)
                {
                    result = prefabPool[0];
                    activeObjects.Add(result, prefabPool);
                    prefabPool.RemoveAt(0);
                    result.SetActive(true);
                    Debug.Log("Spawn de " + prefab.name + ", lo sacamos de la pool");
                }
                else
                {
                    Debug.LogWarning("La pool de " + prefab.name + " está vacia, considerar incrementar el valor de creación");
                    result = GameObject.Instantiate<GameObject>(prefab);
                    activeObjects.Add(result, prefabPool);
                }
            }
            else
            {
                // Si no tiene pool se la creamos
                Debug.LogWarning(prefab.name + " no está en pool, le creamos una. Considerar crear pool al inicio");
                prefabPool = new List<GameObject>(1);
                pools.Add(prefab, prefabPool);
                result = GameObject.Instantiate<GameObject>(prefab);
                activeObjects.Add(result, prefabPool);
            }

            result.transform.localPosition = position;
            result.transform.rotation = rotation;

            return result;
        }

        public static void RecyclePool(this GameObject instance)
        {
            List<GameObject> prefabPool = null;
            if (activeObjects.TryGetValue(instance, out prefabPool))
            {
                Debug.Log("Reciclamos " + instance.name);
                activeObjects.Remove(instance);
                instance.SetActive(false);
                instance.transform.SetParent(null);
                instance.transform.position = Vector3.zero;
                prefabPool.Add(instance);
            }
            else
            {
                // Si por algún motivo no estaba activo lo borramos (por ejemplo dos reciclados del mismo objeto)
                Debug.LogWarning("Se intenta reciclar un objeto que no está activo en la pool "+ instance.name +" se va a destruir");
                GameObject.Destroy(instance);
            }
        }

        public static void ClearPool(this GameObject prefab)
        {
            List<GameObject> prefabPool = null;
            if (ExtensionPool.pools.TryGetValue(prefab, out prefabPool))
            {
                Debug.Log("Borrando la pool de " + prefab.name);
                for (int i = 0; i < prefabPool.Count; ++i)
                {
                    GameObject.Destroy(prefabPool[i]);
                }
                prefabPool.Clear();
                pools.Remove(prefab);
            }
            else
            {
                Debug.LogWarning("El objeto " + prefab.name + " no estaba en pool");
            }
        }
    }
}