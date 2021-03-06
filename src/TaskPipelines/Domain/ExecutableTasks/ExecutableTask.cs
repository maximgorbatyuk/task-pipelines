﻿using System;
using TaskPipelines.Domain.DataAccess;
using TaskPipelines.Domain.Pipelines;

namespace TaskPipelines.Domain.ExecutableTasks
{
    public class ExecutableTask : BaseModel
    {
        protected ExecutableTask()
        {
        }

        public ExecutableTask(string name, ExecutableTask previousExecutableTask = null)
        {
            name.ThrowIfNull(nameof(name));
            Name = name;

            CreatedAt = UpdatedAt = DateTime.Now;
            Duration = new Random().Next(3000);

            if (previousExecutableTask != null)
            {
                PreviousTaskId = previousExecutableTask.Id;
                PipelineId = previousExecutableTask.PipelineId;
            }
        }

        public ExecutableTask(string name, Pipeline pipeline)
        {
            name.ThrowIfNull(nameof(name));
            pipeline.ThrowIfNull(nameof(pipeline));
            Name = name;

            CreatedAt = UpdatedAt = DateTime.Now;
            Duration = new Random().Next(3000);
            PipelineId = pipeline.Id;
        }

        public string Name { get; set; }

        public int Duration { get; set; }

        public string PreviousTaskId { get; set; }

        public string PipelineId { get; set; }

        public void AttachTo(Pipeline pipeline)
        {
            pipeline.ThrowIfNull(nameof(pipeline));

            if (PipelineId != null)
            {
                throw new InvalidOperationException($"The task id{Id} is attached to another pipeline");
            }

            PipelineId = pipeline.Id;
            UpdatedAt = DateTime.Now;
        }

        public System.Threading.Tasks.Task ExecuteAsync()
        {
            return System.Threading.Tasks.Task.Delay(Duration);
        }
    }
}