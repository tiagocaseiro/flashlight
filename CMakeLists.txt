cmake_minimum_required(VERSION 3.21)

set(CMAKE_CXX_STANDARD 20)

project(Game)

include_directories(
PUBLIC
    includes
)

link_directories(libs)

set (CMAKE_RUNTIME_OUTPUT_DIRECTORY_DEBUG ${CMAKE_BINARY_DIR})

add_subdirectory(shaders)
add_subdirectory(src)
add_subdirectory(cubemaps)
