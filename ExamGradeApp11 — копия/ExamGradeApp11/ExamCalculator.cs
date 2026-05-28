using System;

namespace ExamGradeApp11
{
    public static class ExamCalculator
    {
        public const int MaxModule1 = 10;
        public const int MaxModule2 = 15;
        public const int MaxModule3 = 25;
        public const int MaxModule4 = 25;
        public const int MaxModule5 = 25;

        public static (int totalScore, int grade) Calculate(int score1, int score2, int score3, int score4, int score5, string level)
        {
            if (level != "БУ" && level != "ПУ" && level != "ПУ+")
                throw new ArgumentException("Некорректный уровень экзамена. Допустимые значения: БУ, ПУ, ПУ+", nameof(level));

            ValidateScore(score1, 0, MaxModule1, "Модуль 1");
            ValidateScore(score2, 0, MaxModule2, "Модуль 2");
            ValidateScore(score3, 0, MaxModule3, "Модуль 3");

            int total = 0;

            switch (level)
            {
                case "БУ":
                    total = score1 + score2 + score3;
                    break;
                case "ПУ":
                    ValidateScore(score4, 0, MaxModule4, "Модуль 4");
                    total = score1 + score2 + score3 + score4;
                    break;
                case "ПУ+":
                    ValidateScore(score4, 0, MaxModule4, "Модуль 4");
                    ValidateScore(score5, 0, MaxModule5, "Модуль 5");
                    total = score1 + score2 + score3 + score4 + score5;
                    break;
            }

            int grade = CalculateGrade(total, level);
            return (total, grade);
        }

        private static void ValidateScore(int score, int min, int max, string moduleName)
        {
            if (score < min || score > max)
                throw new ArgumentException($"{moduleName}: балл должен быть в диапазоне от {min} до {max}. Текущее значение: {score}");
        }

        private static int CalculateGrade(int totalScore, string level)
        {
            int maxScore;

            
            switch (level)
            {
                case "БУ":
                    maxScore = MaxModule1 + MaxModule2 + MaxModule3;
                    break;
                case "ПУ":
                    maxScore = MaxModule1 + MaxModule2 + MaxModule3 + MaxModule4;
                    break;
                case "ПУ+":
                    maxScore = MaxModule1 + MaxModule2 + MaxModule3 + MaxModule4 + MaxModule5;
                    break;
                default:
                    maxScore = 100;
                    break;
            }

            double percent = (double)totalScore / maxScore * 100;

            if (percent >= 90) return 5;
            if (percent >= 75) return 4;
            if (percent >= 60) return 3;
            if (percent >= 40) return 2;
            return 2;
        }
    }
}