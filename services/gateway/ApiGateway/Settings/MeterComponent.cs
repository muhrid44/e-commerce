using System.Diagnostics.Metrics;

namespace ApiGateway.Settings
{
    public class MeterComponent
    {
        private readonly Meter _meter;
        private readonly Counter<long> _requestCounter;
        private readonly Histogram<double> _requestDurationHistogram;

        public MeterComponent()
        {
            _meter = new Meter("ECommerceGateway");
            _requestCounter = _meter.CreateCounter<long>("request_counter", "requests", "Counts the number of requests");
            _requestDurationHistogram = _meter.CreateHistogram<double>("request_duration", "ms", "Request duration in milliseconds");
        }

        public void CountRequest(string endpoint, string method)
        {
            _requestCounter.Add(1, 
                new KeyValuePair<string, object?>("endpoint", endpoint), 
                new KeyValuePair<string, object?>("method", method));
        }

        public void RecordRequestDuration(double duration, string endpoint, string method)
        {
            _requestDurationHistogram.Record(duration, 
                new KeyValuePair<string, object?>("endpoint", endpoint),
                new KeyValuePair<string, object?>("method", method));
        }

    }
}
