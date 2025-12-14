using MudBlazor;
using BankDatabase.Enums;

namespace BankDatabase.Abstractions.Cron;

public class CronHandler
{
    public static string EmptyCronString { get; } = "* * * * *";
    public static string EmptyCronValue { get; } = "*";

    public CronCategory Minutes = new CronCategory(0, "Minutes");
    public CronCategory Hours = new CronCategory(1, "Hours");
    public CronCategory MonthDays = new CronCategory(2, "Month Days");
    public CronCategory WeekDays = new CronCategory(4, "Week Days", enableVisualSelection: true);

    private ILogger logger;
    public class CronCategory
    {
        public string CronCategoryName = string.Empty;
        public int CronStringIndex = -1;
        public List<string> AllValues = new List<string>();
        public List<string> SelectedValues = new List<string>();
        public string ValueInput = string.Empty;
        public bool ValueInputError = false;
        public string ValueInputErrorText = string.Empty;
        public CronType CronType;

        public bool IsVisualSelectionEnabled { get; init; }

        public CronCategory(int cronStringIndex, string cronCategoryName, bool enableVisualSelection = false)
        {
            IsVisualSelectionEnabled = enableVisualSelection;
            CronCategoryName = cronCategoryName;
            CronStringIndex = cronStringIndex;
        }
    }
    public CronHandler(ILogger logger)
    {
        this.logger = logger;

        WeekDays.AllValues = new List<string>() { "MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN" };
        MonthDays.AllValues = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16",
                                        "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };

        Hours.AllValues = new List<string>();
        Minutes.AllValues = new List<string>();

        for (int i = 1; i <= 24; i++)
        {
            Hours.AllValues.Add($"{i}");
        }

        for (int i = 1; i <= 60; i++)
        {
            Minutes.AllValues.Add($"{i}");
        }

        WeekDays.CronType = CronType.Enum;
        MonthDays.CronType = CronType.Enum;
        Hours.CronType = CronType.Enum;
        Minutes.CronType = CronType.Enum;

        MonthDays.ValueInput = EmptyCronValue;
        WeekDays.ValueInput = EmptyCronValue;
        Hours.ValueInput = EmptyCronValue;
        Minutes.ValueInput = EmptyCronValue;
    }
    public string HandleCronInput(string cron, CronCategory cronCategory)
    {
        var result = GetCronWithValue(cron, cronCategory);
        cron = result.cron;
        cronCategory.ValueInputError = !result.success;
        cronCategory.ValueInputErrorText = result.errorText;

        return cron;
    }
    public string HandleCronClick(string value, string cron, CronCategory cronCategory)
    {
        string newCron = string.Empty;

        if (cronCategory.IsVisualSelectionEnabled)
        {
            cronCategory.SelectedValues = GetSelectedValuesByClick(value, cronCategory);
            cronCategory.ValueInput = GetPartBySelectedValues(value, cronCategory);
            newCron = HandleCronInput(cron, cronCategory);
        }
        else
        {
            logger.LogError("Trying to handle button click to change cron value, but the visual selcetion of the cron part is not enabled");
        }

        return newCron;
    }
    public string GetClearedCron(string cron, CronCategory cronCategory)
    {
        cronCategory.ValueInput = EmptyCronValue;
        string newCron = HandleCronInput(cron, cronCategory);

        return newCron;
    }
    public Color GetCronRowButtonStyle(string data, CronCategory cronCategory)
    {
        var result = cronCategory.SelectedValues.Contains(data) ? Color.Primary : Color.Default;

        return result;
    }
    public bool ApplyCronOnSelectedValues(string cron, CronCategory? cronCategory = null)
    {
        if (CheckCronStringFormat(cron))
        {
            if (cronCategory != null)
            {
                ApplyCronOnSelectedValuesByCategory(cron, cronCategory);
            }
            else
            {
                ApplyCronOnSelectedValuesByCategory(cron, WeekDays);
                ApplyCronOnSelectedValuesByCategory(cron, MonthDays);
                ApplyCronOnSelectedValuesByCategory(cron, Hours);
                ApplyCronOnSelectedValuesByCategory(cron, Minutes);
            }

            return true;
        }

        return false;
    }
    private string GetPartBySelectedValues(string value, CronCategory cronCategory)
    {
        string cronPart = string.Empty;

        if (cronCategory.IsVisualSelectionEnabled)
        {
            if (cronCategory.CronType == CronType.Range)
            {
                if (cronCategory.SelectedValues.Count == 0)
                {
                    cronPart = EmptyCronValue;
                }
                else if (cronCategory.SelectedValues.Count == 1)
                {
                    cronPart = $"{cronCategory.SelectedValues[0]}";
                }
                else
                {
                    var indexes = new List<int>();

                    for (int i = 0; i < cronCategory.SelectedValues.Count; i++)
                    {
                        indexes.Add(cronCategory.AllValues.IndexOf(cronCategory.SelectedValues[i]));
                    }

                    int min = indexes.Min();
                    int max = indexes.Max();

                    cronPart = $"{cronCategory.AllValues[min]}-{cronCategory.AllValues[max]}";
                }
            }
            else if (cronCategory.CronType == CronType.Enum)
            {
                if (cronCategory.SelectedValues.Count == 0)
                {
                    cronPart = EmptyCronValue;
                }
                else if (cronCategory.SelectedValues.Count == 1)
                {
                    cronPart = $"{cronCategory.SelectedValues[0]}";
                }
                else
                {
                    cronPart = string.Join(",", cronCategory.SelectedValues);
                }
            }
        }

        return cronPart;
    }
    private List<string> GetSelectedValuesByClick(string value, CronCategory cronCategory)
    {
        if (cronCategory.CronType == CronType.Range)
        {
            if (cronCategory.SelectedValues.Count == 0)
            {
                cronCategory.SelectedValues.Add(value);
            }
            else if (cronCategory.SelectedValues.Count == 1)
            {
                int f = cronCategory.AllValues.IndexOf(value);
                int s = cronCategory.AllValues.IndexOf(cronCategory.SelectedValues[0]);

                cronCategory.SelectedValues = new List<string>();

                if (f > s)
                {
                    for (int i = s; i <= f; i++)
                    {
                        cronCategory.SelectedValues.Add(cronCategory.AllValues[i]);
                    }
                }
                else if (f < s)
                {
                    cronCategory.SelectedValues.Add(value);
                }
            }
            else if (cronCategory.SelectedValues.Count > 1)
            {
                cronCategory.SelectedValues = new List<string>() { value };
            }
        }
        else if (cronCategory.CronType == CronType.Enum)
        {
            if (cronCategory.SelectedValues.Contains(value))
            {
                cronCategory.SelectedValues.Remove(value);
            }
            else
            {
                if (cronCategory.SelectedValues.Count == 0)
                {
                    cronCategory.SelectedValues.Add(value);
                }
                else
                {
                    int valueIndex = cronCategory.AllValues.IndexOf(value);
                    int currentIndex = 0;

                    for (int i = 0; i < cronCategory.SelectedValues.Count; i++)
                    {
                        currentIndex = cronCategory.AllValues.IndexOf(cronCategory.SelectedValues[i]);

                        if (currentIndex > valueIndex)
                        {
                            cronCategory.SelectedValues.Insert(i, value);
                            break;
                        }

                        if (i == cronCategory.SelectedValues.Count - 1)
                        {
                            cronCategory.SelectedValues.Insert(i + 1, value);
                            break;
                        }
                    }
                }

            }
        }

        return cronCategory.SelectedValues;
    }
    private List<string> GetSelectedValuesByPart(CronCategory cronCategory)
    {
        if (cronCategory.ValueInput == EmptyCronValue)
        {
            cronCategory.SelectedValues = new List<string>();
        }
        else
        {
            if (cronCategory.CronType == CronType.Range)
            {
                var values = cronCategory.ValueInput.Split('-');

                if (values.Length == 1)
                {
                    cronCategory.SelectedValues = new List<string> { values[0] };
                }
                else
                {
                    int f = cronCategory.AllValues.IndexOf(values[0]);
                    int s = cronCategory.AllValues.IndexOf(values[1]);

                    cronCategory.SelectedValues = new List<string>();

                    for (int i = f; i <= s; i++)
                    {
                        cronCategory.SelectedValues.Add(cronCategory.AllValues[i]);
                    }
                }
            }
            else if (cronCategory.CronType == CronType.Enum)
            {
                var values = cronCategory.ValueInput.Split(",");

                cronCategory.SelectedValues = new List<string>();

                for (int i = 0; i < values.Length; i++)
                {
                    cronCategory.SelectedValues.Add(values[i]);
                }
            }
        }

        return cronCategory.SelectedValues;
    }
    private (bool success, string errorText, string cron) GetCronWithValue(string cron, CronCategory cronCategory)
    {
        var result = CheckCronPartFormat(cronCategory.ValueInput, cronCategory);

        if (result.success)
        {
            var data = cron.Split(' ');

            data[cronCategory.CronStringIndex] = cronCategory.ValueInput;

            cron = string.Join(' ', data);

            ApplyCronOnSelectedValues(cron, cronCategory);

            return (true, string.Empty, cron);
        }

        return (false, result.errorText, cron);
    }
    private (bool success, string errorText) CheckCronPartFormat(string part, CronCategory cronCategory)
    {
        if (part.Equals(EmptyCronValue))
        {
            return (true, string.Empty);
        }
        else if (part.Contains(","))
        {
            var values = part.Split(',');

            var result = VerifyCronValues(values, cronCategory);

            return (result.success, result.errorText);
        }
        else if (part.Contains("-"))
        {
            var values = part.Split('-');

            if (values.Length == 2)
            {
                var result = VerifyCronValues(values, cronCategory);
                return (result.success, result.errorText);
            }
            else
            {
                return (false, "value range should be specified with only two values (start & end)");
            }
        }
        else
        {
            var result = VerifyCronValues([part], cronCategory);

            return (result.success, result.errorText);
        }
    }
    private (bool success, string errorText) VerifyCronValues(string[] values, CronCategory cronCategory)
    {
        foreach (var value in values)
        {
            if (!cronCategory.AllValues.Contains(value))
            {
                return (false, "values were unrecognized");
            }
        }

        if (values.Distinct().Count() != values.Length)
        {
            return (false, "values should be unique");
        }

        List<int> valueIndexes = new List<int>();

        foreach (var value in values)
        {
            int index = cronCategory.AllValues.IndexOf(value);

            valueIndexes.Add(index);
        }

        if (!valueIndexes.SequenceEqual(valueIndexes.OrderBy(x => x)))
        {
            return (false, "values should be ordered by ascending");
        }

        return (true, string.Empty);
    }
    private void ApplyCronOnSelectedValuesByCategory(string cron, CronCategory cronCategory)
    {
        var data = cron.Split(' ');

        cronCategory.ValueInput = data[cronCategory.CronStringIndex];
        cronCategory.CronType = GetCronTypeByPart(data[cronCategory.CronStringIndex], cronCategory.CronType);

        if (cronCategory.IsVisualSelectionEnabled)
        {
            cronCategory.SelectedValues = GetSelectedValuesByPart(cronCategory);
        }
    }
    private bool CheckCronStringFormat(string cron)
    {
        var length = EmptyCronString.Split(' ').Length;

        var parts = cron.Split(' ');

        if (parts.Length != length)
        {
            return false;
        }

        for (int i = 0; i < parts.Length; i++)
        {
            if (i == WeekDays.CronStringIndex)
            {
                var result = CheckCronPartFormat(parts[WeekDays.CronStringIndex], WeekDays);

                if (!result.success)
                {
                    return false;
                }
            }

            if (i == MonthDays.CronStringIndex)
            {
                var result = CheckCronPartFormat(parts[MonthDays.CronStringIndex], MonthDays);

                if (!result.success)
                {
                    return false;
                }
            }

            if (i == Hours.CronStringIndex)
            {
                var result = CheckCronPartFormat(parts[Hours.CronStringIndex], Hours);

                if (!result.success)
                {
                    return false;
                }
            }

            if (i == Minutes.CronStringIndex)
            {
                var result = CheckCronPartFormat(parts[Minutes.CronStringIndex], Minutes);

                if (!result.success)
                {
                    return false;
                }
            }
        }

        return true;
    }
    private CronType GetCronTypeByPart(string part, CronType cronType)
    {
        if (part.Contains(','))
        {
            return CronType.Enum;
        }
        else if (part.Contains('-'))
        {
            return CronType.Range;
        }

        return cronType;
    }
}