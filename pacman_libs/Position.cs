﻿using utils_libs.Abstractions;

namespace pacman_libs
{
    public struct Position : IPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}