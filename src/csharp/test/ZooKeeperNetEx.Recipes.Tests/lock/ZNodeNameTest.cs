﻿using System.Collections.Generic;
using Xunit;

// 
// <summary>
// Licensed to the Apache Software Foundation (ASF) under one or more
// contributor license agreements.  See the NOTICE file distributed with
// this work for additional information regarding copyright ownership.
// The ASF licenses this file to You under the Apache License, Version 2.0
// (the "License"); you may not use this file except in compliance with
// the License.  You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </summary>
namespace org.apache.zookeeper.recipes.@lock
{
	/// <summary>
	/// test for znodenames
	/// </summary>
	public sealed class ZNodeNameTest 
	{
        [Fact]
		public void testOrderWithSamePrefix()
		{
			string[] names = {"x-3", "x-5", "x-11", "x-1"};
			string[] expected = {"x-1", "x-3", "x-5", "x-11"};
			assertOrderedNodeNames(names, expected);
		}

        [Fact]
        public void testOrderWithDifferentPrefixes()
        {
            string[] names = { "r-3", "r-2", "r-1", "w-2", "w-1" };
            string[] expected = { "r-1", "w-1", "r-2", "w-2", "r-3" };
            assertOrderedNodeNames(names, expected);
        }

        [Fact]
        public void testOrderWithDifferentPrefixIncludingSessionId()
        {
            string [] names = { "x-242681582799028564-0000000002", "x-170623981976748329-0000000003", "x-98566387950223723-0000000001" };
            string [] expected = { "x-98566387950223723-0000000001", "x-242681582799028564-0000000002", "x-170623981976748329-0000000003" };
            assertOrderedNodeNames(names, expected);
        }

        [Fact]
        public void testOrderWithExtraPrefixes() {
            string[] names = { "r-1-3-2", "r-2-2-1", "r-3-1-3" };
            string[] expected = { "r-2-2-1", "r-1-3-2", "r-3-1-3" };
            assertOrderedNodeNames(names, expected);
        }

	    private static void assertOrderedNodeNames(string[] names, string[] expected) {

			SortedSet<ZNodeName> nodeNames = new SortedSet<ZNodeName>();
			foreach (string name in names) {
				nodeNames.Add(new ZNodeName(name));
			}
	        Assert.assertEquals("The SortedSet does not have the expected size!", nodeNames.size(), expected.Length);

			int index = 0;
			foreach (ZNodeName nodeName in nodeNames) {
				string name = nodeName.Name;
                Assert.assertEquals("Node " + index, expected[index++], name);
			}
		}

	}

    [CollectionDefinition("Setup")]
    public class SetupCollection : ICollectionFixture<TestsSetup>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}