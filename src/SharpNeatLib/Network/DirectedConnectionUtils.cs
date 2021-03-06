﻿using System.Collections.Generic;
using Redzen;

namespace SharpNeat.Network
{
    public static class DirectedConnectionUtils
    {
        /// <summary>
        /// Get the index of the first connection with the given source node ID.
        /// </summary>
        /// <param name="connList">The list of connections to search; these must be sorted by source node ID.</param>
        /// <param name="srcNodeId">The source node ID to search for.</param>
        /// <returns>The index of the first connection with the given source node index.</returns>
        /// <remarks>
        /// If srcNodeId is not found and is less than one or more elements in array, the negative number returned is
        /// the bitwise complement of the index of the first connection that is larger than srcNodeId.
        /// If value is not found and value is greater than all connections in array, the negative number returned is the
        /// bitwise complement of the index of the last element plus 1. 
        /// </remarks>
        public static int GetConnectionIndexBySourceNodeId(IList<DirectedConnection> connList, int srcNodeId)
        {
            // Search for a connection with the given source node ID.
            int connIdx = SearchUtils.BinarySearch(connList, srcNodeId,
                (DirectedConnection conn, int nodeId) => conn.SourceId.CompareTo(nodeId));

            // Test for no match, i.e. no connections with the given source node ID.
            if(connIdx < 0) {   
                return connIdx;
            }

            // Note. if there are multiple connections with the given source ID then BinarySearch() will 
            // return the index of one of them, but makes no guarantee regarding which one. As such we scan
            // in reverse for the first connection.
            for(; connIdx > 0 && connList[connIdx-1].SourceId == srcNodeId; connIdx--);
            
            return connIdx;
        }
    }
}
