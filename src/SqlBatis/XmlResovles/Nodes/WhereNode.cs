﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SqlBatis.XmlResovles
{
    internal class WhereNode : INodeList
    {
        public List<INode> Nodes { get; private set; } = new List<INode>();
    }
}
