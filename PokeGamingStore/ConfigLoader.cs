using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace PokeGamingStore
{
    internal class ConfigLoader
    {
        public AppConfig LoadConfig(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("Path file konfigurasi tidak boleh kosong.");
            }
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File konfigurasi tidak ditemukan.");
            }

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<AppConfig>(json);
        }
    }
}
