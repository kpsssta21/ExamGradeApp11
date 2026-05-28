using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ExamGradeApp11;

namespace ExamGradeApp11.Tests
{
    [TestClass]
    public class ExamCalculatorTests
    {
        #region Корректные данные
        
        [TestMethod]
        public void Calculate_BU_AllMax_ReturnsTotal50_Grade5()
        {
            int m1 = 10, m2 = 15, m3 = 25, m4 = 0, m5 = 0;
            string level = "БУ";

            var (total, grade) = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);

            Assert.AreEqual(50, total);
            Assert.AreEqual(5, grade);
        }

        [TestMethod]
        public void Calculate_BU_Minimum_ReturnsTotal0_Grade2()  // ← Исправлено: пробел после void
        {
            int m1 = 0, m2 = 0, m3 = 0, m4 = 0, m5 = 0;
            string level = "БУ";

            var (total, grade) = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);

            Assert.AreEqual(0, total);
            Assert.AreEqual(2, grade);
        }

        [TestMethod]
        public void Calculate_PU_AllMax_ReturnsTotal75_Grade5()
        {
            int m1 = 10, m2 = 15, m3 = 25, m4 = 25, m5 = 0;
            string level = "ПУ";

            var (total, grade) = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);

            Assert.AreEqual(75, total);
            Assert.AreEqual(5, grade);
        }

        [TestMethod]
        public void Calculate_PUPlus_AllMax_ReturnsTotal100_Grade5()
        {
            int m1 = 10, m2 = 15, m3 = 25, m4 = 25, m5 = 25;
            string level = "ПУ+";

            var (total, grade) = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);

            Assert.AreEqual(100, total);
            Assert.AreEqual(5, grade);
        }

        [TestMethod]
        public void Calculate_GradeBoundary_89Percent_Returns4()
        {
            int m1 = 10, m2 = 15, m3 = 19, m4 = 0, m5 = 0;
            string level = "БУ";

            var (total, grade) = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);

            Assert.AreEqual(44, total);
            Assert.AreEqual(4, grade);
        }

        [TestMethod]
        public void Calculate_GradeBoundary_90Percent_Returns5()
        {
            int m1 = 10, m2 = 15, m3 = 20, m4 = 0, m5 = 0;
            string level = "БУ";

            var (total, grade) = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);

            Assert.AreEqual(45, total);
            Assert.AreEqual(5, grade);
        }

        [TestMethod]
        public void Calculate_GradeBoundary_75Percent_Returns4()
        {
            int m1 = 8, m2 = 15, m3 = 15, m4 = 0, m5 = 0;
            string level = "БУ";

            var (total, grade) = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);

            Assert.AreEqual(38, total);
            Assert.AreEqual(4, grade);
        }

        [TestMethod]
        public void Calculate_GradeBoundary_60Percent_Returns3()
        {
            int m1 = 5, m2 = 10, m3 = 15, m4 = 0, m5 = 0;
            string level = "БУ";

            var (total, grade) = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);

            Assert.AreEqual(30, total);
            Assert.AreEqual(3, grade);
        }

        [TestMethod]
        public void Calculate_GradeBoundary_40Percent_Returns2()
        {
            int m1 = 0, m2 = 10, m3 = 10, m4 = 0, m5 = 0;
            string level = "БУ";

            var (total, grade) = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);

            Assert.AreEqual(20, total);
            Assert.AreEqual(2, grade);
        }

        #endregion

        #region Граничные значения

        [TestMethod]
        public void Calculate_Module1_BoundaryMax_Returns10()
        {
            var (total, grade) = ExamCalculator.Calculate(10, 0, 0, 0, 0, "БУ");
            Assert.AreEqual(10, total);
        }

        [TestMethod]
        public void Calculate_Module1_BoundaryMin_Returns0()
        {
            var (total, grade) = ExamCalculator.Calculate(0, 0, 0, 0, 0, "БУ");
            Assert.AreEqual(0, total);
        }

        [TestMethod]
        public void Calculate_Module2_BoundaryMax_Returns15()
        {
            var (total, grade) = ExamCalculator.Calculate(0, 15, 0, 0, 0, "БУ");
            Assert.AreEqual(15, total);
        }

        [TestMethod]
        public void Calculate_Module3_BoundaryMax_Returns25()
        {
            var (total, grade) = ExamCalculator.Calculate(0, 0, 25, 0, 0, "БУ");
            Assert.AreEqual(25, total);
        }

        [TestMethod]
        public void Calculate_PU_Module4_BoundaryMax_Returns25()
        {
            var (total, grade) = ExamCalculator.Calculate(0, 0, 0, 25, 0, "ПУ");
            Assert.AreEqual(25, total);
        }

        [TestMethod]
        public void Calculate_PUPlus_Module5_BoundaryMax_Returns25()
        {
            var (total, grade) = ExamCalculator.Calculate(0, 0, 0, 0, 25, "ПУ+");
            Assert.AreEqual(25, total);
        }

        #endregion

        #region Некорректные данные (исключения)

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_InvalidLevel_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, 0, 0, 0, 0, "НЕВЕРНЫЙ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_Module1_Negative_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(-1, 0, 0, 0, 0, "БУ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_Module1_ExceedsMax_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(11, 0, 0, 0, 0, "БУ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_Module2_Negative_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, -5, 0, 0, 0, "БУ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_Module2_ExceedsMax_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, 16, 0, 0, 0, "БУ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_Module3_Negative_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, 0, -1, 0, 0, "БУ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_Module3_ExceedsMax_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, 0, 26, 0, 0, "БУ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_PU_Module4_Negative_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, 0, 0, -1, 0, "ПУ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_PU_Module4_ExceedsMax_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, 0, 0, 30, 0, "ПУ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_PUPlus_Module5_Negative_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, 0, 0, 0, -1, "ПУ+");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_PUPlus_Module5_ExceedsMax_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, 0, 0, 0, 26, "ПУ+");
        }

        #endregion

        #region Дополнительные проверки

        [TestMethod]
        public void Calculate_PU_Module4IgnoredForBU_NoException()
        {
            var (total, grade) = ExamCalculator.Calculate(5, 5, 10, 999, 0, "БУ");
            Assert.AreEqual(20, total);
        }

        [TestMethod]
        public void Calculate_PUPlus_WithPartialScores_ReturnsCorrectGrade()
        {
            var (total, grade) = ExamCalculator.Calculate(5, 8, 15, 10, 5, "ПУ+");
            Assert.AreEqual(43, total);
            Assert.AreEqual(2, grade);
        }

        [TestMethod]
        public void Calculate_PU_WithPartialScores_ReturnsCorrectGrade()
        {
            var (total, grade) = ExamCalculator.Calculate(8, 12, 20, 15, 0, "ПУ");
            Assert.AreEqual(55, total);
            Assert.AreEqual(3, grade);
        }

        #endregion
    }
}