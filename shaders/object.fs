#version 330 core
out vec4 FragColor;

in vec2 TexCoords;
in vec3 Normal;
in vec3 FragPos;

struct Material {
    float shininess;
    sampler2D texture_diffuse;
    sampler2D specular;
};

struct Spotlight {
    vec3 position;
    float cutOff;
    float outerCutOff;
    vec3 direction;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

struct DirectionalLight {
    vec3 direction;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

uniform DirectionalLight directional_light;
uniform Spotlight spotlight;

uniform Material material;

uniform vec3 viewPos;

vec3 CalculateDirectionalLight(vec3 norm) {
    // ambient
    vec3 ambient = directional_light.ambient * texture(material.texture_diffuse, TexCoords).rgb;

    // diffuse
    float diff = max(dot(norm, -directional_light.direction), 0.0);
    vec3 diffuse =
        directional_light.diffuse * diff * texture(material.texture_diffuse, TexCoords).rgb;

    // specular
    vec3 viewDir    = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-directional_light.direction, norm);
    float spec      = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular   = directional_light.specular * spec * texture(material.specular, TexCoords).rgb;

    return ambient + diffuse + specular;
}

vec3 CalculateSpotlight(vec3 norm) {
    // ambient
    vec3 ambient = spotlight.ambient * texture(material.texture_diffuse, TexCoords).rgb;

    vec3 lightDir = normalize(spotlight.position - FragPos);
    float cutoff  = dot(normalize(spotlight.direction), normalize(FragPos - spotlight.position));

    float theta     = dot(lightDir, normalize(-spotlight.direction));
    float epsilon   = spotlight.cutOff - spotlight.outerCutOff;
    float intensity = clamp((theta - spotlight.outerCutOff) / epsilon, 0.0, 1.0);
    // diffuse
    float diff   = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = spotlight.diffuse * diff * texture(material.texture_diffuse, TexCoords).rgb;

    // // specular
    // vec3 viewDir    = normalize(viewPos - FragPos);
    // vec3 reflectDir = reflect(-lightDir, norm);
    // float spec      = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    // vec3 specular   = spotlight.specular * spec * texture(material.specular, TexCoords).rgb;

    return ambient + diffuse * intensity;
}

void main() {
    vec3 norm = normalize(Normal);
    FragColor = vec4(CalculateDirectionalLight(norm) + CalculateSpotlight(norm), 1.0);
}