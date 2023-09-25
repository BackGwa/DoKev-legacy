#pragma once

/* CurrentPath : 현재 경로를 반환합니다. */
string CurrentPath() {
    return filesystem::current_path().string();
}