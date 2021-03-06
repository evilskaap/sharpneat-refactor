﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpNeat.Network;
using SharpNeat.Network.Acyclic;
using SharpNeat.NeuralNet.Double;
using SharpNeat.NeuralNet.Double.ActivationFunctions;

namespace SharpNeat.Tests.NeuralNets.Double
{
    [TestClass]
    public class AcyclicNeuralNetTests
    {
        #region Test Methods

        [TestMethod]
        [TestCategory("AcyclicNeuralNet")]
        public void SingleInput_WeightZero()
        {
            var connList = new List<WeightedDirectedConnection<double>>();
            connList.Add(new WeightedDirectedConnection<double>(0, 1, 0.0));

            // Create graph.
            var digraph = WeightedAcyclicDirectedGraphBuilder<double>.Create(connList, 1, 1);

            // Create neural net
            var actFn = new LogisticFunction();
            var net = new AcyclicNeuralNet(digraph, actFn.Fn, false);

            // Note. The single connection weight is zero, so the input value has no affect.
            // Activate and test.
            net.InputVector[0] = 100.0;
            net.Activate();
            Assert.AreEqual(0.5, net.OutputVector[0]);

            // Activate and test.
            net.InputVector[0] = 0;
            net.Activate();
            Assert.AreEqual(0.5, net.OutputVector[0]);

            // Activate and test.
            net.InputVector[0] = -100;
            net.Activate();
            Assert.AreEqual(0.5, net.OutputVector[0]);
        }

        [TestMethod]
        [TestCategory("AcyclicNeuralNet")]
        public void SingleInput_WeightOne()
        {
            var connList = new List<WeightedDirectedConnection<double>>();
            connList.Add(new WeightedDirectedConnection<double>(0, 1, 1.0));

            // Create graph.
            var digraph = WeightedAcyclicDirectedGraphBuilder<double>.Create(connList, 1, 1);

            // Create neural net
            var actFn = new LogisticFunction();
            var net = new AcyclicNeuralNet(digraph, actFn.Fn, false);

            // Activate and test.
            net.InputVector[0] = 0.0;
            net.Activate();
            Assert.AreEqual(0.5, net.OutputVector[0]);

            // Activate and test.
            net.InputVector[0] = 1.0;
            net.Activate();
            Assert.AreEqual(actFn.Fn(1), net.OutputVector[0]);

            // Activate and test.
            net.InputVector[0] = 10.0;
            net.Activate();
            Assert.AreEqual(actFn.Fn(10.0), net.OutputVector[0]);
        }

        [TestMethod]
        [TestCategory("AcyclicNeuralNet")]
        public void TwoInputs_WeightHalf()
        {
            var connList = new List<WeightedDirectedConnection<double>>
            {
                new WeightedDirectedConnection<double>(0, 2, 0.5),
                new WeightedDirectedConnection<double>(1, 2, 0.5)
            };

            // Create graph.
            var digraph = WeightedAcyclicDirectedGraphBuilder<double>.Create(connList, 2, 1);

            // Create neural net
            var actFn = new LogisticFunction();
            var net = new AcyclicNeuralNet(digraph, actFn.Fn, false);

            // Activate and test.
            net.InputVector[0] = 0.0;
            net.InputVector[1] = 0.0;
            net.Activate();
            Assert.AreEqual(0.5, net.OutputVector[0]);

            // Activate and test.
            net.InputVector[0] = 1.0;
            net.InputVector[1] = 2.0;
            net.Activate();
            Assert.AreEqual(actFn.Fn(1.5), net.OutputVector[0]);

            // Activate and test.
            net.InputVector[0] = 10.0;
            net.InputVector[1] = 20.0;
            net.Activate();
            Assert.AreEqual(actFn.Fn(15.0), net.OutputVector[0]);
        }

        [TestMethod]
        [TestCategory("AcyclicNeuralNet")]
        public void HiddenNode()
        {
            var connList = new List<WeightedDirectedConnection<double>>
            {
                new WeightedDirectedConnection<double>(0, 3, 0.5),
                new WeightedDirectedConnection<double>(1, 3, 0.5),
                new WeightedDirectedConnection<double>(3, 2, 2.0)
            };

            // Create graph.
            var digraph = WeightedAcyclicDirectedGraphBuilder<double>.Create(connList, 2, 1);

            // Create neural net
            var actFn = new LogisticFunction();
            var net = new AcyclicNeuralNet(digraph, actFn.Fn, false);

            // Activate and test.
            net.InputVector[0] = 0.0;
            net.InputVector[1] = 0.0;
            net.Activate();
            Assert.AreEqual(actFn.Fn(1.0), net.OutputVector[0]);

            // Activate and test.
            net.InputVector[0] = 0.5;
            net.InputVector[1] = 0.25;
            net.Activate();
            Assert.AreEqual(actFn.Fn(actFn.Fn(0.375) * 2.0), net.OutputVector[0]);
        }

        [TestMethod]
        [TestCategory("AcyclicNeuralNet")]
        public void Complex_WeightOne()
        {
            var connList = new List<WeightedDirectedConnection<double>>
            {
                new WeightedDirectedConnection<double>(0, 4, 1.0),
                new WeightedDirectedConnection<double>(1, 4, 1.0),
                new WeightedDirectedConnection<double>(1, 5, 1.0),
                new WeightedDirectedConnection<double>(3, 4, 1.0),
                new WeightedDirectedConnection<double>(4, 2, 0.9),
                new WeightedDirectedConnection<double>(5, 3, 1.0)
            };

            // Create graph.
            var digraph = WeightedAcyclicDirectedGraphBuilder<double>.Create(connList, 2, 2);

            // Create neural net
            var actFn = new LogisticFunction();
            var net = new AcyclicNeuralNet(digraph, actFn.Fn, false);

            // Activate and test.
            net.InputVector[0] = 0.5;
            net.InputVector[1] = 0.25;
            net.Activate();

            double output1 = actFn.Fn(actFn.Fn(0.25));
            Assert.AreEqual(output1, net.OutputVector[1]);

            double output0 = actFn.Fn(actFn.Fn(output1 + 0.5 + 0.25) * 0.9);
            Assert.AreEqual(output0, net.OutputVector[0]);
        }

        [TestMethod]
        [TestCategory("AcyclicNeuralNet")]
        public void MultipleInputsOutputs()
        {
            var connList = new List<WeightedDirectedConnection<double>>
            {
                new WeightedDirectedConnection<double>(0, 5, 1.0),
                new WeightedDirectedConnection<double>(1, 3, 1.0),
                new WeightedDirectedConnection<double>(2, 4, 1.0)
            };

            // Create graph.
            var digraph = WeightedAcyclicDirectedGraphBuilder<double>.Create(connList, 3, 3);

            // Create neural net
            var actFn = new LogisticFunction();
            var net = new AcyclicNeuralNet(digraph, actFn.Fn, false);

            // Activate and test.
            net.InputVector[0] = 1.0;
            net.InputVector[1] = 2.0;
            net.InputVector[2] = 3.0;
            net.Activate();
            Assert.AreEqual(actFn.Fn(2.0), net.OutputVector[0]);
            Assert.AreEqual(actFn.Fn(3.0), net.OutputVector[1]);
            Assert.AreEqual(actFn.Fn(1.0), net.OutputVector[2]);
        }

        #endregion
    }
}
