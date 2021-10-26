cmake_minimum_required(VERSION 3.21)

set(CMAKE_CXX_STANDARD 20)

project(HauntedHouse)

include_directories(haunted-house
PUBLIC
    includes
    .
)

link_directories(haunted-house
PUBLIC
    libs
)

add_subdirectory(src)