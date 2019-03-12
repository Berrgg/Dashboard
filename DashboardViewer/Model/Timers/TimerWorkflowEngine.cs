
namespace DashboardViewer.Model.Timers
{
    public class TimerWorkflowEngine
    {
        public void Run(ITimerWorkflow workflow)
        {
            if(workflow != null)
            {
                foreach (IDashboardTimer timer in workflow.GetTimers())
                {
                    timer.Execute();
                }
            }
        }
    }
}
