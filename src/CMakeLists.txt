add_executable(game
    main.cpp
    skybox.cpp
    glad.c
)

target_link_libraries(game
PUBLIC
    glfw3
)