add_executable(haunted-house
    main.cpp
    glad.c
)

target_link_libraries(haunted-house
PUBLIC
    glfw3
)