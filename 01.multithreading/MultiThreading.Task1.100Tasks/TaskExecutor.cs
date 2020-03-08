using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task1._100Tasks.Classes
{
    public static class TasksExecutor
    {
        public static void Execute(Task[] tasks)
        {
            if(tasks == null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            tasks.ToList().ForEach(task => task.Start());
                                   
            Task.WaitAll(tasks);
        }

        public static Task[] Create(int taskCount, Action<object> executionFunction)
        {
            if(taskCount < 0)
            {
                throw new ArgumentException("Tasks count can not be negative number.");
            }
            if(executionFunction == null)
            {
                throw new ArgumentNullException(nameof(executionFunction));
            }

            Task[] taskList = new Task[taskCount]; 
            for(int i = 0; i < taskCount; i++)
            {
                taskList[i] = new Task(executionFunction, i);
            }

            return taskList;
        }
    }
}
