using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class GameState
{
    public PlayerData PlayerData { get; set; }
    public List<PoolObjectData> PoolObjects { get; set; }
}

