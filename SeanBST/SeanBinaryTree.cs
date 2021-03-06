﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeanBST
{
    class SeanBinaryTree<T> where T : IComparable
    {
        private SeanNode<T> head;
        public int Size { get; private set; }
        
        public SeanBinaryTree() { }

        public SeanBinaryTree(T firstEntry){ Add(firstEntry); }

        /// <summary>
        /// Adds a T value to the tree
        /// </summary>
        /// <param name="newEntry"></param>
        public void Add(T newEntry)
        {
            if (head == null) //if there are no entries
            {
                head = new SeanNode<T>(newEntry);
            }
            else
            {
                AddRecurse(newEntry, head);
            }
            Size++;
        }

        private void AddRecurse(T newEntry, SeanNode<T> node)
        {
            if (newEntry.CompareTo(node.Value) <= 0)
            {
                AddLeft(newEntry, node);   
            }
            else
            {
                AddRight(newEntry, node);   
            }

            if (Math.Abs(node.LeftLayers - node.RightLayers) > 1)
            {
                Rebalance(node);
            }
        }

        private void AddLeft(T newEntry, SeanNode<T> node)
        {
            if (node.Left == null)
            {
                SeanNode<T> newNode = new SeanNode<T>(newEntry);
                node.Left = newNode;
                node.Left.Parent = node;
                node.LeftLayers++;
            }
            else
            {
                AddRecurse(newEntry, node.Left);
            }

            if (node.Left.LeftLayers == node.LeftLayers || node.Left.RightLayers == node.LeftLayers)
            {
                node.LeftLayers++;
            }
        }

        private void AddRight(T newEntry, SeanNode<T> node)
        {
            if (node.Right == null)
            {
                SeanNode<T> newNode = new SeanNode<T>(newEntry);
                node.Right = newNode;
                node.Right.Parent = node;
                node.RightLayers++;
            }
            else
            {
                AddRecurse(newEntry, node.Right);
            }

            if (node.Right.RightLayers == node.RightLayers || node.Right.LeftLayers == node.RightLayers)
            {
                node.RightLayers++;
            }
        }

        /// <summary>
        /// Returns an in-order List of the values contained in the tree.
        /// </summary>
        /// <returns>A List<T> of the values in the tree, in order.</returns>
        public List<T> Traverse()
        {
            List<T> values = new List<T>();
            values = BuildTraversalList(values, head);

            return values;
        }

        private List<T> BuildTraversalList(List<T> values, SeanNode<T> node)
        {
            if (node.Left != null)
            {
                values = BuildTraversalList(values, node.Left);
            }

            values.Add(node.Value);

            if (node.Right != null)
            {
                values = BuildTraversalList(values, node.Right);
            }

            return values;
        }

        /// <summary>
        /// Looks for the given value in the tree and returns true if the value is found.
        /// </summary>
        /// <param name="value">The value you're trying to find.</param>
        /// <returns>Returns true if the value is in the tree.</returns>
        public bool Exists(T valueToFind)
        {
            SeanNode<T> nodeGotten = GetNode(valueToFind, head);
            if (nodeGotten == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// If the provided value exists in the tree, deletes it.
        /// Returns true if the value was found and deleted.
        /// If multiple instances of the value exist, only the first value found will be deleted.
        /// </summary>
        /// <param name="valueToDelete">The value you want to delete from the tree.</param>
        /// <returns>Whether the value was found and deleted or not.</returns>
        public void Delete(T valueToDelete)
        {
            if (head == null) //If there's no head, there's nothing to delete.
            {
                return;
            }
            else if (head.Left == null && head.Right == null) //If the head has no children, we can just delete it.
            {
                head = null;
                Size--;
            }
            else //If the head has children, we must search out the desired value, delete it, and move the children
            {
                SeanNode<T> nodeToDelete = GetNode(valueToDelete, head);
                if (nodeToDelete != null)
                {
                    DeleteNode(nodeToDelete);
                    Size--;
                }
            }

            return;
        }

        /// <summary>
        /// Finds and returns a node with the given value. If no such node is found, returns null.
        /// </summary>
        /// <param name="valueToGet">The value you're trying to find.</param>
        /// <param name="currentNode">The starting point for the search.</param>
        /// <returns>Returns a node with the desired value.</returns>
        private SeanNode<T> GetNode(T valueToGet, SeanNode<T> currentNode)
        {
            SeanNode<T> nodeToReturn = null;

            if (valueToGet.Equals(currentNode.Value))
            {
                nodeToReturn = currentNode;
            }
            else if (valueToGet.CompareTo(currentNode.Value) < 0)
            {
                if (currentNode.Left != null)
                {
                    nodeToReturn = GetNode(valueToGet, currentNode.Left);
                }
            }
            else
            {
                if (currentNode.Right != null)
                {
                    nodeToReturn = GetNode(valueToGet, currentNode.Right);
                }
            }

            return nodeToReturn;
        }

        private void DeleteNode(SeanNode<T> node)
        {
            //if the node has no children, we can just delete it.
            if (node.Left == null && node.Right == null)
            {
                ReduceParentLayers(node);
                if (node.Value.CompareTo(node.Parent.Value) <= 0)
                {
                    node.Parent.Left = null;
                }
                else
                {
                    node.Parent.Right = null;
                }
            }
            else if (node.Left == null) //if the node has only one child, we can just pass its child to its parent.
            {
                if (node.Parent != null)
                {
                    ReduceParentLayers(node);
                    if (node.Value.CompareTo(node.Parent.Value) <= 0)
                    {
                        node.Parent.Left = node.Right;
                    }
                    else
                    {
                        node.Parent.Right = node.Right;
                    }
                    node.Right.Parent = node.Parent;
                }
                else //unless it's the head, in which case we make the child the head and delete the old head from its child. 
                {
                    head = node.Right;
                    node.Right.Parent = null;
                }
            }
            else if (node.Right == null)
            {
                if (node.Parent != null)
                {
                    ReduceParentLayers(node);
                    if (node.Value.CompareTo(node.Parent.Value) <= 0)
                    {
                        node.Parent.Left = node.Left;
                    }
                    else
                    {
                        node.Parent.Right = node.Left;
                    }
                    node.Left.Parent = node.Parent;
                }
                else
                {
                    head = node.Left;
                    node.Left.Parent = null;
                }
            }
            else //if the node has two children, we must poll for a replacement.
            {       //we don't need to worry about whether this is the head or not, because we don't actually delete it, only change its value
                node.Value = PollReplacementValue(node);
            }
        }

        /// <summary>
        /// Finds the needed value in the tree, deletes the node where value is found, and returns the found value.
        /// Recurses if found node has children.
        /// </summary>
        /// <param name="node">The node that needs a new value</param>
        /// <returns>The value to be placed in your node</returns>
        private T PollReplacementValue(SeanNode<T> node)
        {
            T returnVal = default(T);

            SeanNode<T> currentNode = new SeanNode<T>();
            currentNode = node.Left;
            while (currentNode.Right != null)
            {
                currentNode = currentNode.Right;
            }

            returnVal = currentNode.Value;

            if (currentNode.Left != null)
            {
                currentNode.Value = PollReplacementValue(currentNode);
            }
            else if (currentNode.Value.CompareTo(currentNode.Parent.Value) <= 0)
            {
                ReduceParentLayers(node);
                currentNode.Parent.Left = null;
            }
            else
            {
                ReduceParentLayers(node);
                currentNode.Parent.Right = null;
            }
            
            return returnVal;
        }

        private void ReduceParentLayers(SeanNode<T> node)
        {
            if(node.Parent != null)
            {
                if (node.Value.CompareTo(node.Parent.Value) <= 0)
                {
                    node.Parent.LeftLayers--;
                    if (node.Parent.LeftLayers >= node.Parent.RightLayers)
                    {
                        ReduceParentLayers(node.Parent);
                    }
                }
                else
                {
                    node.Parent.RightLayers--;
                    if (node.Parent.RightLayers >= node.Parent.LeftLayers)
                    {
                        ReduceParentLayers(node.Parent);
                    }
                }
            }
        }

        private void Rebalance(SeanNode<T> node)
        {
            if(node.LeftLayers - node.RightLayers > 1)
            {
                T valueToMove = node.Value;
                DeleteNode(node);
                AddRecurse(valueToMove, head);
            }

            if(node.RightLayers - node.LeftLayers > 1)
            {
                T valueToMove = node.Value;
                DeleteNodeReverse(node);
                AddRecurse(valueToMove, head);
            }
        }

        private void DeleteNodeReverse(SeanNode<T> node)    //This is, unfortunately, the same as DeleteNode except it calls PollReverse instead
        {
            //if the node has no children, we can just delete it.
            if (node.Left == null && node.Right == null)
            {
                ReduceParentLayers(node);
                if (node.Value.CompareTo(node.Parent.Value) <= 0)
                {
                    node.Parent.Left = null;
                    node.Parent.LeftLayers--;
                }
                else
                {
                    node.Parent.Right = null;
                    node.Parent.RightLayers--;
                }
            }
            else if (node.Left == null) //if the node has only one child, we can just pass its child to its parent.
            {
                if (node.Parent != null)
                {
                    ReduceParentLayers(node);
                    if (node.Value.CompareTo(node.Parent.Value) <= 0)
                    {
                        node.Parent.Left = node.Right;
                    }
                    else
                    {
                        node.Parent.Right = node.Right;
                    }
                    node.Right.Parent = node.Parent;
                }
                else //unless it's the head, in which case we make the child the head and delete the old head from its child. 
                {
                    head = node.Right;
                    node.Right.Parent = null;
                }
            }
            else if (node.Right == null)
            {
                if (node.Parent != null)
                {
                    ReduceParentLayers(node);
                    if (node.Value.CompareTo(node.Parent.Value) <= 0)
                    {
                        node.Parent.Left = node.Left;
                    }
                    else
                    {
                        node.Parent.Right = node.Left;
                    }
                    node.Left.Parent = node.Parent;
                }
                else
                {
                    head = node.Left;
                    node.Left.Parent = null;
                }
            }
            else //if the node has two children, we must poll for a replacement.
            {       //we don't need to worry about whether this is the head or not, because we don't actually delete it, only change its value
                node.Value = PollReplacementValueReverse(node);
            }
        }

        private T PollReplacementValueReverse(SeanNode<T> node) //This is the same as PollReplacementValue except it goes right first, then left.
        {
            T returnVal = default(T);

            SeanNode<T> currentNode = new SeanNode<T>();
            currentNode = node.Right;
            while (currentNode.Left != null)
            {
                currentNode = currentNode.Left;
            }

            returnVal = currentNode.Value;

            if (currentNode.Right != null)
            {
                currentNode.Value = PollReplacementValueReverse(currentNode);
            }
            else if (currentNode.Value.CompareTo(currentNode.Parent.Value) <= 0)
            {
                ReduceParentLayers(node);
                currentNode.Parent.Left = null;
            }
            else
            {
                ReduceParentLayers(node);
                currentNode.Parent.Right = null;
            }

            return returnVal;
        }
    }
}
