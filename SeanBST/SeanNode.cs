using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeanBST
{
    class SeanNode<T> where T : IComparable
    {
        public T Value { get; set; }
        public int LeftLayers { get; set; }
        public int RightLayers { get; set; }
        public SeanNode<T> Parent { get; set; }
        public SeanNode<T> Left { get; set; }
        public SeanNode<T> Right { get; set; }


        public SeanNode() {}
        public SeanNode(T newValue) {Value = newValue;}
    }
}
