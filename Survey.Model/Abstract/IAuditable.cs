using System;

namespace Survey.Model.Abstract
{
    public interface IAuditable
    {
        int Id { get; set; }

        DateTime CreatedDate { get; set; }

        DateTime UpdatedDate { get; set; }
    }
}
