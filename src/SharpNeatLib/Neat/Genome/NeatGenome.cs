﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpNeat.Core;
using SharpNeat.Network;

namespace SharpNeat.Neat.Genome
{
    public class NeatGenome : IGenome
    {
        #region Instance Fields

        readonly uint _id;
        // TODO: Consider whether birthGeneration belongs here.
        readonly uint _birthGeneration;
        readonly ConnectionGeneList _connectionGeneList;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs with the provided ID, birth generation and gene lists.
        /// </summary>
        public NeatGenome(uint id, uint birthGeneration,
                          ConnectionGeneList connectionGeneList)
        {
            _id = id;
            _birthGeneration = birthGeneration;
            _connectionGeneList = connectionGeneList;
        }

        #endregion

        #region Properties [NEAT Genome Specific]

        /// <summary>
        /// Gets the genome's list of connection genes.
        /// </summary>
        public ConnectionGeneList ConnectionGeneList
        {
            get { return _connectionGeneList; }
        }

        #endregion

        #region IGenome


        public uint Id { get { return _id; } }
        public FitnessInfo FitnessInfo { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public uint BirthGeneration => throw new NotImplementedException();

        public object[] AuxObjects { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        #endregion




    }
}