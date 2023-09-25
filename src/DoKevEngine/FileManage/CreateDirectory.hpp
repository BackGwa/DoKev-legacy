#pragma once

/* CreateDirectory : 현재 경로를 반환합니다. */
bool CreateDirectory() {
    return filesystem::create_directory("bin");
}