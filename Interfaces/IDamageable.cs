using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SzűcstelepSlayers {
    public interface IDamageable {
    
        float Damage { get; }
        void TakeDamage(float amount, Vector2 knockback);

    }
}
