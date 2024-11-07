using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Core;

public class DecimalToDoubleConverter : ValueConverter<decimal, double>
{
    public DecimalToDoubleConverter() : base(Serialize, Deserialize, null)
    {
    }

    static readonly Expression<Func<decimal, double>> Serialize = x => decimal.ToDouble(x);
    static readonly Expression<Func<double, decimal>> Deserialize = x => (decimal)x;
}
