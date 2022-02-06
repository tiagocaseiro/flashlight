#pragma once

#include <vector>

#include "learnopengl/shader.hpp"

class Skybox {
  public:
    Skybox(std::vector<std::string> faces);
    ~Skybox();
    void draw(Shader shader, const glm::mat4& view);

  private:
    uint32_t VAO;
    uint32_t VBO;
    uint32_t cubemap;
};