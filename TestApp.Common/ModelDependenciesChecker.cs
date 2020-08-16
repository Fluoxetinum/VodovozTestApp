using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Common.Interfaces;

namespace TestApp.Common
{
    public class ModelDependenciesChecker
    {
        public bool CheckNoCycles(IModelDependency rootModelDependency)
        {
            if (rootModelDependency == null)
            {
                return true;
            }

            var visited = new HashSet<Guid>();
            var opened = new HashSet<Guid>();

            var stack = new Stack<IModelDependency>();

            stack.Push(rootModelDependency);

            while (stack.Count != 0)
            {
                IModelDependency modelDependency = stack.Peek();

                if (visited.Contains(modelDependency.Id))
                {
                    stack.Pop();
                    continue;
                }

                if (opened.Contains(modelDependency.Id))
                {
                    stack.Pop();
                    visited.Add(modelDependency.Id);
                    continue;
                }

                opened.Add(modelDependency.Id);

                foreach (var nextDependency in modelDependency.GetDependencies())
                {
                    if (nextDependency == null) continue;
                    
                    if (opened.Contains(nextDependency.Id))
                    {
                        return false;
                    }

                    stack.Push(nextDependency);
                }
            }

            return true;
        }
    }
}
