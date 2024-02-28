namespace RegisterAPI.Model.Common
{
    public class ResultResponseObject<T>
    {
        public bool Success => ErrorMessages == null || !ErrorMessages.Any();
        public T? Value { get; set; }
        ICollection<KeyValuePair<string, string>> ErrorMessages { get; set; }
        public void AddError(string errorResource)
        {
            if (ErrorMessages == null)
            {
                ErrorMessages = new List<KeyValuePair<string, string>>();
            }

            if (!string.IsNullOrEmpty(errorResource))
            {
                ErrorMessages.Add(new KeyValuePair<string, string>(errorResource, null));
            }
        }
    }
}
