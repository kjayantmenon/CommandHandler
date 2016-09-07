namespace Common
{
    public enum CmdStatus
    {
        Dispatched, Delivered, QueueForExec, ExecInProgress, ExecCompleteWithSuccess, ExecFailed, Unknown
    }
}