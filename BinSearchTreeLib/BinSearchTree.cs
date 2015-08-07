using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinSearchTreeLib
{
    public class BinSearchTree<T> where T: IComparer<T>
    {
        Comparer<T> logic = Comparer<T>.Default;
        private T value;
        private BinSearchTree<T> parent;
        private BinSearchTree<T> left;
        private BinSearchTree<T> right;

        public BinSearchTree(T value)
        {
            if (value==null)
                throw new ArgumentNullException("Null value");
            this.value = value;
        }

        public BinSearchTree(T value, Comparer<T> customLogic)
            : this(value)
        {            
            this.logic = customLogic;
        }

        public void Add (T value)
        {
            this.AddTree(new BinSearchTree<T>(value, this.logic));            
        }

        private void AddTree(BinSearchTree<T> addTree)
        {
            if (logic.Compare(this.value,addTree.value) == 1)
                {
                    if (left == null)
                    {
                        this.left = addTree;
                        this.left.parent = this;
                    }

                    left.AddTree(addTree);
                }
            else if (logic.Compare(this.value, addTree.value) == -1)
                {
                    if (right == null)
                    {
                        this.right = addTree;
                        this.right.parent = this;
                    }
                    right.AddTree(addTree);
                }
                else
                {
                    throw new ArgumentException("This value already exist in tree");
                }            
        }

        public BinSearchTree<T> Search (T searchValue)
        {
            if (this.value == null)
                return null;
            if (logic.Compare(this.value, searchValue) == 0)
                return this;
            if (logic.Compare(this.value, searchValue) == 1)
            {
                if (left != null)
                    return this.left.Search(value);
                else return null;
            }
            if (logic.Compare(this.value, searchValue) == -1)
            {
                if (right != null)
                    return this.right.Search(value);
                else return null;
            }
            return null;
        }

        public bool Remove(T value) //remove ne dopisan
        {
            var removeTree = this.Search(value);
            if (removeTree == null)
                return false;
            else
            {
                if (removeTree.parent != null)
                {
                    if (removeTree.left != null)
                    {
                        if (removeTree.parent.left == removeTree)
                        {
                            removeTree.parent.left = removeTree.left;
                            removeTree.left.parent = removeTree.parent;
                            removeTree.left.right.AddTree(removeTree.right);
                        }
                        if (removeTree.parent.right == removeTree)
                        {
                            removeTree.parent.right = removeTree.left;
                            removeTree.left.parent = removeTree.parent;
                            removeTree.left.right.AddTree(removeTree.right);
                        }
                    }
                    else
                        if (removeTree.right != null)
                        {
                            if (removeTree.parent.left == removeTree)
                            {
                                removeTree.parent.left = removeTree.right;
                                removeTree.right.parent = removeTree.parent;
                            }
                            if (removeTree.parent.right == removeTree)
                            {
                                removeTree.parent.right = removeTree.right;
                                removeTree.right.parent = removeTree.parent;
                            }
                        }

                }
            }
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            yield return value;
            if (left != null)
                foreach(T item in this.left)
                    yield return value;
            if (right != null)
                foreach (T item in this.right)
                    yield return value;
        } 
    }
}
