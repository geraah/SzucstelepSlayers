#version 330

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D texture0;
uniform sampler2D bgTexture;
uniform vec2 resolution;

out vec4 finalColor;

void main()
{
    vec4 texelColor = texture(texture0, fragTexCoord);
    if (texelColor.a < 0.1) discard;

    // Normál koordináták: gl_FragCoord az aktuális pixel helye (0,0 a bal alsó sarok)
    // Itt NEM fordítjuk meg az Y tengelyt, hagyjuk az eredeti irányt
    vec2 uv = gl_FragCoord.xy / resolution;

    vec4 bg = texture(bgTexture, uv);

    // Háttér fényessége
    float brightness = dot(bg.rgb, vec3(0.299, 0.587, 0.114));

    // Piros háttér detektálása (ha a piros csatorna erős, a többi gyenge)
    bool isRedBackground = (bg.r > 0.5 && bg.g < 0.3 && bg.b < 0.3);
    
    // Fehér háttér detektálása (magas fényesség)
    bool isWhiteBackground = (brightness > 0.8);

    vec3 textCol;

    if (isWhiteBackground) {
        // Fehér hátteren: FEKETE szöveg
        textCol = vec3(0.0, 0.0, 0.0);
    } 
    else if (isRedBackground) {
        // Piros hátteren: FEHÉR szöveg (kontraszt miatt)
        textCol = vec3(1.0, 1.0, 1.0);
    }
    else {
        // Minden más (fekete/sötét) hátteren: PIROS szöveg
        //textCol = vec3(1.0, 0.0, 0.0); 
        textCol = vec3(0.90196078431, 0.16078431372, 0.21568627451); 
    }

    //ezzel csak fekete feher lesz
    //textCol = (brightness > 0.5) ? vec3(0.0) : vec3(1.0);

    //ezzel fekete piros
    //textCol = (brightness > 0.5) ? vec3(0.0) : vec3(0.90196078431, 0.16078431372, 0.21568627451);

    // A végső szín a betű textúrájának átlátszóságával
    finalColor = vec4(textCol, texelColor.a * fragColor.a);
}