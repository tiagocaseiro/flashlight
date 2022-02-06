#pragma once

#include <vector>

#include "learnopengl/shader.hpp"

class Skybox {
  public:
    Skybox();
    ~Skybox();
    void draw(Shader shader);

  private:
    uint32_t VAO;
    uint32_t VBO;
};