using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UIInputManager : MonoBehaviour
{
    [Serializable]
    public struct TypeInputPair
    {
        public PickupableSkillType skillType;
        public GameObject gameObject;
    }

    public List<TypeInputPair> TypeInputMap;

    private void Start()
    {
        GameManager.Instance.Player.OnPickupableSkillChange += (oldType, newType) =>
        {
            TypeInputMap.ForEach(pair =>
            {
                if(pair.gameObject != null)
                {
                    if (pair.skillType == newType)
                    {
                        pair.gameObject?.SetActive(true);
                    }
                    else
                    {
                        pair.gameObject?.SetActive(false);
                    }
                }
            });
        };
    }
}
