using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Infrastructure.Interfaces
{
    public interface IJsonAdapter<T>
    {
        string Serialize(T data);
        T Deserialize(string json);
        Task ExportToFileAsync(T data, string filePath);
        Task<T> ImportFromFileAsync(string filePath);
    }
}
