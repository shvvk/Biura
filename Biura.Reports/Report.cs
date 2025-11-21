namespace Biura.Reports
{
    public record Report<T>
    {
        public required DateTime GeneratedAt { get; set; }

        public required T Data { get; set; }

        public Report() { }

        public Report(T data) : this(DateTime.Now, data) { }

        public Report(DateTime generatedAt, T data)
        {
            GeneratedAt = generatedAt;
            Data = data;
        }
    }
}