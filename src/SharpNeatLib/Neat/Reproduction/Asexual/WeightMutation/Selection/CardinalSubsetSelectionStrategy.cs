﻿using System;
using Redzen.Numerics;
using Redzen.Random;

namespace SharpNeat.Neat.Reproduction.Asexual.WeightMutation.Selection
{
    /// <summary>
    /// Strategy for selecting a sub-set of items from a superset. 
    /// The number of items to select is a fixed number (the selection cardinality), unless the superset is smaller
    /// in which case all items in the superset are selected.
    /// </summary>
    public class CardinalSubsetSelectionStrategy : ISubsetSelectionStrategy
    {
        readonly int _selectCount;
        readonly IRandomSource _rng;

        #region Constructor

        /// <summary>
        /// Construct with the given selection count (selection cardinality).
        /// </summary>
        /// <param name="selectCount">The number of items to select.</param>
        public CardinalSubsetSelectionStrategy(
            int selectCount, IRandomSource rng)
        {
            _selectCount = selectCount;
            _rng = rng;
        }

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Select a subset of items from a superset of a given size.
        /// </summary>
        /// <param name="supersetCount">The size of the superset to select from.</param>
        /// <returns>An array of indexes that are the selected items.</returns>
        public int[] SelectSubset(int supersetCount)
        {
            // Note. Ideally we'd return a sorted list of indexes to improve performance of the code that consumes them,
            // however, the sampling process inherently produces samples in randomized order, thus the decision of whether
            // to sort or not depends on the cost to the code using the samples. I.e. don't sort here!
            int selectionCount = Math.Min(_selectCount, supersetCount);
            int[] idxArr = new int[selectionCount];
            DiscreteDistributionUtils.SampleUniformWithoutReplacement(supersetCount, idxArr, _rng);            
            return idxArr;
        }

        #endregion
    }
}
