#pragma once

/* CurrentPath : 현재 경로를 반환합니다. */
std::string CurrentPath() {
    return std::filesystem::current_path().string();
}