using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;

namespace SzűcstelepSlayers {
    public interface IDamageable {
    
        float Damage { get; }

        Rectangle Hitbox { get; }

        void TakeDamage(float amount, Vector2 knockback);

    }
}
