﻿using Redzen.Numerics;

namespace SharpNeat.Neat.Reproduction.Asexual
{
    public class NeatReproductionAsexualSettings
    {
        #region Auto Properties [Genome Mutation Settings]

        /// <summary>
        /// Probability that a genome mutation is a connection weights mutation.
        /// </summary>
        public double ConnectionWeightMutationProbability { get; set; } = 0.94;

        /// <summary>
        /// Probability that a genome mutation is an 'add node' mutation.
        /// </summary>
        public double AddNodeMutationProbability { get; set; } = 0.01;

        /// <summary>
        /// Probability that a genome mutation is an 'add connection' mutation.
        /// </summary>
        public double AddConnectionMutationProbability { get; set; } = 0.025;

        /// <summary>
        /// Probability that a genome mutation is a 'delete connection' mutation.
        /// </summary>
        public double DeleteConnectionMutationProbability { get; set; } = 0.025;

        #endregion

        #region Auto Properties [Readonly]

        /// <summary>
        /// The mutation type probability settings represented as a DiscreteDistribution.
        /// </summary>
        public DiscreteDistribution MutationTypeDistribution { get; }
        /// <summary>
        /// A copy of MutationTypeDistribution but with all destructive mutations (i.e. delete connections)
        /// removed. Useful when e.g. mutating a genome with just one connection.
        /// </summary>
        public DiscreteDistribution MutationTypeDistributionNonDestructive { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NeatReproductionAsexualSettings()
        {
            this.MutationTypeDistribution = CreateMutationTypeDiscreteDistribution();
            this.MutationTypeDistributionNonDestructive = CreateMutationTypeDiscreteDistribution_NonDestructive();
        }

        #endregion

        #region Private Methods

        private DiscreteDistribution CreateMutationTypeDiscreteDistribution()
        {
            double[] probabilities = new double[] 
                {
                    this.ConnectionWeightMutationProbability, 
                    this.AddNodeMutationProbability,
                    this.AddConnectionMutationProbability,
                    this.DeleteConnectionMutationProbability
                };
            return new DiscreteDistribution(probabilities);
        }

        private DiscreteDistribution CreateMutationTypeDiscreteDistribution_NonDestructive()
        {
            double[] probabilities = new double[] 
                {
                    this.ConnectionWeightMutationProbability, 
                    this.AddNodeMutationProbability,
                    this.AddConnectionMutationProbability
                };
            return new DiscreteDistribution(probabilities);
        }

        #endregion
    }
}
