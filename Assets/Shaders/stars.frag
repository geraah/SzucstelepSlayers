#version 330

in vec2 fragTexCoord;

uniform vec2 resolution;
uniform float time;

out vec4 finalColor;

// 2D Forgatási mátrix
mat2 rotate(float angle) {
    float s = sin(angle);
    float c = cos(angle);
    return mat2(c, -s, s, c);
}

// 5-ágú csillag SDF (Signed Distance Field) funkciója
float sdStar(vec2 p, float r, float rf) {
    const vec2 k1 = vec2(0.809016994375, -0.587785252292);
    const vec2 k2 = vec2(-k1.x, k1.y);
    
    p.x = abs(p.x);
    p -= 2.0 * max(dot(k1, p), 0.0) * k1;
    p -= 2.0 * max(dot(k2, p), 0.0) * k2;
    p.x = abs(p.x);
    p.y -= r;
    
    vec2 ba = rf * vec2(-k1.y, k1.x) - vec2(0.0, 1.0);
    float h = clamp(dot(p, ba) / dot(ba, ba), 0.0, r);
    
    return length(p - ba * h) * sign(p.y * ba.x - p.x * ba.y);
}

// Egy adott csillag rétegének kirajzolása sávokkal
vec4 drawStarLayer(vec2 uv, vec2 center, float scale, float rotSpeed, float timeOffset) {
    vec2 p = uv - center;
    p *= rotate(time * rotSpeed * 0.9); // Forgás az idő függvényében
    p /= scale;
    
    // Csillag távolság kiszámítása
    float d = sdStar(p, 1.0, 0.45);
    
    // Csak a csillagon belüli területet sávosítjuk
    if (d < 0.0) {
        // A távolságból csináljuk a fekete-fehér csíkokat
        // A time hozzáadásával érjük el, hogy a csíkok kifelé mozogjanak
        float bands = step(0.5, fract(abs(d) * 8.0 - time * 0.5 + timeOffset));
        return vec4(vec3(bands), 1.0);
    }
    
    return vec4(0.0); // Ha a csillagon kívül van, átlátszó/fekete lesz a réteg
}

void main() {
    // KÉPERNYŐ TÉR HASZNÁLATA: A fragTexCoord helyett az abszolút pixeleket nézzük
    vec2 uv = gl_FragCoord.xy / resolution.xy;
    
    // Átalakítás -1..1 koordinátarendszerbe (középpont a képernyő közepe)
    uv = uv * 2.0 - 1.0;
    
    // Képarány korrigálása: Mivel a teljes képernyőt nézzük, a sima arányt használjuk
    uv.x *= resolution.x / resolution.y; 
    
    // --- INNENTŐL A KÓDOD UGYANAZ MARAD ---

    // Háttérszín (legalsó réteg - pl. fekete-fehér rács vagy sima szín, itt egy alap csillag)
    vec4 col = vec4(step(0.5, fract(length(uv) * 5.0 - time)), 0.0, 0.0, 1.0);
    col.rgb = vec3(step(0.5, fract(length(uv) * 10.0 - time * 0.3))); // Lassított körkörös háttér
    
    
    // Rápakoljuk a csillag rétegeket (a lassított értékekkel)

    // 1. FŐ CSILLAG: Hatalmas, lassan forgó az egész jobb oldal hátterében
    vec4 star1 = drawStarLayer(uv, vec2(0.9, -0.2), 2.5, 0.04, 0.0);
    if (star1.a > 0.0) col = star1;
    
    // 2. JOBB FELSŐ: Egy közepes csillag a jobb felső sarokban
    vec4 star2 = drawStarLayer(uv, vec2(1.4, 0.7), 1.0, -0.08, 0.5);
    if (star2.a > 0.0) col = star2;
    
    // 3. LENT KÖZÉPEN: Épphogy kilóg a vastag ferde sáv alól
    vec4 star3 = drawStarLayer(uv, vec2(0.4, -0.8), 0.8, 0.1, 1.2);
    if (star3.a > 0.0) col = star3;
    
    // 4. JOBB SZÉL: Félig lelóg a képernyőről a jobb oldalon
    vec4 star4 = drawStarLayer(uv, vec2(1.7, 0.1), 1.4, -0.05, 2.0);
    if (star4.a > 0.0) col = star4;
    
    // 5. FENT KÖZÉPEN: Egy kisebb csillag a képernyő tetején
    vec4 star5 = drawStarLayer(uv, vec2(0.6, 0.85), 0.6, 0.15, 3.1);
    if (star5.a > 0.0) col = star5;

    finalColor = col;

}