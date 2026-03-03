using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SzűcstelepSlayers {
    internal interface IHittable {

        void OnHit(Player player, float damage, Vector2 knockback);
        
    }
}
