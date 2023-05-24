namespace MultiProcessor.Data
{
    public static class ProcessorFactory
    {
        private static Processor _getFirstComeFirstServiceProcessor()
        {
            var processor = new 
                Processor((p1, p2) => p1.ArrivalTime - p2.ArrivalTime)
                {
                    ProcessorType = ProcessorSchedulingType.FirstComeFirstService
                };
            return processor;
        }
        private static Processor _getShortestJobFirstProcessor()
        {
            var processor = new 
                Processor((p1, p2) => p1.CpuTime - p2.CpuTime)
                {
                    ProcessorType = ProcessorSchedulingType.ShortestJobFirst
                };
            return processor;
        }
        private static Processor _getRoundRobinProcessor()
        {
            var processor = new 
                Processor((p1, p2) => p1.ArrivalTime - p2.ArrivalTime)
                {
                    ProcessorType = ProcessorSchedulingType.RoundRobin
                };
            return processor;
        }

        public static Processor GetProcessor(ProcessorSchedulingType processorSchedulingType)
        {
            return processorSchedulingType switch
            {
                ProcessorSchedulingType.RoundRobin => _getRoundRobinProcessor(),
                ProcessorSchedulingType.ShortestJobFirst => _getShortestJobFirstProcessor(),
                ProcessorSchedulingType.FirstComeFirstService => _getFirstComeFirstServiceProcessor(),
                _ => _getFirstComeFirstServiceProcessor()
            };
        }
    }
}