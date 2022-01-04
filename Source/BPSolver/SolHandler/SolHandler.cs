using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPSolver.Enums;
using BPSolver.Objects;

namespace BPSolver
{
    internal partial class SolHandler : ISolver
    {
        private const string RootName = "Cloned Root";

        public SolHandler()
        {
            CreatePieceSet();
        }


        // Matriz de Valores Preferencia
        private int[,] Preferences = new int[Constants.Rank, Constants.Rank]
        {
            {
                8,
                7,
                6,
                5,
                4,
                4,
                5,
                6,
                7,
                8
            },
            {
                7,
                6,
                5,
                4,
                3,
                3,
                4,
                5,
                6,
                7
            },
            {
                6,
                5,
                4,
                3,
                2,
                2,
                3,
                4,
                5,
                6
            },
            {
                5,
                4,
                3,
                2,
                1,
                1,
                2,
                3,
                4,
                5
            },
            {
                4,
                3,
                2,
                1,
                0,
                0,
                1,
                2,
                3,
                4
            },
            {
                4,
                3,
                2,
                1,
                0,
                0,
                1,
                2,
                3,
                4
            },
            {
                5,
                4,
                3,
                2,
                1,
                1,
                2,
                3,
                4,
                5
            },
            {
                6,
                5,
                4,
                3,
                2,
                2,
                3,
                4,
                5,
                6
            },
            {
                7,
                6,
                5,
                4,
                3,
                3,
                4,
                5,
                6,
                7
            },
            {
                8,
                7,
                6,
                5,
                4,
                4,
                5,
                6,
                7,
                8
            }
        };

    }
}
