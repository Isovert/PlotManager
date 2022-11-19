using PlotManager.Application.Repositories.RepositoryManager;
using PlotManager.Domain.Entities;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotManager.Application.Services
{
    internal class PlotFileImporterCSV
    {
        private readonly IRepositoryManager _repositoryManager;

        public PlotFileImporterCSV(IRepositoryManager repositoryManager)
        {
            this._repositoryManager = repositoryManager;
        }

        public async Task<MemoryStream> GenerateImportFile()
        {
            var features = await _repositoryManager.FeatureRepository.GetAllAsync();
            var plotTemplate = new Plot();
            var fileHeaderList = new List<string>();
            fileHeaderList.Add(nameof(plotTemplate.OrdinalNumber));
            fileHeaderList.AddRange(features.Select(x => x.Name).ToList());
            string headerAsRawText = string.Join(",", fileHeaderList);
            var memoryStream = new MemoryStream();
            var sw = new StreamWriter(memoryStream, Encoding.UTF8);
            sw.WriteLine(headerAsRawText);
            sw.Flush();
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        public async Task<List<Plot>> ParseImportFile(string filePath)
        {
            var csvDataTable = ConvertCSVFileToDataTable(filePath);
            await ValidateHeaders(csvDataTable);
            return await ConvertDataTableToPlotList(csvDataTable);
        }

        private DataTable ConvertCSVFileToDataTable(string filePath)
        {
            var dt = new DataTable();
            using (var fileStream = File.OpenRead(filePath))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    int lineNumber = 0;
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (lineNumber == 0)
                        {
                            string[] columnNames = line.Split('\u002C');
                            foreach (var colName in columnNames)
                            {
                                dt.Columns.Add(colName);
                            }
                        }
                        else
                        {
                            string[] valueSplits = line.Split('\u002C');
                            var row = dt.NewRow();
                            row.ItemArray = valueSplits;
                            dt.Rows.Add(row);
                        }
                        lineNumber++;
                    }
                }
            }
            return dt;
        }

        private async Task ValidateHeaders(DataTable dataTable)
        {
            var validationResult = new List<string>();
            var features = await _repositoryManager.FeatureRepository.GetAllAsync();
            foreach (var feature in features)
            {
                if (!dataTable.Columns.Contains(feature.Name))
                {
                    validationResult.Add($"Import file is missing column: {feature.Name}");
                }
            }
            if (validationResult.Count > 0)
            {
                throw new ImportFileMissingColumnException("Import file is missing one or more columns.", validationResult);
            }
        }

        private async Task<List<Plot>> ConvertDataTableToPlotList(DataTable dataTable)
        {
            var plots = new List<Plot>();
            var extractor = new DataRowObjectExtractor<Plot>();
            var features = await _repositoryManager.FeatureRepository.GetAllAsync();
            foreach (DataRow row in dataTable.Rows)
            {
                var plot = extractor.InitiateObjectFromDataRow(row);
                plot.PlotFeatures = new List<PlotFeatures>();
                foreach (var feature in features)
                {
                    if (row[feature.Name].ToString().Trim() == "1")
                    {
                        plot.PlotFeatures.Add(new PlotFeatures() { FeatureId = feature.Id });
                    }
                }
                plots.Add(plot);
            }
            return plots;
        }
    }
}