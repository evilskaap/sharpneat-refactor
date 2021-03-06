﻿/* ***************************************************************************
 * This file is part of SharpNEAT - Evolution of Neural Networks.
 * 
 * Copyright 2004-2016 Colin Green (sharpneat@gmail.com)
 *
 * SharpNEAT is free software; you can redistribute it and/or modify
 * it under the terms of The MIT License (MIT).
 *
 * You should have received a copy of the MIT License
 * along with SharpNEAT; if not, see https://opensource.org/licenses/MIT.
 */

using System;
using System.Numerics;

namespace SharpNeat.NeuralNet.Double.ActivationFunctions
{
    /// <summary>
    /// Rectified linear activation unit (ReLU).
    /// </summary>
    public class ReLU : IActivationFunction<double>
    {
        public double Fn(double x)
        {
            return Math.Max(x, 0.0);
        }

        public void Fn(double[] v)
        {
            for(int i=0; i < v.Length; i++) {
                v[i]= Fn(v[i]);
            }
        }

        public void Fn(double[] v, int startIdx, int endIdx)
        {
            for(int i=startIdx; i < endIdx; i++) {
                v[i]= Fn(v[i]);
            }
        }

        public void Fn(double[] v, double[] w, int startIdx, int endIdx)
        {
            for(int i=startIdx; i < endIdx; i++) {
                w[i]= Fn(v[i]);
            }
        }
    }
}
