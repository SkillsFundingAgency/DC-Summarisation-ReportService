﻿using ESFA.DC.IO.AzureStorage.Config.Interfaces;


namespace ESFA.DC.Summarisation.ReportService.Stateless.Config
{
    public class IOConfiguration : IAzureStorageKeyValuePersistenceServiceConfig
    {
        public string ConnectionString { get; set; }

        public string ContainerName { get; set; }
    }
}
