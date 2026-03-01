#version 330

in vec2 fragTexCoord;
in vec4 fragColor;

uniform vec2 resolution;
uniform float time;

out vec4 finalColor;

float star(vec2 p, float size) {
    float angle = atan(p.y, p.x);
    float radius = length(p);
    float sides = 5.0;
    float starShape = cos(floor(0.5 + angle / (2.0 * 3.14159) * sides) * (2.0 * 3.14159) / sides - angle) * radius;
    return starShape;
}

void main() {
    vec2 uv = (fragTexCoord * resolution - resolution * 0.5) / resolution.y;
    
    float t = time * 0.3;
    
    float pattern = 0.0;
    float scale = 1.0;
    
    for (int i = 0; i < 5; i++) {
        vec2 p = uv * scale;
        float s = star(p, 1.0);
        float ring = mod(s - t, 0.15) / 0.15;
        pattern += ring / scale;
        scale *= 1.8;
    }
    
    pattern = mod(pattern, 1.0);
    
    vec3 color = mix(vec3(0.0), vec3(1.0), step(0.5, pattern));
    
    finalColor = vec4(color, 1.0);
}