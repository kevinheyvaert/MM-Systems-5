﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanzenbordWPF
{
    class Bord
    {
        //Grid coordinaten eerste is kollom en tweede is rij getal is de rij
        public int[,] plaats = new int[63, 2]{
        {0,6},{1,6},{2,6},{3,6},{4,6},{5,6},{6,6},{7,6},{8,6},
        {8,5},{8,4},{8,3},{8,2},{8,1},{8,0},
        {7,0},{6,0},{5,0},{4,0},{3,0},{2,0},{1,0},{0,0},
        {0,1},{0,2},{0,3},{0,4},{0,5},
        {1,5},{2,5},{3,5},{4,5},{5,5},{6,5},{7,5},
        {7,4},{7,3},{7,2},{7,1},
        {6,1},{5,1},{4,1},{3,1},{2,1},{1,1},
        {1,2},{1,3},{1,4},
        {2,4},{3,4},{4,4},{5,4},{6,4},
        {6,3},{6,2},
        {5,2},{4,2},{3,2},{2,2},
        {2,3},
        {3,3},{4,3},{5,3}};
    }
}